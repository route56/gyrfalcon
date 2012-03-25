namespace ReportApp
{
	partial class Dashboard
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
			this.combinedControl1 = new ReportApp.CustomControls.CombinedControl();
			this.SuspendLayout();
			// 
			// combinedControl1
			// 
			this.combinedControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.combinedControl1.Location = new System.Drawing.Point(0, 0);
			this.combinedControl1.Name = "combinedControl1";
			this.combinedControl1.Size = new System.Drawing.Size(533, 528);
			this.combinedControl1.TabIndex = 0;
			// 
			// Dashboard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(533, 528);
			this.Controls.Add(this.combinedControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(360, 400);
			this.Name = "Dashboard";
			this.Text = "Dashboard";
			this.ResumeLayout(false);

		}

		#endregion

		private CustomControls.CombinedControl combinedControl1;






	}
}

