using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DesktopClient.ClientInterface;

namespace SystemtrayApp
{
	public partial class Form1 : Form
	{
		private SystemTrayInterface _sysTrayInterface;
		private List<IToolStripMenuItem> _menuItems;

		public Form1()
		{
			InitializeComponent();

			_sysTrayInterface = new SystemTrayInterface();

			_menuItems = new List<IToolStripMenuItem>();

			_menuItems.Add(new SysTrayToolStripMenuItem(snoozeFor15MinsToolStripMenuItem) { Duration = new TimeSpan(0,0,3) }); // TODOH atfer verification Change to 15 mins Read this from config file
			_menuItems.Add(new SysTrayToolStripMenuItem(snoozeFor60MinsToolStripMenuItem) { Duration = new TimeSpan(0,0,3) }); // TODOH atfer verification Change to 60 mins Read this from config file
			_menuItems.Add(new SysTrayToolStripMenuItem(snoozeForTheDayToolStripMenuItem) { Duration = new TimeSpan(0,0,3) }); // TODOH atfer verification Change to 1 day Read this from config file

			_sysTrayInterface.ToolStripMenuItems = _menuItems;

			_sysTrayInterface.OnSnoozeCompleteCallback = () => { SetSnoozeIcon(false); };
		}

		private void openDashboardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_sysTrayInterface.OpenWebsite();
		}

		private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//DCSettings settings = _sysTrayInterface.GetSettings();
			//TODO Implementation of settings: 6 offline dialog buttons configurable (but this isn't seen now)
			//Use above data to populate settings and show it to user.
			// Use SetSettings to save them back
		}

		private void SnoozeFor(ToolStripMenuItem menuItem)
		{
            // TODO Localization stuff: Read text strings from one giant string resource file.

			CheckForIllegalCrossThreadCalls = false; // TODO REMOVE THIS! Bug ID 218

			bool snoozed = _sysTrayInterface.SnoozeFor(_menuItems.FirstOrDefault(m => (m as SysTrayToolStripMenuItem).MenuItem == menuItem));

			SetSnoozeIcon(snoozed);
		}

		private void snoozeFor15MinsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SnoozeFor(snoozeFor15MinsToolStripMenuItem);
		}

		private void snoozeFor60MinsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SnoozeFor(snoozeFor60MinsToolStripMenuItem);
		}

		private void snoozeForTheDayToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SnoozeFor(snoozeForTheDayToolStripMenuItem);
		}

		private void SetSnoozeIcon(bool visible)
		{
			notifyIcon1.Visible = !visible;
			notifyIcon2.Visible = visible;
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_sysTrayInterface.Exit();
			this.Close();
		}

		protected override void OnLoad(EventArgs e)
		{
			this.ShowInTaskbar = false;
			this.Visible = false;

			base.OnLoad(e);
		}
	}
}
