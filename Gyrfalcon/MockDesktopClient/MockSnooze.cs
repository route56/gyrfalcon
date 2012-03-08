using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesktopClient.ClientInterface;
using System.Timers;

namespace MockDesktopClient
{
	class MockSnooze : ISnooze
	{
		public bool IsSnoozed
		{
			get
			{
				return _isSnoozed;
			}
		}

		public void Sleep(TimeSpan timespan)
		{
			_isSnoozed = true;
			_timer = new Timer(10000) { AutoReset = false };

			_timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
			_timer.Start();
		}

		public void Wakeup()
		{
			_timer.Stop();
			_timer = null;
			_isSnoozed = false;
		}

		public event Action OnSnoozeCompletion;

		private void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			_isSnoozed = false;
			OnSnoozeCompletion();
		}

		private bool _isSnoozed = false;
		private Timer _timer;
	}
}
