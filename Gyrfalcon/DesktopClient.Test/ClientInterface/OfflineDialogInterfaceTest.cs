using DesktopClient.ClientInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DesktopClient.Test.ClientInterface
{
	[TestClass()]
	public class OfflineDialogInterfaceTest
	{
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

		[TestMethod()]
		[DeploymentItem("DesktopClient.dll")]
		public void OfflineDialogInterfaceConfigTest()
		{
			//<add key="button1" value="test1"/>
			//<add key="button2" value="test2"/>
			//<add key="button3" value="test3"/>
			//<add key="button4" value="test4"/>
			//<add key="button5" value="test5"/>
			//<add key="button6" value="test6"/>
			OfflineDialogInterface target = new OfflineDialogInterface();

			string[] expected = { "test1",
								  "test2",
								  "test3",
								  "test4",
								  "test5",
								  "test6" };

			string[] actual = target.GetButtonText().ToArray();

			CollectionAssert.AreEqual(expected, actual, "should read from test app.config");
		}
	}
}
