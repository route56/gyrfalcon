using ProcessMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading;

namespace TestSystemTrayInterface
{
    
    
    /// <summary>
    ///This is a test class for CurrentProcessTest and is intended
    ///to contain all CurrentProcessTest Unit Tests
    ///</summary>
	[TestClass()]
	public class CurrentProcessTest
	{


		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


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
