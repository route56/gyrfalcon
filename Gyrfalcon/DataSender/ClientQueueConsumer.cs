using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProcessMonitor;

namespace DataSender
{
	public class ClientQueueConsumer
	{
		private Queue<ProcessData> queue;

		public ClientQueueConsumer(Queue<ProcessData> queue)
		{
			// TODO: Complete member initialization
			this.queue = queue;
		}

		public int QueueCount { get; set; }

		public IEnumerable<ProcessData> Consume()
		{
			throw new NotImplementedException();
		}
	}
}
