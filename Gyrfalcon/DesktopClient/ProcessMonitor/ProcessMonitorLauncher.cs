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
	public class ProcessMonitorLauncher : ISleep
	{
		private System.Threading.Thread _thread;
		private bool _stopRequested = false;
		private ProcessDataGenerator _analyzer;
		private Timer _timer;
		private StatusManager _statusManager;
		private ICurrentProcess _currentProcess;
		private bool _isSleeping = false;
		private bool _sleepRequested = false;
		private System.Threading.AutoResetEvent _event = new System.Threading.AutoResetEvent(false);
		private bool _waiting;

		public void Start(ICurrentProcess currProcess, StatusManager status)
		{
			_currentProcess = currProcess;
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
				AnyPendingSignal();

				if (_isSleeping == false)
				{
					DataAtom md = new DataAtom();
					md.Time = DateTime.Now;

					var process = _currentProcess.GetActiveWindowProcess();

					md.Process = process.ProcessName;
					md.Title = process.MainWindowTitle;

					queue.Enqueue(md);
				}

				System.Threading.Thread.Sleep(1000);
			}

			_timer.Dispose();
			_analyzer.Dispose();
		}

		// Called in timer thread
		private void AnyPendingSignal()
		{
			if (_waiting)
			{
				_isSleeping = _sleepRequested;
				_waiting = false;
				_event.Set();
			}
		}

		public void Sleep()
		{
			_sleepRequested = true;
			_waiting = true;
			_event.WaitOne();
		}

		public void WakeUp()
		{
			_sleepRequested = false;
			_waiting = true;
			_event.WaitOne();
		}

		public bool IsSleeping
		{
			get { return _isSleeping; }
		}
	}
}
