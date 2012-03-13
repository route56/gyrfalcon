using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DesktopClient.ProcessMonitor;
using DesktopClient.SystemServices;

namespace DesktopClient.ManualTest
{
	class FirstFly
	{
		public void AggregatorClassifier()
		{
			List<MainData> dataList = new List<MainData>();

			using (StreamReader sr = File.OpenText("Copy of DCManualLog.txt"))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					string[] read = line.Split(',');
					dataList.Add(new MainData()
					{
						Time = DateTime.Parse(read[0]),
						Process = read[1],
						Title = read[2],
						Frequency = 1
					}); // This can hit OOM if file is HUGE
				}
			}

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

			using (StreamWriter sw = File.AppendText("ClassifiedAnalysis.txt"))
			{
				foreach (var item in finalDataList)
				{
					sw.WriteLine(string.Format("{0},{1},{2},{3}", item.Time, item.Process, item.Title, item.Frequency));
				}
			}
		}

		public void DataCollector()
		{
			while (true)
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendFormat("{0}", DateTime.Now);

				var process = CurrentProcess.GetActiveWindowProcess();

				sb.AppendFormat(",{0}", process.ProcessName);
				sb.AppendFormat(",{0}", process.MainWindowTitle);

				Console.WriteLine(sb.ToString());

				using (StreamWriter sw = File.AppendText("DCManualLog.txt"))
				{
					sw.WriteLine(sb.ToString());
				}

				System.Threading.Thread.Sleep(1000);
			}
		}
	}
}
