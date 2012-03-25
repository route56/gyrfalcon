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
			StartTime = timeWindow1.StartTime;
			EndTime = timeWindow1.EndTime;

			if (TimeWindowChanged != null)
			{
				TimeWindowChanged(sender, e);
			}
		}

		// Inconsistent pattern for refresh. Calling for timewindow but not for area and bar, grid,chart
		public override void Refresh()
		{
			base.Refresh();

			timeWindow1.StartTime = StartTime;
			timeWindow1.EndTime = EndTime;

			timeWindow1.Refresh();

			areaChartControl1.AreaGridData = AreaGridData;
			areaGridControl1.AreaGridData = AreaGridData;

			barChartControl1.BarChartData = BarGridData;
			barGridControl1.BarChartData = BarGridData;
		}

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public string Summary { get; set; }

		public string BarName { get; set; }

		public string GroupByName { get; set; }

		public List<AreaGridControlDataFormat> AreaGridData { get; set; }

		public List<BarGridControlDataFormat> BarGridData { get; set; }

		public event EventHandler TimeWindowChanged;
	}
}
