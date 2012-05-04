using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DesktopClient.SystemServices
{
	public class CurrentProcess : ICurrentProcess
	{
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

		public string ProcessName { get; private set; }
		public string MainWindowTitle { get; private set; }

		public ICurrentProcess GetActiveWindowProcess()
		{
			CurrentProcess cp = new CurrentProcess()
			{ 
				ProcessName = string.Empty, 
				MainWindowTitle = string.Empty 
			};

			Process proc = _GetCurrentProcess();

			if (proc != null)
			{
				try
				{
					cp.ProcessName = proc.ProcessName;
					cp.MainWindowTitle = proc.MainWindowTitle;
				}
				catch (Exception ex)
				{
					LogError(ex);
				}
			}

			return cp;
		}

		private static Process _GetCurrentProcess()
		{
			IntPtr handle = new IntPtr(0);

			handle = GetForegroundWindow();

			if (handle == null)
			{
				return null;
			}

			try
			{
				int pid;
				GetWindowThreadProcessId(handle, out pid);

				return Process.GetProcessById(pid);
			}
			catch (Exception ex)
			{
				LogError(ex);
				return null;
			}
		}

		[Conditional("DEBUG")]
		static private void LogError(Exception ex)
		{
			using (System.IO.StreamWriter sw = System.IO.File.AppendText(@"C:\DebugLogFile.txt"))
			{
				sw.WriteLine(ex.Message);
				sw.WriteLine(ex.StackTrace);
				sw.WriteLine(ex.ToString());
			}
		}
	}
}
