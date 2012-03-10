using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Timers;
using System.Configuration;
using DesktopClient.Settings;
using DesktopClient.ClientInterface;

namespace SystemtrayApp
{
	public class SystemTrayInterface
	{
		private ISnooze _snoozeManager;

		public SystemTrayInterface(ISnooze snooze)
		{
			_snoozeManager = snooze;

			_snoozeManager.OnSnoozeCompletion += new Action(Snooze_OnSnoozeCompletion);
		}

		void Snooze_OnSnoozeCompletion()
		{
			// This removes snoozed flag
			ToolStripMenuItems.FirstOrDefault(m => m.IsSnoozed == true).IsSnoozed = false;
		}

		public void OpenWebsite()
		{
			//Process.Start(ConfigurationManager.AppSettings["WebsiteURL"]); // TODO Can this be a security concern?
			// TODO Get url logic. Login stuff. For now just open the file generated
		}

		public void Exit()
		{
			//TODO handle exit code for now removing below throw.
		}

		public bool IsSnoozed
		{ 
			get 
			{
				if (ToolStripMenuItems != null)
				{
					if (ToolStripMenuItems.FirstOrDefault(m => m.IsSnoozed == true) != null)
					{
						return true;
					}
				}

				return false;
			} 
		}

		public DCSettings GetSettings()
		{
			throw new NotImplementedException(); // TODOL Next ver
		}

		public void SetSettings(DCSettings settings)
		{
			throw new NotImplementedException(); // TODOL Next ver
		}

		private void Snooze(TimeSpan timespan)
		{
			_snoozeManager.Sleep(timespan);
		}

		private void AbortSnooze()
		{
			_snoozeManager.Wakeup();
		}

		/// <summary>
		/// Snooze systray.
		/// </summary>
		/// <param name="menuItem"></param>
		/// <param name="timespan"></param>
		/// <returns>True if snoozed else False</returns>
		public bool SnoozeFor(IToolStripMenuItem menuItem)
		{ 
			if (ToolStripMenuItems == null)
			{
				throw new InvalidOperationException("ToolStripMenuItems should be initialized before calling this method");
			}

			IToolStripMenuItem exists = ToolStripMenuItems.FirstOrDefault(m => m == menuItem);

			if (exists == null)
			{
				throw new InvalidOperationException("Argument not found in ToolStripMenuItems");
			}

			if (menuItem.IsSnoozed)
			{
				AbortSnooze();
				menuItem.IsSnoozed = false;
				return false;
			}
			else
			{
				IToolStripMenuItem oldSnoozed = ToolStripMenuItems.FirstOrDefault(m => m.IsSnoozed == true);

				if (oldSnoozed != null)
				{
					AbortSnooze();
					oldSnoozed.IsSnoozed = false;
				}
				
				menuItem.IsSnoozed = true;
				Snooze(menuItem.Duration);
				return true;
			}
		}

		public List<IToolStripMenuItem> ToolStripMenuItems { get; set; }
	}
}
