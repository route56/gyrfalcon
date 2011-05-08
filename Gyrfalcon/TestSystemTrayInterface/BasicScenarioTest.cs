using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSystemTrayInterface
{
	[TestClass]
	public class BasicScenarioTest
	{
		[TestMethod]
		public void TestLaunchAndExit()
		{
			SetUpMockProcessActive();
			LaunchTheApp();
			WaitForSomeTime();
			ChangeActiveProcess();
			WaitForSomeTime();
			CloseTheApp();
			VerifyRecordedData();
		}

		private void VerifyRecordedData()
		{
			throw new NotImplementedException();
		}

		private void CloseTheApp()
		{
			throw new NotImplementedException();
		}

		private void ChangeActiveProcess()
		{
			throw new NotImplementedException();
		}

		private void WaitForSomeTime()
		{
			throw new NotImplementedException();
		}

		private void LaunchTheApp()
		{
			throw new NotImplementedException();
		}

		private void SetUpMockProcessActive()
		{
			throw new NotImplementedException();
		}
	}
}
