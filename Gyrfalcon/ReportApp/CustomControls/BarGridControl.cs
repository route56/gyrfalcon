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
		private List<BarGridControlDataFormat> _barGridData;
		public BarGridControl()
		{
			InitializeComponent();
		}

		public List<BarGridControlDataFormat> BarChartData
		{
			get { return _barGridData; }
			set
			{
				_barGridData = value;
				barGridControlDataFormatBindingSource.DataSource = _barGridData;
			}
		}
	}

	public class BarGridControlDataFormat
	{
		public int Rank { get; set; }
		public long TimeSpan { get; set; }
		public string Activity { get; set; }
	}
}
