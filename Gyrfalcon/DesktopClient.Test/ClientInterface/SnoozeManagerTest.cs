using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Collections.Generic;
using DesktopClient.ClientInterface;

namespace DesktopClient.Test.ClientInterface
{
	[TestClass()]
	public class SnoozeManagerTest
	{
		[TestMethod()]
		public void OnSnoozeCompletion_SleepCalled_EventIsRaizedProperly()
		{
			SnoozeManager target = new SnoozeManager();

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
			SnoozeManager target = new SnoozeManager();
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
