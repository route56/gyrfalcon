using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesktopClient.ClientInterface;
using System.Threading;
using System.Diagnostics;

namespace DesktopClient.Test.ClientInterface
{
	[TestClass]
	public class BasicScenarioTest
	{
		/// <summary>
		/// Integration test. 
		/// </summary>
		[TestMethod]
		public void ClientManager_LaunchProcess_DataGetRecorded()
		{
			ClientManager target = new ClientManager();
			//Thread.Sleep(1000);
			Process.Start("cmd.exe");
			//Thread.Sleep(1000);
			//target.Dashboard.LaunchDashboard();
		}
	}
}
