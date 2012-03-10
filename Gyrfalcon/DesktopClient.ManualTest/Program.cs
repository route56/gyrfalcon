using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesktopClient.SystemServices;
using System.IO;
using System.Threading;

namespace DesktopClient.ManualTest
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendFormat("{0}", DateTime.Now);

				var process = CurrentProcess.GetActiveWindowProcess();

				sb.AppendFormat(",{0}", process.ProcessName);
				sb.AppendFormat(",{0}", process.MainWindowTitle);

				Console.WriteLine(sb.ToString());

				using (StreamWriter sw = File.AppendText("DCManualLog.txt"))
				{
					sw.WriteLine(sb.ToString());
				}

				Thread.Sleep(1000);
			}
		}
	}
}
