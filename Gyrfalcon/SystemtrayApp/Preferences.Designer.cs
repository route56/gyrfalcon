namespace SystemtrayApp
{
	partial class Preferences
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Preferences));
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openDashboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.goToDashboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.getFocusedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.snoozeFor15MinsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.snoozeFor60MinsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.snoozeForTheDayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.notifyIcon2 = new System.Windows.Forms.NotifyIcon(this.components);
			this.btnDashboard = new System.Windows.Forms.Button();
			this.btnSettings = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.lblAbout = new System.Windows.Forms.LinkLabel();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.BalloonTipText = "GyrFalcon running";
			this.notifyIcon1.BalloonTipTitle = "GyrFalcon";
			this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "GyrFalcon";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDashboardToolStripMenuItem,
            this.toolStripSeparator3,
            this.goToDashboardToolStripMenuItem,
            this.getFocusedToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.toolStripSeparator2,
            this.snoozeFor15MinsToolStripMenuItem,
            this.snoozeFor60MinsToolStripMenuItem,
            this.snoozeForTheDayToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.ShowCheckMargin = true;
			this.contextMenuStrip1.ShowImageMargin = false;
			this.contextMenuStrip1.Size = new System.Drawing.Size(190, 220);
			// 
			// openDashboardToolStripMenuItem
			// 
			this.openDashboardToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.openDashboardToolStripMenuItem.Name = "openDashboardToolStripMenuItem";
			this.openDashboardToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.openDashboardToolStripMenuItem.Text = "Preferences...";
			this.openDashboardToolStripMenuItem.Click += new System.EventHandler(this.openDashboardToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(186, 6);
			// 
			// goToDashboardToolStripMenuItem
			// 
			this.goToDashboardToolStripMenuItem.Name = "goToDashboardToolStripMenuItem";
			this.goToDashboardToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.goToDashboardToolStripMenuItem.Text = "Go To Dashboard...";
			this.goToDashboardToolStripMenuItem.Click += new System.EventHandler(this.goToDashboardToolStripMenuItem_Click);
			// 
			// getFocusedToolStripMenuItem
			// 
			this.getFocusedToolStripMenuItem.Name = "getFocusedToolStripMenuItem";
			this.getFocusedToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.getFocusedToolStripMenuItem.Text = "Get Focused...";
			this.getFocusedToolStripMenuItem.Click += new System.EventHandler(this.getFocusedToolStripMenuItem_Click);
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.settingsToolStripMenuItem.Text = "Settings...";
			this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(186, 6);
			// 
			// snoozeFor15MinsToolStripMenuItem
			// 
			this.snoozeFor15MinsToolStripMenuItem.Name = "snoozeFor15MinsToolStripMenuItem";
			this.snoozeFor15MinsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.snoozeFor15MinsToolStripMenuItem.Text = "Pause 15 minutes";
			this.snoozeFor15MinsToolStripMenuItem.Click += new System.EventHandler(this.snoozeFor15MinsToolStripMenuItem_Click);
			// 
			// snoozeFor60MinsToolStripMenuItem
			// 
			this.snoozeFor60MinsToolStripMenuItem.Name = "snoozeFor60MinsToolStripMenuItem";
			this.snoozeFor60MinsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.snoozeFor60MinsToolStripMenuItem.Text = "Pause 60 minutes";
			this.snoozeFor60MinsToolStripMenuItem.Click += new System.EventHandler(this.snoozeFor60MinsToolStripMenuItem_Click);
			// 
			// snoozeForTheDayToolStripMenuItem
			// 
			this.snoozeForTheDayToolStripMenuItem.Name = "snoozeForTheDayToolStripMenuItem";
			this.snoozeForTheDayToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.snoozeForTheDayToolStripMenuItem.Text = "Pause Until Tomorrow";
			this.snoozeForTheDayToolStripMenuItem.Click += new System.EventHandler(this.snoozeForTheDayToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(186, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// notifyIcon2
			// 
			this.notifyIcon2.ContextMenuStrip = this.contextMenuStrip1;
			this.notifyIcon2.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon2.Icon")));
			this.notifyIcon2.Text = "GyrFalcon snoozed";
			this.notifyIcon2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon2_MouseDoubleClick);
			// 
			// btnDashboard
			// 
			this.btnDashboard.Location = new System.Drawing.Point(23, 39);
			this.btnDashboard.Name = "btnDashboard";
			this.btnDashboard.Size = new System.Drawing.Size(161, 55);
			this.btnDashboard.TabIndex = 1;
			this.btnDashboard.Text = "Dashboard";
			this.btnDashboard.UseVisualStyleBackColor = true;
			this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
			// 
			// btnSettings
			// 
			this.btnSettings.Location = new System.Drawing.Point(236, 39);
			this.btnSettings.Name = "btnSettings";
			this.btnSettings.Size = new System.Drawing.Size(145, 55);
			this.btnSettings.TabIndex = 2;
			this.btnSettings.Text = "Settings";
			this.btnSettings.UseVisualStyleBackColor = true;
			this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.Location = new System.Drawing.Point(20, 116);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(361, 60);
			this.lblStatus.TabIndex = 4;
			this.lblStatus.Text = "label1";
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(306, 201);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 5;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// lblAbout
			// 
			this.lblAbout.AutoSize = true;
			this.lblAbout.Location = new System.Drawing.Point(20, 211);
			this.lblAbout.Name = "lblAbout";
			this.lblAbout.Size = new System.Drawing.Size(55, 13);
			this.lblAbout.TabIndex = 6;
			this.lblAbout.TabStop = true;
			this.lblAbout.Text = "linkLabel1";
			this.lblAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblAbout_LinkClicked);
			// 
			// Preferences
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(406, 249);
			this.ControlBox = false;
			this.Controls.Add(this.lblAbout);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnDashboard);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.btnSettings);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Preferences";
			this.Text = "Gyrfalcon";
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem snoozeFor15MinsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem snoozeFor60MinsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openDashboardToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem snoozeForTheDayToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.NotifyIcon notifyIcon2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem goToDashboardToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem getFocusedToolStripMenuItem;
		private System.Windows.Forms.Button btnDashboard;
		private System.Windows.Forms.Button btnSettings;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.LinkLabel lblAbout;
	}
}

