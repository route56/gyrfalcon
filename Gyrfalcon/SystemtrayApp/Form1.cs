using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DesktopClient;

namespace SystemtrayApp
{
	public partial class Form1 : Form
	{
		private SystemTrayInterface _sysTrayInterface;
		public Form1()
		{
			InitializeComponent();

			_sysTrayInterface = new SystemTrayInterface(); 
		}

		private void openDashboardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_sysTrayInterface.OpenWebsite();
		}

		private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DCSettings settings = _sysTrayInterface.GetSettings();
			//TODO Use above data to populate settings and show it to user.
			// Use SetSettings to save them back
		}

		private void snoozeFor15MinsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TimeSpan timespan = new TimeSpan(0, 0, 10); // TODO atfer verification Change to 15 mins Read this from config file

			CheckForIllegalCrossThreadCalls = false; // TODO REMOVE THIS! Bug ID 218

			//_sysTrayInterface.SnoozeFor(snoozeFor15MinsToolStripMenuItem, timespan);
		}

		private void ToggleNotifyIconVisibility()
		{
			notifyIcon1.Visible = !notifyIcon1.Visible;
			notifyIcon2.Visible = !notifyIcon2.Visible;
		}

		private void snoozeFor60MinsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// todo
		}

		private void snoozeForTheDayToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// todo
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_sysTrayInterface.Exit();
			this.Close();
		}

		//private void Form1_Load(object sender, EventArgs e)
		//{
		//    this.WindowState = FormWindowState.Minimized;
		//    this.Visible = false;
		//    this.ShowInTaskbar = false;
		//}

		protected override void OnLoad(EventArgs e)
		{
			this.ShowInTaskbar = false;
			this.Visible = false;

			base.OnLoad(e);
		}
	}
}
