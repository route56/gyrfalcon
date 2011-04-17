using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessMonitor
{
	public class ClientQueueProducer
	{
		private Queue<ProcessData> queue;

		public ClientQueueProducer(Queue<ProcessData> queue)
		{
			// TODO: Complete member initialization
			this.queue = queue;
		}

		public void Add(ProcessData processData)
		{
			throw new NotImplementedException();
		}
	}
}
