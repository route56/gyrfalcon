using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

namespace DesktopClient.ProcessMonitor
{
	public class ClientQueueProducer
	{
		private ConcurrentQueue<ProcessData> queue;

		public ClientQueueProducer(ConcurrentQueue<ProcessData> queue)
		{
			this.queue = queue;
		}

		public void Add(ProcessData processData)
		{
			queue.Enqueue(processData);
		}
	}
}
