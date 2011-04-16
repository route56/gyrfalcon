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
			//this.InvokeRequired
			snoozeFor15MinsToolStripMenuItem.Checked = true;
			//snoozeFor15MinsToolStripMenuItem.Invoke
			
			_sysTrayInterface.Snooze(timespan,
				() =>
				{
					//if (InvokeRequired)
					//{
					//    Invoke(new Action<object>(snoozeFor15MinsToolStripMenuItem), o);
					//}
					snoozeFor15MinsToolStripMenuItem.Checked = false;
				});

		}

		private void snoozeFor60MinsToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void snoozeForTheDayToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}


	}
}
