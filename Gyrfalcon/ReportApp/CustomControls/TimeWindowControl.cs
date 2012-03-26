using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReportApp.CustomControls
{
	public partial class TimeWindowControl : UserControl
	{
		public TimeWindowControl()
		{
			InitializeComponent();
		}

		public override void Refresh()
		{
			base.Refresh();

			TimeSpan ts = EndTime.Subtract(StartTime);

			// Unit test needed.
			if (ts.Days == 0)
			{
				// day case
			}
			else if (ts.Days < 7)
			{
				// week case
			}
			else if(ts.Days < 29)
			{
				// month case
			}
			else
			{
				// year case.
			}
		}

		public DateTime StartTime 
		{
			get
			{
				return _startTime;
			}
			set
			{
				// keep only date. no time.
				_startTime = new DateTime(value.Year, value.Month, value.Day);
			}
		}

		public DateTime EndTime 
		{
			get
			{
				return _endTime;
			}
			set
			{
				// end time max
				_endTime = new DateTime(value.Year, value.Month, value.Day, 23, 59, 59, 999);
			}
		}

		public event EventHandler TimeWindowChanged;
		private DateTime _startTime;
		private DateTime _endTime;
	}
}
