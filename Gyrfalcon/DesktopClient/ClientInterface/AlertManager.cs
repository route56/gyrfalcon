using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace DesktopClient.ClientInterface
{
	class AlertManager : IAlert
	{
		public event Action<string> AlertMessenger;

		public event Action<DateTime> AlertSystemIsIdle;

		public event Action<DateTime> AlertSystemIsBusy;

	}
}
