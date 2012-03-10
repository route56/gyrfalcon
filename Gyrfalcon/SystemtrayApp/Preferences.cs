using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DesktopClient.ClientInterface;
using System.Diagnostics;

namespace SystemtrayApp
{
	public partial class Preferences : Form
	{
		private SystemTrayInterface _sysTrayInterface;
		private List<IToolStripMenuItem> _menuItems;

		private IClientInterface _clientManager;
		private SysTrayToolStripMenuItem _restOfTheDay;

		public Preferences(IClientInterface client)
		{
			InitializeComponent();

			_clientManager = client;

			EnableAvaliableFeatures();

			lblAbout.Text = _clientManager.Status.ClientVersion;

			lblStatus.Text = GetStatusMessage(_clientManager.Status.IsEverythingOk, _clientManager.Status.LastSuccessfulTransmission);

			_clientManager.Status.StatusChanged += new Action(Status_StatusChanged);

			if (_clientManager.Snooze != null)
			{
				EnableSnoozeFeature();
			}

			_clientManager.Alert.AlertMessenger += new Action<string>(Alert_AlertMessenger);
			_clientManager.Alert.AlertSystemIsIdle += new Action<DateTime>(Alert_AlertSystemIsIdle);
		}

		private void EnableSnoozeFeature()
		{
			_sysTrayInterface = new SystemTrayInterface(_clientManager.Snooze);

			_menuItems = new List<IToolStripMenuItem>();

			_menuItems.Add(new SysTrayToolStripMenuItem(snoozeFor15MinsToolStripMenuItem) { Duration = new TimeSpan(0, 15, 0) });
			_menuItems.Add(new SysTrayToolStripMenuItem(snoozeFor60MinsToolStripMenuItem) { Duration = new TimeSpan(0, 60, 0) });
			_restOfTheDay = new SysTrayToolStripMenuItem(snoozeForTheDayToolStripMenuItem);
			_menuItems.Add(_restOfTheDay);

			_sysTrayInterface.ToolStripMenuItems = _menuItems;

			_clientManager.Snooze.OnSnoozeCompletion += new Action(Snooze_OnSnoozeCompletion);
		}

		private void EnableAvaliableFeatures()
		{
			if (_clientManager.GetFocused == null)
			{
				contextMenuStrip1.Items.Remove(getFocusedToolStripMenuItem);
			}

			if (_clientManager.Snooze == null)
			{
				contextMenuStrip1.Items.Remove(snoozeFor15MinsToolStripMenuItem);
				contextMenuStrip1.Items.Remove(snoozeFor60MinsToolStripMenuItem);
				contextMenuStrip1.Items.Remove(snoozeForTheDayToolStripMenuItem);
				contextMenuStrip1.Items.Remove(toolStripSeparator1);
			}

			if (_clientManager.Settings == null)
			{
				btnSettings.Visible = false;
				contextMenuStrip1.Items.Remove(settingsToolStripMenuItem);
			}
		}

		private string GetStatusMessage(bool success, DateTime dateTime)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("Status: ");

			if (success)
			{
				sb.Append("Successfully sent data at ");
				sb.Append(dateTime.ToShortDateString());
				sb.Append(" ");
				sb.Append(dateTime.ToShortTimeString());
			}
			else
			{
				sb.Append("Unable to connect to the internet. No data will be lost. We'll try again in 3 to 30 min");
			}

			return sb.ToString();
		}

		private TimeSpan GetRemainingTimeLeft(DateTime dateTime)
		{
			DateTime dtEndOfDay = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);

			return dtEndOfDay.Subtract(dateTime);
		}

		private void openDashboardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Visible = true;
		}

		private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start(_clientManager.Settings.GetSettingEditorUrl());
		}

		private void SnoozeFor(ToolStripMenuItem menuItem)
		{
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

		#region Button Clicks
		private void btnDashboard_Click(object sender, EventArgs e)
		{
			_clientManager.Dashboard.LaunchDashboard();
		}

		private void btnSettings_Click(object sender, EventArgs e)
		{
			Process.Start(_clientManager.Settings.GetSettingEditorUrl());
		}

		private void lblAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(_clientManager.Status.VersionHistoryURL);
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Visible = false;
		}
		#endregion

		#region Alerts and events

		void Status_StatusChanged()
		{
			if (InvokeRequired)
			{
				Action cb = new Action(Status_StatusChanged);
				Invoke(cb);
				return;
			}

			lblStatus.Text = GetStatusMessage(_clientManager.Status.IsEverythingOk, _clientManager.Status.LastSuccessfulTransmission);
		}

		void Snooze_OnSnoozeCompletion()
		{
			if (InvokeRequired)
			{
				Action cb = new Action(Snooze_OnSnoozeCompletion);
				Invoke(cb);
				return;
			}

			SetSnoozeIcon(false);
		}

		void Alert_AlertMessenger(string msg)
		{
			if (InvokeRequired)
			{
				Action<string> cb = new Action<string>(Alert_AlertMessenger);
				Invoke(cb, new object[] { msg });
				return;
			}

			if (notifyIcon1.Visible)
			{
				notifyIcon1.BalloonTipText = msg;
				notifyIcon1.ShowBalloonTip(5000);
			}
			else
			{
				notifyIcon2.BalloonTipText = msg;
				notifyIcon2.ShowBalloonTip(5000);
			}
		}

		void Alert_AlertSystemIsIdle(DateTime offlineSince)
		{
			if (InvokeRequired)
			{
				Action<DateTime> cb = new Action<DateTime>(Alert_AlertSystemIsIdle);
				Invoke(cb, new object[] { offlineSince });
				return;
			}

			//var t = new System.Threading.Thread(() => Application.Run(new OfflineTimeForm(_clientManager, offlineSince)));
			//t.SetApartmentState(System.Threading.ApartmentState.STA);
			//t.Start();

			// Modal dialog on same thread.
			if (_clientManager.OfflineTask != null)
			{
				var offline = new OfflineTimeForm(_clientManager, offlineSince);
				offline.ShowDialog();
			}
		}

		#endregion

		private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.Visible = true;
		}

		private void notifyIcon2_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.Visible = true;
		}

		private void getFocusedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//_clientManager.GetFocused.
			//MessageBox.Show("Feature not implemented yet");
		}

		private void goToDashboardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_clientManager.Dashboard.LaunchDashboard();
		}
	}
}
