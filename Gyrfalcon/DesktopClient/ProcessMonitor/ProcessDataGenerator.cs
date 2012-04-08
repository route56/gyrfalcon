using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using DataStore;
using System.Collections.Concurrent;
using DesktopClient.ClientInterface;

namespace DesktopClient.ProcessMonitor
{
	public class ProcessDataGenerator : IDisposable
	{
		private IWriteStore dataStore = new WriteStore();
		private ConcurrentQueue<DataAtom> _queue;
		private StatusManager _statusManager;

		public ProcessDataGenerator(ConcurrentQueue<DataAtom> _queue, StatusManager status)
		{
			this._queue = _queue;
			_statusManager = status;
		}

		internal void Analyze()
		{
			List<DataAtom> dataList = new List<DataAtom>();

			int length = _queue.Count;

			for (int i = 0; i < length; i++)
			{
				DataAtom md;
				if (_queue.TryDequeue(out md))
				{
					dataList.Add(md);
				}
			}

			Aggregate(dataList);
		}

		public void Aggregate(List<DataAtom> dataList)
		{
			Aggregator agg = new Aggregator();

			for (int i = 0; i < dataList.Count; i++)
			{
				agg.Add(dataList[i].Process, dataList[i].Title);
			}

			var aggRes = agg.GetAggregationResult();

			List<DataAtom> aggregatedDataList = new List<DataAtom>();

			for (int i = 0; i < aggRes.GetLength(0); i++)
			{
				dataList[aggRes[i, 0]].Frequency = aggRes[i, 1];

				aggregatedDataList.Add(dataList[aggRes[i, 0]]);
			}

			dataStore.AddToAggregatedStore(aggregatedDataList);
			_statusManager.LastSuccessfulTransmission = DateTime.Now;
		}

		public void Dispose()
		{
			dataStore.Dispose();
		}
	}
}
