using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesktopClient.ClientInterface;
using System.Timers;

namespace MockDesktopClient
{
	class MockStatus : IStatus
	{
		public MockStatus()
		{
			_timer = new Timer(3000) { AutoReset = true, Enabled = true };

			_timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
			_timer.Start();
		}

		void _timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			_isOk = !_isOk;
			_time = DateTime.Now;

			if (StatusChanged != null)
			{
				StatusChanged();
			}
		}

		public bool IsEverythingOk
		{
			get { return _isOk; }
		}

		public DateTime LastSuccessfulTransmission
		{
			get { return _time; }
		}

		public string ClientVersion
		{
			get { return "0.1.0.0"; }
		}

		public string VersionHistoryURL
		{
			get { return "http://news.google.com"; }
		}

		public event Action StatusChanged;
		private Timer _timer;
		private bool _isOk;
		private DateTime _time;
	}
}
