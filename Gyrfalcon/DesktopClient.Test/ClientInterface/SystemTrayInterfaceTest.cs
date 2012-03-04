using DesktopClient.ClientInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Collections.Generic;

namespace DesktopClient.Test.ClientInterface
{
	[TestClass()]
	public class SystemTrayInterfaceTest
	{
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
			//SystemTrayInterface target = new SystemTrayInterface();

			//DCSettings settings = new DCSettings();

			//DCSettings oldSettings = target.GetSettings();

			//target.SetSettings(settings);

			//DCSettings newSettings = target.GetSettings();
			
			//Assert.AreEqual(settings, newSettings, "Unable to save settings");

			//// Save the old settings back. 
			//// TODO Can UT avoid this thing? Mocks?
			//target.SetSettings(oldSettings);
			Assert.Inconclusive("This feature isn't implemented yet");
		}

		[TestMethod()]
		public void SnoozeForTest_CallbackShouldWork()
		{
			SystemTrayInterface target = new SystemTrayInterface();
			List<IToolStripMenuItem> stubMenuItems = new List<IToolStripMenuItem>(new FakeIToolStripMenuItem[] 
				{
					new FakeIToolStripMenuItem() { IsSnoozed = false, Duration = new TimeSpan(0, 0, 2) }
				});

			Assert.IsFalse(target.IsSnoozed, "target just launched and shouldn't be snoozed");
			target.ToolStripMenuItems = stubMenuItems;
			Assert.IsFalse(target.IsSnoozed, "target just launched and shouldn't be snoozed");

			bool calledback = false;

			target.OnSnoozeCompleteCallback = () => { calledback = true; };

			target.SnoozeFor(stubMenuItems[0]);

			Assert.IsTrue(target.IsSnoozed, "Target should be snoozed now");
			Thread.Sleep(TimeSpan.FromTicks(stubMenuItems[0].Duration.Ticks * 2));
			Assert.IsFalse(target.IsSnoozed, "target should be awake now");
			Assert.IsTrue(calledback, "Should call callback delegate");
			calledback = false;
			Thread.Sleep(stubMenuItems[0].Duration);
			Assert.IsFalse(calledback, "Should not call callback delegate after called once");
		}

		[TestMethod()]
		public void SnoozeForTest_CallbackShouldntBeCalledOnAbort()
		{
			SystemTrayInterface target = new SystemTrayInterface();
			List<IToolStripMenuItem> stubMenuItems = new List<IToolStripMenuItem>(new FakeIToolStripMenuItem[] 
				{
					new FakeIToolStripMenuItem() { IsSnoozed = false, Duration = new TimeSpan(0, 0, 2) }
				});

			Assert.IsFalse(target.IsSnoozed, "target just launched and shouldn't be snoozed");
			target.ToolStripMenuItems = stubMenuItems;
			Assert.IsFalse(target.IsSnoozed, "target just launched and shouldn't be snoozed");

			bool calledback = false;

			target.OnSnoozeCompleteCallback = () => { calledback = true; };

			bool snoozed = target.SnoozeFor(stubMenuItems[0]);

			Assert.IsTrue(target.IsSnoozed, "Target should be snoozed now");

			snoozed = target.SnoozeFor(stubMenuItems[0]); // this should abort snooze

			Assert.IsFalse(snoozed, "should be able to abort snooze");
			Assert.IsFalse(target.IsSnoozed, "Target should be awake");

			// Lets wait and see if callback isn't called
			Thread.Sleep(TimeSpan.FromTicks(stubMenuItems[0].Duration.Ticks * 2));
			Assert.IsFalse(calledback, "Shouldn't call callback delegate because snooze was aborted");
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


		/// <summary>
		///A test for SnoozeFor
		///</summary>
		[TestMethod()]
		public void SnoozeForTest_MutualExclusiveness()
		{
			SystemTrayInterface target = new SystemTrayInterface();
			List<IToolStripMenuItem> stubMenuItems = new List<IToolStripMenuItem>();

			IToolStripMenuItem mockOne = new FakeIToolStripMenuItem() { IsSnoozed = false, Duration = new TimeSpan(0, 0, 10) };
			IToolStripMenuItem mockTwo = new FakeIToolStripMenuItem() { IsSnoozed = false, Duration = new TimeSpan(0, 0, 20) };
			IToolStripMenuItem mockThree = new FakeIToolStripMenuItem() { IsSnoozed = false, Duration = new TimeSpan(0, 0, 30) };

			stubMenuItems.Add(mockOne);
			stubMenuItems.Add(mockTwo);
			stubMenuItems.Add(mockThree);

			target.ToolStripMenuItems = stubMenuItems;

			target.SnoozeFor(mockOne);
			target.SnoozeFor(mockTwo);

			Assert.IsFalse(mockOne.IsSnoozed, "After snooze for two, one should be not checked");
			Assert.IsTrue(mockTwo.IsSnoozed, "Two should be snoozed");

			target.SnoozeFor(mockThree);
			Assert.IsFalse(mockOne.IsSnoozed, "Three is set.");
			Assert.IsFalse(mockTwo.IsSnoozed, "Three is set");
			Assert.IsTrue(mockThree.IsSnoozed, "Three is set");

			target.SnoozeFor(mockOne);
			Assert.IsFalse(mockThree.IsSnoozed, "One is set.");
			Assert.IsFalse(mockTwo.IsSnoozed, "One is set");
			Assert.IsTrue(mockOne.IsSnoozed, "One is set");
		}

		[TestMethod()]
		public void SnoozeForTest_CorrectReturnValue()
		{
			SystemTrayInterface target = new SystemTrayInterface();
			List<IToolStripMenuItem> stubMenuItems = new List<IToolStripMenuItem>();

			IToolStripMenuItem stubOne = new FakeIToolStripMenuItem() { IsSnoozed = false, Duration = new TimeSpan(0, 0, 10) };
			IToolStripMenuItem stubTwo = new FakeIToolStripMenuItem() { IsSnoozed = false, Duration = new TimeSpan(0, 0, 20) };
			IToolStripMenuItem stubThree = new FakeIToolStripMenuItem() { IsSnoozed = false, Duration = new TimeSpan(0, 0, 30) };

			stubMenuItems.Add(stubOne);
			stubMenuItems.Add(stubTwo);
			stubMenuItems.Add(stubThree);

			target.ToolStripMenuItems = stubMenuItems;

			Assert.IsTrue(target.SnoozeFor(stubOne), "One is snoozed");
			Assert.IsFalse(target.SnoozeFor(stubOne), "One is awake");

			Assert.IsTrue(target.SnoozeFor(stubTwo), "Two is snoozed");
			Assert.IsTrue(target.SnoozeFor(stubThree), "Three is snoozed");
			Assert.IsTrue(target.SnoozeFor(stubTwo), "Two is again snoozed");
			Assert.IsFalse(target.SnoozeFor(stubTwo), "Two is awake");
		}

		[TestMethod()]
		[ExpectedException(typeof(InvalidOperationException))]
		public void SnoozeForTest_ExceptionThrownForMissingMenuItem()
		{
			SystemTrayInterface target = new SystemTrayInterface();
			List<IToolStripMenuItem> stubMenuItems = new List<IToolStripMenuItem>();

			IToolStripMenuItem stubOne = new FakeIToolStripMenuItem() { IsSnoozed = false, Duration = new TimeSpan(0, 0, 10) };
			IToolStripMenuItem stubTwo = new FakeIToolStripMenuItem() { IsSnoozed = false, Duration = new TimeSpan(0, 0, 20) };

			stubMenuItems.Add(stubOne);

			target.ToolStripMenuItems = stubMenuItems;

			target.SnoozeFor(stubTwo);
		}

		[TestMethod()]
		[ExpectedException(typeof(InvalidOperationException))]
		public void SnoozeForTest_ExceptionThrownForMissingMenuItemList()
		{
			SystemTrayInterface target = new SystemTrayInterface();

			IToolStripMenuItem stubOne = new FakeIToolStripMenuItem() { IsSnoozed = false, Duration = new TimeSpan(0, 0, 10) };

			target.SnoozeFor(stubOne);
		}

		class FakeIToolStripMenuItem : IToolStripMenuItem
		{
			public bool IsSnoozed { get; set; }
			public TimeSpan Duration { get; set; }
		}
	}
}
