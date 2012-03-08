using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesktopClient.ClientInterface;
using System.Timers;

namespace MockDesktopClient
{
	class MockGetFocused : IGetFocused
	{
		public void GetFocused(DateTime startTime, DateTime endTime)
		{
			Timer tm = new Timer(endTime.Subtract(startTime).Milliseconds)
				{
					Enabled = true,
					AutoReset = false
				};

			tm.Elapsed += new ElapsedEventHandler(tm_Elapsed);
			tm.Start();
		}

		void tm_Elapsed(object sender, ElapsedEventArgs e)
		{
			OnFocusTimeCompletion();
		}

		public event Action OnFocusTimeCompletion;
	}
}
