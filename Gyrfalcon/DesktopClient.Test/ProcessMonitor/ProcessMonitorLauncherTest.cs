using DesktopClient.ProcessMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DesktopClient.SystemServices;
using System.Threading;

namespace DesktopClient.Test
{
	class StubCurrentProcess : ICurrentProcess
	{
		public int CallCount { get; set; }

		public string ProcessName
		{
			get { return "Foo"; }
		}

		public string MainWindowTitle
		{
			get { return "Bar"; }
		}

		public ICurrentProcess GetActiveWindowProcess()
		{
			CallCount++;
			return this;
		}
	}

	[TestClass()]
	public class ProcessMonitorLauncherTest
	{
		private ProcessMonitorLauncher _target = null;
		private StubCurrentProcess _currProcess = null;
		private const int _sleepTime = 1500;

		[TestInitialize]
		public void Init()
		{
			_currProcess = new StubCurrentProcess() { CallCount = 0 };
			_target = new ProcessMonitorLauncher();
			_target.Start(_currProcess, new DesktopClient.ClientInterface.StatusManager());
		}

		[TestCleanup]
		public void Cleanup()
		{
			_target.Stop();
			_target = null;
			_currProcess = null;
		}

		[TestMethod()]
		public void SleepTest()
		{
			_target.Sleep();
			Assert.IsTrue(_target.IsSleeping);

			int oldCount = _currProcess.CallCount;
			Thread.Sleep(_sleepTime);
			Assert.AreEqual(oldCount, _currProcess.CallCount);

			_target.Sleep();
			Assert.IsTrue(_target.IsSleeping);
			Thread.Sleep(_sleepTime);
			Assert.AreEqual(oldCount, _currProcess.CallCount);
		}

		[TestMethod()]
		public void WakeUpTest()
		{
			_target.WakeUp();
			Assert.IsFalse(_target.IsSleeping);

			int oldCount = _currProcess.CallCount;
			Thread.Sleep(_sleepTime);
			Assert.AreNotEqual(oldCount, _currProcess.CallCount);

			_target.WakeUp();
			Assert.IsFalse(_target.IsSleeping);
			Thread.Sleep(_sleepTime);
			Assert.AreNotEqual(oldCount, _currProcess.CallCount);
		}

		[TestMethod()]
		public void IsSleepingTest()
		{
			Assert.IsFalse(_target.IsSleeping);

			_target.Sleep();
			Assert.IsTrue(_target.IsSleeping);

			int oldCount = _currProcess.CallCount;
			Thread.Sleep(_sleepTime);
			Assert.AreEqual(oldCount, _currProcess.CallCount);

			_target.WakeUp();
			Assert.IsFalse(_target.IsSleeping);
			oldCount = _currProcess.CallCount;
			Thread.Sleep(_sleepTime);
			Assert.AreNotEqual(oldCount, _currProcess.CallCount);
		}
	}
}
