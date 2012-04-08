using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Timers;

namespace DataStore
{
	public class WriteStore : IWriteStore
	{
		private Timer _timer;

		private string _persistPerMinFile;
		private FilePaths _filePathProvider = null;

		public WriteStore()
		{
			_timer = new Timer()
				{
					AutoReset = true,
					Enabled = true,
					Interval = new TimeSpan(1, 0, 0).TotalMilliseconds
				};
			_timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
			_timer.Start();

			FilePathProvider = new FilePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
		}

		void _timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			ClassifyPerHour();
		}

		public void AddToAggregatedStore(IEnumerable<DataAtom> dataList)
		{
			lock (_persistPerMinFile)
			{
				using (StreamWriter sw = File.AppendText(_persistPerMinFile))
				{
					foreach (var item in dataList)
					{
						sw.WriteLine(item.ToString());
					}
				}
			}
		}

		private void ClassifyPerHour()
		{
			lock (_persistPerMinFile)
			{
				List<DataAtom> aggregatedDataList = ReadAggregatedListFromFile(_persistPerMinFile);
				List<DataAtom> finalDataList = Classify(aggregatedDataList);

				PersistPerHour(finalDataList);

				File.WriteAllText(_persistPerMinFile, String.Empty);
			}
		}

		private List<DataAtom> ReadAggregatedListFromFile(string fileName)
		{
			List<DataAtom> result = new List<DataAtom>();

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

		//private void ClassifyPerDay()
		//{
		//    if (_min.HasValue && _max.HasValue)
		//    {
		//        for (DateTime i = _min.Value; i < _max.Value.AddDays(1); i = i.AddDays(1))
		//        {
		//            List<DataAtom> aggregatedDataList = ReadAggregatedListFromFolder();
		//            List<DataAtom> finalDataList = Classify(aggregatedDataList);
		//            // append or load and append.
		//            // ClassifyPerWeek
		//            PersistPerDay(finalDataList);
		//        }

		//        _min = _max = null;
		//    }
		//}

		private List<DataAtom> Classify(List<DataAtom> aggregatedDataList)
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

		private void PersistPerHour(IEnumerable<DataAtom> finalDataList)
		{
			var start = finalDataList.Min(s => s.Time);
			var end = finalDataList.Min(s => s.Time);

			for (DateTime i = start; i < end.AddHours(1); i = i.AddHours(1))
			{
				FilePathProvider.EnsureFileWithFolder(FilePathProvider.GetHourSummary(i));
				PersistToFile(
					finalDataList.Where(s => s.Time >= i && s.Time < i.AddHours(1)),
					FilePathProvider.GetHourSummary(i));
			}
		}

		private void PersistPerDay(IEnumerable<DataAtom> finalDataList)
		{
			var start = finalDataList.Min(s => s.Time);
			var end = finalDataList.Min(s => s.Time);

			for (DateTime i = start; i < end.AddDays(1); i = i.AddDays(1))
			{
				FilePathProvider.EnsureFileWithFolder(FilePathProvider.GetDaySummary(i));
				PersistToFile(
					finalDataList.Where(s => s.Time >= i && s.Time < i.AddDays(1)),
					FilePathProvider.GetDaySummary(i));
			}
		}

		private void PersistToFile(IEnumerable<DataAtom> finalDataList, string fileName)
		{
			using (StreamWriter sw = File.AppendText(fileName))
			{
				foreach (var item in finalDataList)
				{
					sw.WriteLine(item.ToString());
				}
			}
		}

		public void Dispose()
		{
			_timer.Dispose();
			ClassifyPerHour();
			FilePathProvider.Dispose();
		}

		public FilePaths FilePathProvider 
		{
			get { return _filePathProvider; }
			set
			{
				if (_filePathProvider != null)
				{
					_filePathProvider.Dispose();
				}

				_filePathProvider = value;
				_persistPerMinFile = FilePathProvider.GetTempFile();
			}
		}
	}
}
