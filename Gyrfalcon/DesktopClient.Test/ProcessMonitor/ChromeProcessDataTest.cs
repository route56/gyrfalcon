using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesktopClient.ProcessMonitor;

namespace DesktopClient.Test.ProcessMonitor
{
    [TestClass]
    public class ChromeProcessDataTest
    {
        [TestMethod]
        public void GetCurrentUrl_LaunchGooogle_Expected()
        {
            ChromeProcessData target = new ChromeProcessData();

            string urlToLaunch = "http://google.com";

            LaunchChromeFor(urlToLaunch);

            string urlExpected = target.GetCurrentUrl();

            Assert.AreEqual(urlToLaunch, urlExpected, "URLS don't match!!");

        }

        private void LaunchChromeFor(string urlToLaunch)
        {
            // This is test code which will launch chrome with url.
            throw new NotImplementedException();
        }
    }
}
