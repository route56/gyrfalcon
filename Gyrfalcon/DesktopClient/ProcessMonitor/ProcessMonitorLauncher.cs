using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using DataStore;
using System.Timers;
using DesktopClient.SystemServices;
using DesktopClient.ClientInterface;

namespace DesktopClient.ProcessMonitor
{
	public class ProcessMonitorLauncher
	{
		private System.Threading.Thread _thread;
		private bool _stopRequested = false;
		private ProcessDataGenerator _analyzer;
		private Timer _timer;
		private StatusManager _statusManager;

		public void Start(StatusManager status)
		{
			_statusManager = status;
			_thread = new System.Threading.Thread( () => ThreadCode());

			_thread.Start();
		}

		public void Stop()
		{
			_stopRequested = true;
			_thread.Join();
		}

		private void ThreadCode()
		{
			ConcurrentQueue<DataAtom> queue = new ConcurrentQueue<DataAtom>();

			_analyzer = new ProcessDataGenerator(queue, _statusManager);

			_timer = new Timer()
			{
				Enabled = true,
				AutoReset = true,
				Interval = 60 * 1000
			};

			_timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

			Listener(queue);
		}

		void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			_analyzer.Analyze();
		}

		private void Listener(ConcurrentQueue<DataAtom> queue)
		{
			while (!_stopRequested)
			{
				DataAtom md = new DataAtom();
				md.Time = DateTime.Now;

				var process = CurrentProcess.GetActiveWindowProcess();

				md.Process = process.ProcessName;
				md.Title = process.MainWindowTitle;

				queue.Enqueue(md);

				System.Threading.Thread.Sleep(1000);
			}

			_timer.Dispose();
			_analyzer.Dispose();
		}
	}
}
