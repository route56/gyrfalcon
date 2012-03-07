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
		private Timer _timer;
		private bool _isBusy;

		// TEMP TEST CODE
		public AlertManager()
		{
			_isBusy = true;
			_timer = new Timer(10000) { AutoReset = true };

			_timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
			_timer.Start();
		}

		void _timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (_isBusy)
			{
				AlertSystemIsIdle(DateTime.Now);
			}
			else
			{
				AlertSystemIsBusy(DateTime.Now);
			}

			_isBusy = !_isBusy;
		}
	}
}
