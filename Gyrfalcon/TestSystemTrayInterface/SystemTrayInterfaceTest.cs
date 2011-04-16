using DesktopClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace TestSystemTrayInterface
{
    
    
    /// <summary>
    ///This is a test class for SystemTrayInterfaceTest and is intended
    ///to contain all SystemTrayInterfaceTest Unit Tests
    ///</summary>
	[TestClass()]
	public class SystemTrayInterfaceTest
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
		///A test for SystemTrayInterface Constructor
		///</summary>
		[TestMethod()]
		public void SystemTrayInterfaceConstructorTest()
		{
			SystemTrayInterface target = new SystemTrayInterface();
		}

		[TestMethod()]
		public void SystemTrayInterfaceSaveSettingsTest()
		{
			SystemTrayInterface target = new SystemTrayInterface();

			DCSettings settings = new DCSettings();

			DCSettings oldSettings = target.GetSettings();

			target.SetSettings(settings);

			DCSettings newSettings = target.GetSettings();
			
			Assert.AreEqual(settings, newSettings, "Unable to save settings");

			// Save the old settings back. 
			// TODO Can UT avoid this thing? Mocks?
			target.SetSettings(oldSettings);

		}

		[TestMethod()]
		public void SystemTrayInterfaceSnoozeTest()
		{
			SystemTrayInterface target = new SystemTrayInterface();

			Assert.IsFalse(target.IsSnoozed, "target just launched and shouldn't be snoozed");
			TimeSpan timespan = new TimeSpan(0, 0, 5); // 5 sec
			
			bool calledback = false;

			target.Snooze(timespan, () => { calledback = true; });
			
			Assert.IsTrue(target.IsSnoozed, "Target should be snoozed now");
			Thread.Sleep(timespan);
			Assert.IsFalse(target.IsSnoozed, "target should be awake now");
			Assert.IsTrue(calledback, "Should call callback delegate");
		}

		[TestMethod()]
		public void SystemTrayInterfaceExitTest()
		{
			SystemTrayInterface target = new SystemTrayInterface();

			target.Exit();
		}

		[TestMethod()]
		public void SystemTrayInterfaceOpenWebsiteTest()
		{
			SystemTrayInterface target = new SystemTrayInterface();

			target.OpenWebsite();
		}

	}
}
