using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace DataStore
{
	public class WriteStore : IWriteStore
	{
		private DateTime _startTimeDay;
		private DateTime _startTimeHour;

		private string _persistPerMinFile;

		public WriteStore()
		{
			_startTimeDay = _startTimeHour = DateTime.Now;
			FilePathProvider = new FilePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
			_persistPerMinFile = FilePathProvider.GetTempFile();
		}

		public void AddToAggregatedStore(IEnumerable<DataAtom> dataList)
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

		private List<DataAtom> ReadAggregatedListFromFolder()
		{
			List<DataAtom> result = new List<DataAtom>();

			DirectoryInfo dirInfo = new DirectoryInfo(FilePathProvider.GetDirName(_startTimeDay));

			foreach (var file in dirInfo.GetFiles())
			{
				result.AddRange(ReadAggregatedListFromFile(file.FullName));
			}

			return result;
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

			Persist(finalDataList, FilePathProvider.GetFileName(_startTimeHour));

			_startTimeHour = _startTimeHour.AddHours(1);
			File.WriteAllText(_persistPerMinFile, String.Empty);
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
				aggregatedDataList[(int)csResult[i, 0]].Frequency = csResult[i, 1];

				finalDataList.Add(aggregatedDataList[(int)csResult[i, 0]]);
			}
			return finalDataList;
		}

		private void ClassifyPerDay()
		{
			List<DataAtom> aggregatedDataList = ReadAggregatedListFromFolder();

			List<DataAtom> finalDataList = Classify(aggregatedDataList);

			Persist(finalDataList, FilePathProvider.GetFileNameDay(_startTimeDay));

			_startTimeDay = _startTimeDay.AddDays(1);
		}

		public void Dispose()
		{
			ClassifyPerHour();
			FilePathProvider.Dispose();
		}

		public FilePaths FilePathProvider { get; set; }
	}
}
