using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Timers;

namespace DesktopClient
{
	public class SystemTrayInterface
	{
		public void OpenWebsite()
		{
			Process.Start("http://google.com");
			throw new NotImplementedException();
		}

		public void Exit()
		{
			throw new NotImplementedException();
		}

		public bool IsSnoozed { get { return _callBack != null; } }

		public DCSettings GetSettings()
		{
			throw new NotImplementedException();
		}

		public void SetSettings(DCSettings settings)
		{
			throw new NotImplementedException();
		}

		public delegate void SnoozeCompleteCallback();
		private SnoozeCompleteCallback _callBack = null;
		private Timer _timer = null;
		public void Snooze(TimeSpan timespan, SnoozeCompleteCallback callBack)
		{
			Debug.Assert(timespan.TotalMilliseconds <= Int32.MaxValue, "constraint of timer class");
			// One time timer.
			_timer = new Timer(timespan.TotalMilliseconds) { AutoReset = false };
			Debug.Assert(_callBack == null, "should not be having pending callbacks, the contract. may want to change this where snooze can be called multiple times");
			_callBack = callBack;
			// TODO snooze processing

			_timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
			//_timer.Enabled = true;
			_timer.Start();

			//throw new NotImplementedException();
		}

		void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			Debug.Assert(_callBack != null, "should assign callback before setting timer");
			_callBack();
			_callBack = null;
		}

		public bool AbortSnooze()
		{
			Debug.Assert(IsSnoozed, "Contract: abort to be called only if snooze is set");
			Debug.Assert(_timer != null, "time shouldn't be null either");

			//_timer.Enabled = false;
			_timer.Stop();
			_callBack = null;
			_timer = null;

			return true;
			//throw new NotImplementedException();
		}
	}
}
