using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesktopClient.ClientInterface;
using System.Diagnostics;

namespace MockDesktopClient
{
	class MockDashboard : IDashboard
	{
		public void LaunchDashboard()
		{
			Process.Start("https://google.com");
		}
	}
}
