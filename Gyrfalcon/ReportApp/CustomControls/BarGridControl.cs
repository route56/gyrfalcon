using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataStore;

namespace ReportApp.CustomControls
{
	public partial class BarGridControl : UserControl
	{
		private IEnumerable<RankedDataFormat> _barGridData;
		public BarGridControl()
		{
			InitializeComponent();
		}

		public IEnumerable<RankedDataFormat> BarChartData
		{
			get { return _barGridData; }
			set
			{
				_barGridData = value;
				barGridControlDataFormatBindingSource.DataSource = _barGridData;
			}
		}
	}
}
