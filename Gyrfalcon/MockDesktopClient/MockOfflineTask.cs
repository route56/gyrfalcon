using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesktopClient.ClientInterface;

namespace MockDesktopClient
{
	class MockOfflineTask : IOfflineTask
	{
		public string OfflineCategoryEditUrl
		{
			get { return "http://microsoft.com"; }
		}

		public void SubmitOfflineTaskDetails(DateTime startTime, DateTime endTime, string category, string details)
		{
			return;
		}
	}
}
