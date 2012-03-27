namespace ReportApp.CustomControls
{
	partial class CombinedControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageAll = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tabPageGroupBy = new System.Windows.Forms.TabPage();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.lblSummary = new System.Windows.Forms.Label();
			this.barChartControl1 = new ReportApp.CustomControls.BarChartControl();
			this.barGridControl1 = new ReportApp.CustomControls.BarGridControl();
			this.areaChartControl1 = new ReportApp.CustomControls.AreaChartControl();
			this.areaGridControl1 = new ReportApp.CustomControls.AreaGridControl();
			this.timeWindow1 = new ReportApp.CustomControls.TimeWindowControl();
			this.tabControl1.SuspendLayout();
			this.tabPageAll.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabPageGroupBy.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPageAll);
			this.tabControl1.Controls.Add(this.tabPageGroupBy);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tabControl1.Location = new System.Drawing.Point(0, 75);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(500, 434);
			this.tabControl1.TabIndex = 1;
			// 
			// tabPageAll
			// 
			this.tabPageAll.Controls.Add(this.splitContainer1);
			this.tabPageAll.Location = new System.Drawing.Point(4, 22);
			this.tabPageAll.Name = "tabPageAll";
			this.tabPageAll.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageAll.Size = new System.Drawing.Size(492, 408);
			this.tabPageAll.TabIndex = 0;
			this.tabPageAll.Text = "all activities";
			this.tabPageAll.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.barChartControl1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.barGridControl1);
			this.splitContainer1.Size = new System.Drawing.Size(486, 402);
			this.splitContainer1.SplitterDistance = 162;
			this.splitContainer1.TabIndex = 0;
			// 
			// tabPageGroupBy
			// 
			this.tabPageGroupBy.Controls.Add(this.splitContainer2);
			this.tabPageGroupBy.Location = new System.Drawing.Point(4, 22);
			this.tabPageGroupBy.Name = "tabPageGroupBy";
			this.tabPageGroupBy.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageGroupBy.Size = new System.Drawing.Size(492, 408);
			this.tabPageGroupBy.TabIndex = 1;
			this.tabPageGroupBy.Text = "by day";
			this.tabPageGroupBy.UseVisualStyleBackColor = true;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(3, 3);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.areaChartControl1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.areaGridControl1);
			this.splitContainer2.Size = new System.Drawing.Size(486, 402);
			this.splitContainer2.SplitterDistance = 162;
			this.splitContainer2.TabIndex = 0;
			// 
			// lblSummary
			// 
			this.lblSummary.Location = new System.Drawing.Point(3, 3);
			this.lblSummary.Name = "lblSummary";
			this.lblSummary.Size = new System.Drawing.Size(137, 50);
			this.lblSummary.TabIndex = 2;
			this.lblSummary.Text = "Summary";
			// 
			// barChartControl1
			// 
			this.barChartControl1.BarChartData = null;
			this.barChartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.barChartControl1.Location = new System.Drawing.Point(0, 0);
			this.barChartControl1.Name = "barChartControl1";
			this.barChartControl1.Size = new System.Drawing.Size(486, 162);
			this.barChartControl1.TabIndex = 0;
			// 
			// barGridControl1
			// 
			this.barGridControl1.BarChartData = null;
			this.barGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.barGridControl1.Location = new System.Drawing.Point(0, 0);
			this.barGridControl1.Name = "barGridControl1";
			this.barGridControl1.Size = new System.Drawing.Size(486, 236);
			this.barGridControl1.TabIndex = 0;
			// 
			// areaChartControl1
			// 
			this.areaChartControl1.AreaGridData = null;
			this.areaChartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.areaChartControl1.Location = new System.Drawing.Point(0, 0);
			this.areaChartControl1.Name = "areaChartControl1";
			this.areaChartControl1.Size = new System.Drawing.Size(486, 162);
			this.areaChartControl1.TabIndex = 0;
			// 
			// areaGridControl1
			// 
			this.areaGridControl1.AreaGridData = null;
			this.areaGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.areaGridControl1.Location = new System.Drawing.Point(0, 0);
			this.areaGridControl1.Name = "areaGridControl1";
			this.areaGridControl1.Size = new System.Drawing.Size(486, 236);
			this.areaGridControl1.TabIndex = 0;
			// 
			// timeWindow1
			// 
			this.timeWindow1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.timeWindow1.Location = new System.Drawing.Point(146, 3);
			this.timeWindow1.Name = "timeWindow1";
			this.timeWindow1.Size = new System.Drawing.Size(347, 66);
			this.timeWindow1.Start = new System.DateTime(2012, 3, 28, 0, 0, 0, 0);
			this.timeWindow1.TabIndex = 0;
			// 
			// CombinedControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblSummary);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.timeWindow1);
			this.Name = "CombinedControl";
			this.Size = new System.Drawing.Size(500, 509);
			this.tabControl1.ResumeLayout(false);
			this.tabPageAll.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tabPageGroupBy.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private TimeWindowControl timeWindow1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageAll;
		private System.Windows.Forms.TabPage tabPageGroupBy;
		private System.Windows.Forms.Label lblSummary;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private BarChartControl barChartControl1;
		private BarGridControl barGridControl1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private AreaChartControl areaChartControl1;
		private AreaGridControl areaGridControl1;
	}
}
