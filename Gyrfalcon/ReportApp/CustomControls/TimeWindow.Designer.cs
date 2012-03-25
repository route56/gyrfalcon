namespace ReportApp.CustomControls
{
	partial class TimeWindow
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
			this.btnNext = new System.Windows.Forms.Button();
			this.lblTimeWindow = new System.Windows.Forms.Label();
			this.btnPrevious = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.linkLabelYear = new System.Windows.Forms.LinkLabel();
			this.linkLabelMonth = new System.Windows.Forms.LinkLabel();
			this.linkLabelWeek = new System.Windows.Forms.LinkLabel();
			this.linkLabelDay = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// btnNext
			// 
			this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNext.Location = new System.Drawing.Point(213, 9);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(31, 23);
			this.btnNext.TabIndex = 1;
			this.btnNext.Text = "->";
			this.btnNext.UseVisualStyleBackColor = true;
			// 
			// lblTimeWindow
			// 
			this.lblTimeWindow.AutoSize = true;
			this.lblTimeWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTimeWindow.Location = new System.Drawing.Point(52, 14);
			this.lblTimeWindow.Name = "lblTimeWindow";
			this.lblTimeWindow.Size = new System.Drawing.Size(155, 20);
			this.lblTimeWindow.TabIndex = 2;
			this.lblTimeWindow.Text = "Mar 18 - 24, 2012 ";
			// 
			// btnPrevious
			// 
			this.btnPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnPrevious.Location = new System.Drawing.Point(6, 9);
			this.btnPrevious.Name = "btnPrevious";
			this.btnPrevious.Size = new System.Drawing.Size(31, 23);
			this.btnPrevious.TabIndex = 0;
			this.btnPrevious.Text = "<-";
			this.btnPrevious.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(71, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "show current:";
			// 
			// linkLabelYear
			// 
			this.linkLabelYear.AutoSize = true;
			this.linkLabelYear.Location = new System.Drawing.Point(80, 39);
			this.linkLabelYear.Name = "linkLabelYear";
			this.linkLabelYear.Size = new System.Drawing.Size(27, 13);
			this.linkLabelYear.TabIndex = 4;
			this.linkLabelYear.TabStop = true;
			this.linkLabelYear.Text = "year";
			// 
			// linkLabelMonth
			// 
			this.linkLabelMonth.AutoSize = true;
			this.linkLabelMonth.Location = new System.Drawing.Point(113, 39);
			this.linkLabelMonth.Name = "linkLabelMonth";
			this.linkLabelMonth.Size = new System.Drawing.Size(36, 13);
			this.linkLabelMonth.TabIndex = 5;
			this.linkLabelMonth.TabStop = true;
			this.linkLabelMonth.Text = "month";
			// 
			// linkLabelWeek
			// 
			this.linkLabelWeek.AutoSize = true;
			this.linkLabelWeek.Location = new System.Drawing.Point(155, 39);
			this.linkLabelWeek.Name = "linkLabelWeek";
			this.linkLabelWeek.Size = new System.Drawing.Size(33, 13);
			this.linkLabelWeek.TabIndex = 6;
			this.linkLabelWeek.TabStop = true;
			this.linkLabelWeek.Text = "week";
			// 
			// linkLabelDay
			// 
			this.linkLabelDay.AutoSize = true;
			this.linkLabelDay.Location = new System.Drawing.Point(194, 39);
			this.linkLabelDay.Name = "linkLabelDay";
			this.linkLabelDay.Size = new System.Drawing.Size(24, 13);
			this.linkLabelDay.TabIndex = 7;
			this.linkLabelDay.TabStop = true;
			this.linkLabelDay.Text = "day";
			// 
			// TimeWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.linkLabelDay);
			this.Controls.Add(this.linkLabelWeek);
			this.Controls.Add(this.linkLabelMonth);
			this.Controls.Add(this.linkLabelYear);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lblTimeWindow);
			this.Controls.Add(this.btnNext);
			this.Controls.Add(this.btnPrevious);
			this.Name = "TimeWindow";
			this.Size = new System.Drawing.Size(254, 66);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.Label lblTimeWindow;
		private System.Windows.Forms.Button btnPrevious;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel linkLabelYear;
		private System.Windows.Forms.LinkLabel linkLabelMonth;
		private System.Windows.Forms.LinkLabel linkLabelWeek;
		private System.Windows.Forms.LinkLabel linkLabelDay;

	}
}
