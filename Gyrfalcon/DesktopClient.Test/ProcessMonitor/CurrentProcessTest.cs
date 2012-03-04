using DesktopClient.ProcessMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading;

namespace DesktopClient.Test.ProcessMonitor
{
	[TestClass()]
	public class CurrentProcessTest
	{
		/// <summary>
		///A test for CurrentProcess Constructor
		///</summary>
		[TestMethod()]
		public void CurrentProcessConstructorTest()
		{
			CurrentProcess target = new CurrentProcess();
			int count = 10;
			while (count > 0)
			{
				target.GetActiveWindow();
				Thread.Sleep(1000);
				count--;
			}
		}

		//[TestMethod()]
		//public void CurrentProcessConstructorTest()
		//{
		//    CurrentProcess target = new CurrentProcess();

		//    // TODO launch some process and set in focus.
		//    ProcessInfo info = target.GetCurrentProcess();
		//    // verify the process ids are same.
		//}

		//[TestMethod()]
		//public void CurrentProcessConstructorTest()
		//{
		//    CurrentProcess target = new CurrentProcess();

			
		//    target.NotifyOnCurrentProcessChange(EventHandler<> handler);

		//    // TODO launch some process and set in focus.
		//    // verify the event handler was called
		//    // launch some process and set in focus.
		//    // verify the event handler was called
		//}
	}
}
