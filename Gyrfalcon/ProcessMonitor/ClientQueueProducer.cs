using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collection;

namespace ProcessMonitor
{
	public class ClientQueueProducer
	{
		private SynchronizedQueue<ProcessData> queue;

		public ClientQueueProducer(SynchronizedQueue<ProcessData> queue)
		{
			this.queue = queue;
		}

		public void Add(ProcessData processData)
		{
			queue.Enqueue(processData);
		}
	}
}
