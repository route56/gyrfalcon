using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DesktopClient.ProcessMonitor;
using System.Diagnostics;

namespace DesktopClient.ClientStorage
{
	public class DataStore : IDisposable
	{
		private DateTime _startTimeDay;
		private DateTime _startTimeHour;

		private string _persistPerMinFile = Path.GetRandomFileName();

		public DataStore()
		{
			_startTimeDay = _startTimeHour = DateTime.Now;
		}

		public void AddToAggregatedStore(List<DataAtom> dataList)
		{
			using (StreamWriter sw = File.AppendText(_persistPerMinFile))
			{
				foreach (var item in dataList)
				{
					sw.WriteLine(item.ToString());
				}
			}

			if (IsOneHourDone())
			{
				ClassifyPerHour();
			}

			if (IsOneDayDone())
			{
				ClassifyPerDay();
			}
		}

		private List<DataAtom> ReadAggregatedListFromFile(string fileName)
		{
			List<DataAtom> result = new List<DataAtom>();

			// TODO. This needs to be done for strict 1 hour. Sleep/ Cancel etc will abrupt this.
			using (StreamReader sr = File.OpenText(fileName))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					result.Add(DataAtom.FromString(line));
				}
			}

			return result;
		}

		private void Persist(List<DataAtom> finalDataList, string fileName)
		{
			using (StreamWriter sw = File.CreateText(fileName))
			{
				foreach (var item in finalDataList)
				{
					sw.WriteLine(item.ToString());
				}
			}
		}

		private string GetFileName(DateTime time)
		{
			string dirPath = GetDirName(time);

			string fileName = string.Format("{0}.txt", time.Hour);

			return Path.Combine(dirPath, fileName);
		}

		private string GetDirName(DateTime time)
		{
			string dirPath = string.Format("{0}_{1}_{2}", time.Year, time.Month, time.Day);

			if (Directory.Exists(dirPath) == false)
			{
				Directory.CreateDirectory(dirPath);
			}
			return dirPath;
		}

		private List<DataAtom> ReadAggregatedListFromFolder()
		{
			List<DataAtom> result = new List<DataAtom>();

			DirectoryInfo dirInfo = new DirectoryInfo(GetDirName(_startTimeDay));

			foreach (var file in dirInfo.GetFiles())
			{
				result.AddRange(ReadAggregatedListFromFile(file.FullName));
			}

			return result;
		}

		private string GetFileNameDay(DateTime time)
		{
			string dirName = GetDirName(time);

			return Path.Combine(dirName, "DaySummary.txt");
		}

		private bool IsOneHourDone()
		{
			if (_startTimeHour.AddHours(1) < DateTime.Now)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private bool IsOneDayDone()
		{
			if (_startTimeDay.AddDays(1) < DateTime.Now)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private void ClassifyPerHour()
		{
			List<DataAtom> aggregatedDataList = ReadAggregatedListFromFile(_persistPerMinFile);

			List<DataAtom> finalDataList = Classify(aggregatedDataList);

			Persist(finalDataList, GetFileName(_startTimeHour));

			_startTimeHour = _startTimeHour.AddHours(1);
			File.Delete(_persistPerMinFile);
		}

		private static List<DataAtom> Classify(List<DataAtom> aggregatedDataList)
		{
			Classifier cs = new Classifier();

			for (int i = 0; i < aggregatedDataList.Count; i++)
			{
				cs.Add(i, aggregatedDataList[i].Process + aggregatedDataList[i].Title, aggregatedDataList[i].Frequency);
			}

			var csResult = cs.GetClassificationResult();

			List<DataAtom> finalDataList = new List<DataAtom>();

			for (int i = 0; i < csResult.GetLength(0); i++)
			{
				aggregatedDataList[csResult[i, 0]].Frequency = csResult[i, 1];

				finalDataList.Add(aggregatedDataList[csResult[i, 0]]);
			}
			return finalDataList;
		}

		private void ClassifyPerDay()
		{
			List<DataAtom> aggregatedDataList = ReadAggregatedListFromFolder();

			List<DataAtom> finalDataList = Classify(aggregatedDataList);

			Persist(finalDataList, GetFileNameDay(_startTimeDay));

			_startTimeDay = _startTimeDay.AddDays(1);
		}

		public void Dispose()
		{
			ClassifyPerHour();
			Debug.Assert(File.Exists(_persistPerMinFile) == false);
		}
	}
}
