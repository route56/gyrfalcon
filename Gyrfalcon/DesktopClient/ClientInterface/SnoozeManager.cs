using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Timers;
using DesktopClient.ProcessMonitor;

namespace DesktopClient.ClientInterface
{
	public class SnoozeManager : ISnooze
	{
		public SnoozeManager(ISleep target)
		{
			_target = target;
		}

		public bool IsSnoozed
		{
			get
			{
				return _target.IsSleeping;
			}
		}

		public void Sleep(TimeSpan timespan)
		{
			lock (_sync)
			{
				_target.Sleep();

				if (_target.IsSleeping == false)
				{
					throw new Exception("ISleep target not sleeping");
				}

				// One time timer.
				_timer = new Timer(timespan.TotalMilliseconds) { AutoReset = false };
				_timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
				_timer.Start();
			}
		}

		public void Wakeup()
		{
			lock (_sync)
			{
				_target.WakeUp();
				if (_target.IsSleeping)
				{
					throw new Exception("ISleep target not waking up");
				}

				if (_timer != null)
				{
					_timer.Stop();
					_timer.Dispose();
					_timer = null;
				} 
			}
		}

		public event Action OnSnoozeCompletion;

		private void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			lock (_sync)
			{
				Wakeup();
				OnSnoozeCompletion(); 
			}
		}

		private Timer _timer;
		private ISleep _target;
		private object _sync = new object();
	}
}
