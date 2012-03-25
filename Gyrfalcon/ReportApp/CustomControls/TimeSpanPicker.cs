using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportApp.CustomControls
{
	class TimeSpanPicker
	{
		internal System.Windows.Forms.Control[] NewTimeSpan()
		{
			this.btnPrevious = new System.Windows.Forms.Button();
			this.lblTimeSpan = new System.Windows.Forms.Label();
			this.btnNext = new System.Windows.Forms.Button();
			this.lblShowCurrent = new System.Windows.Forms.Label();
			this.linkLabelYear = new System.Windows.Forms.LinkLabel();
			this.linkLabelMonth = new System.Windows.Forms.LinkLabel();
			this.linkLabelWeek = new System.Windows.Forms.LinkLabel();
			this.linkLabelDay = new System.Windows.Forms.LinkLabel();
			// 
			// btnPrevious
			// 
			this.btnPrevious.Location = new System.Drawing.Point(12, 12);
			this.btnPrevious.Name = "btnPrevious";
			this.btnPrevious.Size = new System.Drawing.Size(34, 23);
			this.btnPrevious.TabIndex = 2;
			this.btnPrevious.Text = "<-";
			this.btnPrevious.UseVisualStyleBackColor = true;
			// 
			// lblTimeSpan
			// 
			this.lblTimeSpan.AutoSize = true;
			this.lblTimeSpan.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTimeSpan.Location = new System.Drawing.Point(68, 17);
			this.lblTimeSpan.Name = "lblTimeSpan";
			this.lblTimeSpan.Size = new System.Drawing.Size(127, 18);
			this.lblTimeSpan.TabIndex = 3;
			this.lblTimeSpan.Text = "Fri, Mar 2, 2012";
			// 
			// btnNext
			// 
			this.btnNext.Location = new System.Drawing.Point(219, 12);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(34, 23);
			this.btnNext.TabIndex = 4;
			this.btnNext.Text = "->";
			this.btnNext.UseVisualStyleBackColor = true;
			// 
			// lblShowCurrent
			// 
			this.lblShowCurrent.AutoSize = true;
			this.lblShowCurrent.Location = new System.Drawing.Point(9, 52);
			this.lblShowCurrent.Name = "lblShowCurrent";
			this.lblShowCurrent.Size = new System.Drawing.Size(70, 13);
			this.lblShowCurrent.TabIndex = 5;
			this.lblShowCurrent.Text = "Show current";
			// 
			// linkLabelYear
			// 
			this.linkLabelYear.AutoSize = true;
			this.linkLabelYear.Location = new System.Drawing.Point(85, 52);
			this.linkLabelYear.Name = "linkLabelYear";
			this.linkLabelYear.Size = new System.Drawing.Size(27, 13);
			this.linkLabelYear.TabIndex = 6;
			this.linkLabelYear.TabStop = true;
			this.linkLabelYear.Text = "year";
			// 
			// linkLabelMonth
			// 
			this.linkLabelMonth.AutoSize = true;
			this.linkLabelMonth.Location = new System.Drawing.Point(118, 52);
			this.linkLabelMonth.Name = "linkLabelMonth";
			this.linkLabelMonth.Size = new System.Drawing.Size(36, 13);
			this.linkLabelMonth.TabIndex = 7;
			this.linkLabelMonth.TabStop = true;
			this.linkLabelMonth.Text = "month";
			// 
			// linkLabelWeek
			// 
			this.linkLabelWeek.AutoSize = true;
			this.linkLabelWeek.Location = new System.Drawing.Point(162, 52);
			this.linkLabelWeek.Name = "linkLabelWeek";
			this.linkLabelWeek.Size = new System.Drawing.Size(33, 13);
			this.linkLabelWeek.TabIndex = 8;
			this.linkLabelWeek.TabStop = true;
			this.linkLabelWeek.Text = "week";
			// 
			// linkLabelDay
			// 
			this.linkLabelDay.AutoSize = true;
			this.linkLabelDay.Location = new System.Drawing.Point(201, 52);
			this.linkLabelDay.Name = "linkLabelDay";
			this.linkLabelDay.Size = new System.Drawing.Size(24, 13);
			this.linkLabelDay.TabIndex = 9;
			this.linkLabelDay.TabStop = true;
			this.linkLabelDay.Text = "day";

			// 
			// Return controls
			// 
			List<System.Windows.Forms.Control> controls = new List<System.Windows.Forms.Control>();

			controls.Add(this.linkLabelDay);
			controls.Add(this.linkLabelWeek);
			controls.Add(this.linkLabelMonth);
			controls.Add(this.linkLabelYear);
			controls.Add(this.lblShowCurrent);
			controls.Add(this.btnNext);
			controls.Add(this.lblTimeSpan);
			controls.Add(this.btnPrevious);

			return controls.ToArray();
		}

		private System.Windows.Forms.Button btnPrevious;
		private System.Windows.Forms.Label lblTimeSpan;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.Label lblShowCurrent;
		private System.Windows.Forms.LinkLabel linkLabelYear;
		private System.Windows.Forms.LinkLabel linkLabelMonth;
		private System.Windows.Forms.LinkLabel linkLabelWeek;
		private System.Windows.Forms.LinkLabel linkLabelDay;
	}
}
