using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Common.TimeWindow;

namespace DataStore
{
	public class FilePaths
	{
		#region File, Folder name constants

		private const string _rootFolder = "GyrfalconData";
		private const string _persistPerMin = "PersistPerMinute.txt";
		private const string _daySummary = "DaySummary.txt";
		private const string _weekSummary = "WeekSummary.txt";

		#endregion

		private string _baseFolder;

		public FilePaths()
			: this(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), _rootFolder))
		{
		}

		public FilePaths(string baseFolder)
		{
			_baseFolder = baseFolder;
		}

		internal IEnumerable<string> GetFilesToRead(DateTime start, DateTime end)
		{
			if (start > end)
			{
				throw new ArgumentException("Start time is greater than end time");
			}

			ITimeWindow window = new DayTimeWindow(start);
			List<string> fileList = new List<string>();

			if (end > window.EndTime)
			{
				window = window.ToWeekWindow();

				if (end > window.EndTime)
				{
					fileList.AddRange(GetAllWeeksBetween(start, end));
				}
				else
				{
					fileList.AddRange(GetAllDaysForWeek(window.StartTime));
				}
			}
			else
			{
				fileList.AddRange(GetAllHourForDay(window.StartTime));
			}
			return fileList;
		}

		internal void EnsureFileWithFolder(string filename)
		{
			if (File.Exists(filename) == false)
			{
				Directory.CreateDirectory(Path.GetDirectoryName(filename));
				File.Create(filename).Dispose();
			}
		}

		#region Get Summary Files Paths

		internal string GetPerMinuteFile()
		{
			string perMinFile = Path.Combine(_baseFolder, _persistPerMin);
			EnsureFileWithFolder(perMinFile);
			return perMinFile;
		}

		internal string GetHourSummary(DateTime time)
		{
			return Path.Combine(_baseFolder, 
				time.Year.ToString(), 
				time.Month.ToString(), 
				time.Day.ToString(), 
				time.Hour.ToString()+".txt");
		}

		internal string GetDaySummary(DateTime time)
		{
			return Path.Combine(_baseFolder,
				time.Year.ToString(),
				time.Month.ToString(),
				time.Day.ToString(),
				_daySummary);
		}

		internal string GetWeekSummary(DateTime time)
		{
			// TODO this method needs to be called by write store at sometime.
			ITimeWindow window = new DayTimeWindow(time);
			window = window.ToWeekWindow();
			
			return Path.Combine(_baseFolder,
				window.EndTime.Year.ToString(),
				window.EndTime.Month.ToString(),
				window.EndTime.Day.ToString(),
				_weekSummary);
		}
		#endregion

		#region Get file list

		/// <summary>
		/// 
		/// </summary>
		/// <param name="date">Date for which hours will be looked into</param>
		/// <returns></returns>
		private List<string> GetAllHourForDay(DateTime date)
		{
			DateTime dt = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
			List<string> filePaths = new List<string>();

			for (int i = 0; i < 24; i++)
			{
				string file = GetHourSummary(dt);
				if (File.Exists(file))
				{
					filePaths.Add(file);
				}

				dt = dt.AddHours(1);
			}

			return filePaths;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="start">Week's start day</param>
		/// <returns></returns>
		private List<string> GetAllDaysForWeek(DateTime start)
		{
			DateTime dt = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0);
			List<string> filePaths = new List<string>();

			for (int i = 0; i < 7; i++)
			{
				string file = GetDaySummary(dt);
				if (File.Exists(file))
				{
					filePaths.Add(file);
				}

				dt = dt.AddDays(1);
			}

			return filePaths;
		}

		private IEnumerable<string> GetAllWeeksBetween(DateTime start, DateTime end)
		{
			List<string> filePaths = new List<string>();

			ITimeWindow window = new DayTimeWindow(start);
			window = window.ToWeekWindow();
			while (end > window.EndTime)
			{
				string fileName = GetWeekSummary(window.EndTime);
				if (File.Exists(fileName))
				{
					filePaths.Add(fileName);
				}

				window.GoNext();
			}

			return filePaths;
		}

		#endregion
	}
}
