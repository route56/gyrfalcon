using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace DesktopClient.ClientInterface
{
	class DashboardManager : IDashboard
	{
		public void LaunchDashboard()
		{
			try
			{
				Process.Start("ReportApp.exe");
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}
	}
}
