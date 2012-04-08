using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Common.TimeWindow;

namespace DataStore
{
	public class FilePaths : IDisposable
	{
		private string _baseFolder;
		List<string> _tempFileList = new List<string>();

		public FilePaths(string baseFolder)
		{
			_baseFolder = baseFolder;
		}

		public IEnumerable<string> GetFilesToRead(DateTime start, DateTime end)
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

		#region Get Summary Files Paths
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
				"DaySummary.txt");
		}

		internal string GetWeekSummary(DateTime time)
		{
			ITimeWindow window = new DayTimeWindow(time);
			window = window.ToWeekWindow();
			
			return Path.Combine(_baseFolder,
				window.EndTime.Year.ToString(),
				window.EndTime.Month.ToString(),
				window.EndTime.Day.ToString(),
				"WeekSummary.txt");
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
				if (string.IsNullOrEmpty(fileName) == false)
				{
					filePaths.Add(fileName);
				}

				window.GoNext();
			}

			return filePaths;
		}

		#endregion

		internal void EnsureFileWithFolder(string filename)
		{
			if (File.Exists(filename) == false)
			{
				Directory.CreateDirectory(Path.GetDirectoryName(filename));
				File.Create(filename).Dispose();
			}
		}

		internal string GetTempFile()
		{
			string tempFile = Path.Combine(_baseFolder, Path.GetRandomFileName());
			_tempFileList.Add(tempFile);

			return tempFile;
		}

		public void Dispose()
		{
			foreach (var file in _tempFileList)
			{
				File.Delete(file);
			}
		}

		internal string GetFileName(DateTime time)
		{
			string dirPath = GetDirName(time);

			string fileName = string.Format("{0}.txt", time.Hour);

			return Path.Combine(dirPath, fileName);
		}

		internal string GetDirName(DateTime time)
		{
			string dirPath = Path.Combine(_baseFolder, string.Format("{0}_{1}_{2}", time.Year, time.Month, time.Day));

			if (Directory.Exists(dirPath) == false)
			{
				Directory.CreateDirectory(dirPath);
			}

			return dirPath;
		}

		internal string GetFileNameDay(DateTime time)
		{
			return Path.Combine(GetDirName(time), "DaySummary.txt");
		}
	}
}
