using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Collections.Generic;
using DesktopClient.ClientInterface;
using DesktopClient.ProcessMonitor;

namespace DesktopClient.Test.ClientInterface
{
	class StubSleep : ISleep
	{
		private bool _sleep = false;

		public void Sleep()
		{
			_sleep = true;
		}

		public void WakeUp()
		{
			_sleep = false;
		}

		public bool IsSleeping
		{
			get { return _sleep; }
		}
	}

	[TestClass()]
	public class SnoozeManagerTest
	{
		/*
		 * 3 Test define. How to test snoozing is working. Stubs/Mocks. Independent of FS
		 * 4 Actual implementation
		 * 2 Actual test
		 */
		[TestMethod()]
		public void OnSnoozeCompletion_SleepCalled_EventIsRaizedProperly()
		{
			SnoozeManager target = new SnoozeManager(new StubSleep());

			Assert.IsFalse(target.IsSnoozed, "target just launched and shouldn't be snoozed");

			bool calledback = false;

			target.OnSnoozeCompletion += () => { calledback = true; };

			var sleepDuration = new TimeSpan(0, 0, 0, 0, 200);
			target.Sleep(sleepDuration);

			Assert.IsTrue(target.IsSnoozed, "Target should be snoozed now");

			Thread.Sleep(TimeSpan.FromTicks(sleepDuration.Ticks * 2));

			Assert.IsFalse(target.IsSnoozed, "target should be awake now");
			Assert.IsTrue(calledback, "Should call callback delegate");

			calledback = false;

			Thread.Sleep(sleepDuration);
			Assert.IsFalse(calledback, "Should not call callback delegate after called once");
		}

		[TestMethod()]
		public void Wakeup_Sleep_NoCallbackAndIsNotSnoozed()
		{
			SnoozeManager target = new SnoozeManager(new StubSleep());
			var sleepDuration = new TimeSpan(0, 0, 0, 0, 200);

			bool calledback = false;

			target.OnSnoozeCompletion += () => { calledback = true; };

			target.Sleep(sleepDuration);
			Assert.IsTrue(target.IsSnoozed, "Target should be snoozed now");

			target.Wakeup();

			Assert.IsFalse(target.IsSnoozed, "should be able to abort snooze");

			// Lets wait and see if callback isn't called
			Thread.Sleep(TimeSpan.FromTicks(sleepDuration.Ticks * 2));
			Assert.IsFalse(calledback, "Shouldn't call callback delegate because snooze was aborted");
		}
	}
}
