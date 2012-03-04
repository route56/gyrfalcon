﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DesktopClient.ProcessMonitor
{
	public class CurrentProcess
	{
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

		private const int _nChars = 256; // TODO figure out max value to pass

		public void GetActiveWindow()
		{
			IntPtr handle = new IntPtr(0);
			StringBuilder Buff = new StringBuilder(_nChars);

			handle = GetForegroundWindow();

			if (GetWindowText(handle, Buff, _nChars) > 0)
			{
				Debug.WriteLine(Buff.ToString());
				Debug.WriteLine(handle.ToString());
			}
		}

		/// <summary>
		/// Can return NULL
		/// </summary>
		/// <returns></returns>
		public Process GetActiveWindowProcess()
		{
			IntPtr handle = new IntPtr(0);
			// The foreground window can be NULL in certain circumstances, 
			// such as when a window is losing activation.
			handle = GetForegroundWindow();

			if (handle == null)
			{
				return null;
			}

			return Process.GetProcessById((int)handle);
		}

		//public Process GetForegroundProcess()
		//{
			//try
			//{
			//    IntPtr hwnd = GetForegroundWindow();
			//    // The foreground window can be NULL in certain circumstances, 
			//    // such as when a window is losing activation.
			//    uint pid;
			//    GetWindowThreadProcessId(hwnd, out pid);

			//    Process p = Process.GetProcessById((int)pid);

			//    return p;
			//}
			//catch (Exception)
			//{
			//    return null;
			//}
		//}
	}
}
/*
 * http://www.csharphelp.com/2006/08/get-current-window-handle-and-caption-with-windows-api-in-c/
 * http://stackoverflow.com/questions/5412241/getforegroundwindow-with-ticker-c-reduce-cpu-usage
 * Are you calling Dispose on those Process objects after you're done with them?
 * Disable the timer when you're code is running!. If its slower then the timer it'll lock up you're system. 
 * Execute the code on a background thread and see if there is a different API to get the same information 
 * that works better.
 * // Returns the name of the process owning the foreground window.
    private static Process GetForegroundProcess()
    {
        try
        {
            IntPtr hwnd = GetForegroundWindow();
            // The foreground window can be NULL in certain circumstances, 
            // such as when a window is losing activation.
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);

            Process p = Process.GetProcessById((int)pid);

            return p;
        }
        catch (Exception)
        {
            return null;
        }
    }*/
