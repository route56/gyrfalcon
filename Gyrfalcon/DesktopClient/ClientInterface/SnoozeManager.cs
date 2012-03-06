using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Timers;

namespace DesktopClient.ClientInterface
{
	public class SnoozeManager : ISnooze
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
			Debug.Assert(timespan.TotalMilliseconds <= Int32.MaxValue, "constraint of timer class");

			// TODO worry about thread safety. Worry about same method called to snooze for different duration.
			_isSnoozed = true;

			// Call system's process monitor to stop monitor
			// One time timer.
			_timer = new Timer(timespan.TotalMilliseconds) { AutoReset = false };

			_timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
			_timer.Start();
		}

		public void Wakeup()
		{
			Debug.Assert(_timer != null, "time shouldn't be null either");

			// TODO Call system's process monitor to start monitor
			_timer.Stop();
			_timer = null;
			_isSnoozed = false;
		}

		public event Action OnSnoozeCompletion;

		private void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			_isSnoozed = false;
			// TODO Call system's process monitor to start monitor
			OnSnoozeCompletion();
		}

		private bool _isSnoozed = false;
		private Timer _timer;
	}
}
