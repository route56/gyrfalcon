using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProcessMonitor;
using Collection;

namespace DataSender
{
	public class ClientQueueConsumer
	{
		private SynchronizedQueue<ProcessData> queue;

		public ClientQueueConsumer(SynchronizedQueue<ProcessData> queue)
		{
			this.queue = queue;
		}

		public IEnumerable<ProcessData> Consume()
		{
			List<ProcessData> results = new List<ProcessData>();

			try
			{
				while (CanConsumeMore())
				{
					results.Add(queue.Dequeue());
				}
			}
			catch (InvalidOperationException)
			{
				// Ignore
			}

			return results;
		}

		public bool CanConsumeMore()
		{
			return queue.Count > 0;
		}
	}
}
