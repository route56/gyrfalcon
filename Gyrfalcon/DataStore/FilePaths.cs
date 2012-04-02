using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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

		internal string GetMonthSummary(DateTime time)
		{
			return Path.Combine(_baseFolder,
				time.Year.ToString(),
				time.Month.ToString(),
				"MonthSummary.txt");
		}
		#endregion

		#region Get file list

		internal List<string> GetAllHourForDay(DateTime time)
		{
			DateTime dt = new DateTime(time.Year, time.Month, time.Day, 0, 0, 0);
			List<string> filePaths = new List<string>();

			for (int i = 0; i < 24; i++)
			{
				string file = GetHourSummary(dt);
				if (File.Exists(file))
				{
					filePaths.Add(file);
				}

				dt.AddHours(1);
			}

			return filePaths;
		}

		internal List<string> GetAllDaysForWeek(DateTime time)
		{
			DateTime dt = new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0);
			List<string> filePaths = new List<string>();

			for (int i = 0; i < 24; i++)
			{
				string file = GetHourSummary(dt);
				if (File.Exists(file))
				{
					filePaths.Add(file);
				}

				dt.AddHours(1);
			}

			return filePaths;
		}

		#endregion
		#region Ensure file is present
		internal void EnsureHourSummary(DateTime time)
		{
			string filename = GetHourSummary(time);
			if (File.Exists(filename) == false)
			{
				Directory.CreateDirectory(Path.Combine(_baseFolder,
					time.Year.ToString(),
					time.Month.ToString(),
					time.Day.ToString()));

				File.Create(filename).Dispose();
			}
		}

		internal void EnsureDaySummary(DateTime time)
		{
			string filename = GetDaySummary(time);
			if (File.Exists(filename) == false)
			{
				Directory.CreateDirectory(Path.Combine(_baseFolder,
					time.Year.ToString(),
					time.Month.ToString(),
					time.Day.ToString()));

				File.Create(filename).Dispose();
			}
		}

		internal void EnsureMonthSummary(DateTime time)
		{
			string filename = GetMonthSummary(time);
			if (File.Exists(filename) == false)
			{
				Directory.CreateDirectory(Path.Combine(_baseFolder,
				time.Year.ToString(),
				time.Month.ToString()));

				File.Create(filename).Dispose();
			}
		}
		#endregion

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
