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
	public partial class BarGridControl : UserControl
	{
		public BarGridControl()
		{
			InitializeComponent();
		}

		public List<BarGridControlDataFormat> BarChartData { get; set; }
	}

	public class BarGridControlDataFormat
	{
		public int Rank { get; set; }
		public string TimeSpan { get; set; }
		public string Activity { get; set; }
	}
}
