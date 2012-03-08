using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesktopClient.ClientInterface;
using System.Timers;

namespace MockDesktopClient
{
	class MockAlert : IAlert
	{
		public event Action<string> AlertMessenger;

		public event Action<DateTime> AlertSystemIsIdle;

		public event Action<DateTime> AlertSystemIsBusy;
		private Timer _timer;
		private bool _isBusy;

		public MockAlert()
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
				if(AlertSystemIsIdle != null)
					AlertSystemIsIdle(DateTime.Now);
			}
			else
			{
				if(AlertSystemIsBusy != null)
					AlertSystemIsBusy(DateTime.Now);
			}

			if (AlertMessenger != null && new Random().Next() % 2 == 0)
			{
				AlertMessenger("Mock message");
			}

			_isBusy = !_isBusy;
		}
	}
}
