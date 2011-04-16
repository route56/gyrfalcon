﻿namespace SystemtrayApp
{
	partial class Form1
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.snoozeFor15MinsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.snoozeFor60MinsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openDashboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.snoozeForTheDayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
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
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDashboardToolStripMenuItem,
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
			this.contextMenuStrip1.Size = new System.Drawing.Size(175, 170);
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.settingsToolStripMenuItem.Text = "Settings...";
			this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
			// 
			// snoozeFor15MinsToolStripMenuItem
			// 
			this.snoozeFor15MinsToolStripMenuItem.Name = "snoozeFor15MinsToolStripMenuItem";
			this.snoozeFor15MinsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.snoozeFor15MinsToolStripMenuItem.Text = "Snooze for 15 mins";
			this.snoozeFor15MinsToolStripMenuItem.Click += new System.EventHandler(this.snoozeFor15MinsToolStripMenuItem_Click);
			// 
			// snoozeFor60MinsToolStripMenuItem
			// 
			this.snoozeFor60MinsToolStripMenuItem.Name = "snoozeFor60MinsToolStripMenuItem";
			this.snoozeFor60MinsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.snoozeFor60MinsToolStripMenuItem.Text = "Snooze for 60 mins";
			this.snoozeFor60MinsToolStripMenuItem.Click += new System.EventHandler(this.snoozeFor60MinsToolStripMenuItem_Click);
			// 
			// openDashboardToolStripMenuItem
			// 
			this.openDashboardToolStripMenuItem.Name = "openDashboardToolStripMenuItem";
			this.openDashboardToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.openDashboardToolStripMenuItem.Text = "Open Dashboard...";
			this.openDashboardToolStripMenuItem.Click += new System.EventHandler(this.openDashboardToolStripMenuItem_Click);
			// 
			// snoozeForTheDayToolStripMenuItem
			// 
			this.snoozeForTheDayToolStripMenuItem.Name = "snoozeForTheDayToolStripMenuItem";
			this.snoozeForTheDayToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.snoozeForTheDayToolStripMenuItem.Text = "Snooze for the day";
			this.snoozeForTheDayToolStripMenuItem.Click += new System.EventHandler(this.snoozeForTheDayToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(171, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(171, 6);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 264);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.Text = "Form1";
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

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
	}
}

