using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Timers;
using DesktopClient.SystemServices;
using DesktopClient.ProcessMonitor;
using System.IO;

namespace DesktopClient.ManualTest
{
	class CoreIdea
	{
		ConcurrentQueue<MainData> _queue = new ConcurrentQueue<MainData>();
		private DateTime _startTimeDay;
		private DateTime _startTimeHour;
		private string _persistPerMinFile = @"PersistPerMinute.txt";

		internal void Start()
		{
			Timer timer = new Timer()
			{
				Enabled = true,
				AutoReset = true,
				Interval = 60 * 1000
			};

			timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

			_startTimeHour = _startTimeDay = DateTime.Now;

			Listener(_queue);
		}

		void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			Analyzer(_queue);
		}

		private void Listener(ConcurrentQueue<MainData> queue)
		{
			while (true)
			{
				MainData md = new MainData();
				md.Time = DateTime.Now;

				var process = CurrentProcess.GetActiveWindowProcess();

				md.Process = process.ProcessName;
				md.Title = process.MainWindowTitle;

				queue.Enqueue(md);

				System.Threading.Thread.Sleep(1000);
			}
		}

		private void Analyzer(ConcurrentQueue<MainData> queue)
		{
			List<MainData> dataList = new List<MainData>();

			int length = queue.Count;

			for (int i = 0; i < length; i++)
			{
				MainData md;
				if (queue.TryDequeue(out md))
				{
					dataList.Add(md);
				}
			}

			Aggregate(dataList);

			if (IsOneHourDone())
			{
				ClassifyPerHour();
			}

			if (IsOneDayDone())
			{
				ClassifyPerDay();
			}

			// saving in tab seprated format
			// reporting 
			// categorizing and ranked CSV

			// console -> per hour log file.
			// console -> per day log file.
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

		private void Aggregate(List<MainData> dataList)
		{
			Aggregator agg = new Aggregator();

			for (int i = 0; i < dataList.Count; i++)
			{
				agg.Add(dataList[i].Process, dataList[i].Title);
			}

			var aggRes = agg.GetAggregationResult();

			List<MainData> aggregatedDataList = new List<MainData>();

			for (int i = 0; i < aggRes.GetLength(0); i++)
			{
				dataList[aggRes[i, 0]].Frequency = aggRes[i, 1];

				aggregatedDataList.Add(dataList[aggRes[i, 0]]);
			}

			using (StreamWriter sw = File.AppendText(_persistPerMinFile))
			{
				foreach (var item in aggregatedDataList)
				{
					sw.WriteLine(item.ToString());
				}
			}
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

		private void ClassifyPerHour()
		{
			List<MainData> aggregatedDataList = ReadAggregatedListFromFile();

			Classifier cs = new Classifier();

			for (int i = 0; i < aggregatedDataList.Count; i++)
			{
				cs.Add(i, aggregatedDataList[i].Process + aggregatedDataList[i].Title, aggregatedDataList[i].Frequency);
			}

			var csResult = cs.GetClassificationResult();

			List<MainData> finalDataList = new List<MainData>();

			for (int i = 0; i < csResult.GetLength(0); i++)
			{
				aggregatedDataList[csResult[i, 0]].Frequency = csResult[i, 1];

				finalDataList.Add(aggregatedDataList[csResult[i, 0]]);
			}

			PersistPerHour(finalDataList);
		}

		private List<MainData> ReadAggregatedListFromFile()
		{
			List<MainData> result = new List<MainData>();

			// TODO. This needs to be done for strict 1 hour. Sleep/ Cancel etc will abrupt this.
			using (StreamReader sr = File.OpenText(_persistPerMinFile))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					result.Add(MainData.FromString(line));
				}
			}

			return result;
		}

		private void PersistPerHour(List<MainData> finalDataList)
		{
			using (StreamWriter sw = File.CreateText(GetFileName(_startTimeHour)))
			{
				foreach (var item in finalDataList)
				{
					sw.WriteLine(item.ToString());
				}
			}

			_startTimeHour = _startTimeHour.AddHours(1);
			File.Delete(_persistPerMinFile);
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

		// TODO fix DRY voilation!!!
		private void ClassifyPerDay()
		{
			List<MainData> aggregatedDataList = ReadAggregatedListFromFolder();

			Classifier cs = new Classifier();

			for (int i = 0; i < aggregatedDataList.Count; i++)
			{
				cs.Add(i, aggregatedDataList[i].Process + aggregatedDataList[i].Title, aggregatedDataList[i].Frequency);
			}

			var csResult = cs.GetClassificationResult();

			List<MainData> finalDataList = new List<MainData>();

			for (int i = 0; i < csResult.GetLength(0); i++)
			{
				aggregatedDataList[csResult[i, 0]].Frequency = csResult[i, 1];

				finalDataList.Add(aggregatedDataList[csResult[i, 0]]);
			}

			PersistPerDay(finalDataList);
		}

		private List<MainData> ReadAggregatedListFromFolder()
		{
			List<MainData> result = new List<MainData>();

			DirectoryInfo dirInfo = new DirectoryInfo(GetDirName(_startTimeDay));

			foreach (var file in dirInfo.GetFiles())
			{
				using (StreamReader sr = File.OpenText(file.FullName))
				{
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						result.Add(MainData.FromString(line));
					}
				}
			}

			return result;
		}

		private void PersistPerDay(List<MainData> finalDataList)
		{
			using (StreamWriter sw = File.CreateText(GetFileNameDay(_startTimeDay)))
			{
				foreach (var item in finalDataList)
				{
					sw.WriteLine(item.ToString());
				}
			}

			_startTimeDay = _startTimeDay.AddDays(1);
		}

		private string GetFileNameDay(DateTime time)
		{
			string dirName = GetDirName(time);

			return Path.Combine(dirName, "DaySummary.txt");
		}
	}
}
