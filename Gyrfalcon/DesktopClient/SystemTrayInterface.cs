using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopClient
{
	public class SystemTrayInterface
	{
		public void OpenWebsite()
		{
			throw new NotImplementedException();
		}

		public void Exit()
		{
			throw new NotImplementedException();
		}

		public bool IsSnoozed { get; set; }

		public DCSettings GetSettings()
		{
			throw new NotImplementedException();
		}

		public void SetSettings(DCSettings settings)
		{
			throw new NotImplementedException();
		}

		public delegate void SnoozeCompleteCallback();
		public void Snooze(TimeSpan timespan, SnoozeCompleteCallback callBack)
		{
			throw new NotImplementedException();
		}
	}
}
