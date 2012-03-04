using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesktopClient.ProcessMonitor;
using System.Collections.Concurrent;

namespace DesktopClient.DataSender
{
	public class ClientQueueConsumer
	{
		private ConcurrentQueue<ProcessData> queue;

		public ClientQueueConsumer(ConcurrentQueue<ProcessData> queue)
		{
			this.queue = queue;
		}

		public IEnumerable<ProcessData> Consume()
		{
			List<ProcessData> results = new List<ProcessData>();

			ProcessData data;

			while (queue.TryDequeue(out data))
			{
				results.Add(data);
			}

			return results;
		}

		public bool CanConsumeMore()
		{
			return !queue.IsEmpty;
		}
	}
}
