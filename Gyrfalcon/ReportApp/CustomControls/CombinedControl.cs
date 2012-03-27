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
	public partial class CombinedControl : UserControl
	{
		public CombinedControl()
		{
			InitializeComponent();

			timeWindow1.TimeWindowChanged += new EventHandler(timeWindow1_Changed);
		}

		void timeWindow1_Changed(object sender, EventArgs e)
		{
			if (TimeWindowChanged != null)
			{
				TimeWindowChanged(sender, e);
			}
		}

		// Inconsistent pattern for refresh. Calling for timewindow but not for area and bar, grid,chart
		public override void Refresh()
		{
			base.Refresh();

			timeWindow1.Refresh();

			areaChartControl1.AreaGridData = AreaGridData;
			areaGridControl1.AreaGridData = AreaGridData;

			barChartControl1.BarChartData = BarGridData;
			barGridControl1.BarChartData = BarGridData;
		}

		public string Summary { get; set; }

		public string BarName { get; set; }

		public string GroupByName { get; set; }

		public List<AreaGridControlDataFormat> AreaGridData { get; set; }

		public List<BarGridControlDataFormat> BarGridData { get; set; }

		public event EventHandler TimeWindowChanged;

		public DateTime Start
		{
			get { return timeWindow1.Start; }
			set
			{
				timeWindow1.Start = value;
			}
		}

		public DateTime End
		{
			get { return timeWindow1.End; }
		}
	}
}
