using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Timers;
using System.Configuration;

namespace DesktopClient
{
	public class SystemTrayInterface
	{
		public void OpenWebsite()
		{
			Process.Start(ConfigurationManager.AppSettings["WebsiteURL"]); // TODO Can this be a security concern?
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

		public delegate void SnoozeCompleteCallback();

		public SnoozeCompleteCallback OnSnoozeCompleteCallback { get; set; }

		private void Snooze(TimeSpan timespan)
		{
			Debug.Assert(timespan.TotalMilliseconds <= Int32.MaxValue, "constraint of timer class");

			// One time timer.
			_timer = new Timer(timespan.TotalMilliseconds) { AutoReset = false };

			// TODOH is this ok? Debug.Assert(OnSnoozeCompleteCallback == null, "should not be having pending callbacks, the contract. may want to change this where snooze can be called multiple times");

			_timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
			_timer.Start();
		}

		private void AbortSnooze(IToolStripMenuItem menuItem)
		{
			//Debug.Assert(IsSnoozed, "Contract: abort to be called only if snooze is set");
			Debug.Assert(_timer != null, "time shouldn't be null either");

			_timer.Stop();
			OnSnoozeCompleteCallback = null;
			_timer = null;
		}

		/// <summary>
		/// Snooze systray.
		/// </summary>
		/// <param name="menuItem"></param>
		/// <param name="timespan"></param>
		/// <returns>True if snoozed else False</returns>
		public bool SnoozeFor(IToolStripMenuItem menuItem)
		{ 
			// Dev is a HERO!
			// Awesome PM,	Bad Dev,	Bad QA	=> Doomed
			// Awesome QA,	Bad PM,		Bad Dev	=> Doomed
			// Awesome Dev,	Bad QA,		Bad PM	=> Saves the day!

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
				AbortSnooze(menuItem);
				menuItem.IsSnoozed = false;
				return false;
			}
			else
			{
				IToolStripMenuItem oldSnoozed = ToolStripMenuItems.FirstOrDefault(m => m.IsSnoozed == true);

				if (oldSnoozed != null)
				{
					oldSnoozed.IsSnoozed = false;
				}
				
				menuItem.IsSnoozed = true;
				Snooze(menuItem.Duration);
				return true;
			}
		}

		public List<IToolStripMenuItem> ToolStripMenuItems { get; set; }

		private void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (OnSnoozeCompleteCallback != null)
			{
				OnSnoozeCompleteCallback();
				OnSnoozeCompleteCallback = null;
			}

			// This removes snoozed flag
			ToolStripMenuItems.FirstOrDefault(m => m.IsSnoozed == true).IsSnoozed = false;
		}

		private Timer _timer = null;
	}
}
