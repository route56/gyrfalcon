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

		private ClientManager clientManager;
		private SysTrayToolStripMenuItem _restOfTheDay;

		public Form1(ClientManager client)
		{
			InitializeComponent();

			clientManager = client;

			// clientManager.Snooze
			_sysTrayInterface = new SystemTrayInterface(clientManager);

			_menuItems = new List<IToolStripMenuItem>();

			_menuItems.Add(new SysTrayToolStripMenuItem(snoozeFor15MinsToolStripMenuItem) 
					{ Duration = new TimeSpan(0,15,0) });
			_menuItems.Add(new SysTrayToolStripMenuItem(snoozeFor60MinsToolStripMenuItem) 
					{ Duration = new TimeSpan(0,60,0) });
			_restOfTheDay = new SysTrayToolStripMenuItem(snoozeForTheDayToolStripMenuItem);
			_menuItems.Add(_restOfTheDay);

			_sysTrayInterface.ToolStripMenuItems = _menuItems;

			clientManager.Snooze.OnSnoozeCompletion += new Action(Snooze_OnSnoozeCompletion);

			clientManager.Alert.AlertMessenger += new Action<string>(Alert_AlertMessenger);
			clientManager.Alert.AlertSystemIsIdle += new Action<DateTime>(Alert_AlertSystemIsIdle);
		}

		void Alert_AlertSystemIsIdle(DateTime offlineSince)
		{
			var t = new System.Threading.Thread(() => Application.Run(new OfflineTimeForm(clientManager, offlineSince)));
			t.SetApartmentState(System.Threading.ApartmentState.STA);
			t.Start();
		}

		void Alert_AlertMessenger(string obj)
		{
			throw new NotImplementedException();
		}

		void Snooze_OnSnoozeCompletion()
		{
			SetSnoozeIcon(false);
		}

		private TimeSpan GetRemainingTimeLeft(DateTime dateTime)
		{
			DateTime dtEndOfDay = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);

			return dtEndOfDay.Subtract(dateTime);
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

			bool snoozed = _sysTrayInterface.SnoozeFor(_menuItems.FirstOrDefault(
				m => (m as SysTrayToolStripMenuItem).MenuItem == menuItem));

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
			// uh! ugly.
			_restOfTheDay.Duration = GetRemainingTimeLeft(DateTime.Now);
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
