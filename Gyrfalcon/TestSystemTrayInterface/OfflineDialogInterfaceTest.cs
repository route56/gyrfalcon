using DesktopClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestSystemTrayInterface
{
    
    
    /// <summary>
    ///This is a test class for OfflineDialogInterfaceTest and is intended
    ///to contain all OfflineDialogInterfaceTest Unit Tests
    ///</summary>
	[TestClass()]
	public class OfflineDialogInterfaceTest
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


		[TestMethod()]
		[DeploymentItem("DesktopClient.dll")]
		public void OfflineDialogInterfaceSubmitTest()
		{
			OfflineDialogInterfaceSubmitFor(new OfflineDialogInterface());
		}

		private void OfflineDialogInterfaceSubmitFor(OfflineDialogInterface target)
		{
			string name = "Help";
			string comment = "Some guy";
			DateTime st = DateTime.Now.AddMinutes(-6);
			DateTime ed = DateTime.Now;

			target.Submit(name, comment, st, ed);

			Assert.AreEqual(target.ProcessData.StartTime, st, "Should be same objects");
			Assert.AreEqual(target.ProcessData.EndTime, ed, "Should be same objects");
			Assert.AreEqual(target.ProcessData.Name, name, "Should be same objects");
			CollectionAssert.AreEqual(target.ProcessData.TitleList, new string[] { comment }, "Should be same objects");
		}

		[TestMethod()]
		[DeploymentItem("DesktopClient.dll")]
		public void OfflineDialogInterfaceDelegateCallbackTest()
		{
			OfflineDialogInterface target = new OfflineDialogInterface();

			bool calledBack = false;

			target.OnSubmit = (processData) => { calledBack = true; };

			OfflineDialogInterfaceSubmitFor(target);

			Assert.IsTrue(calledBack, "Submit should call back on submit");
		}
	}
}
