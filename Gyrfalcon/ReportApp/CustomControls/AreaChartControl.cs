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
	public partial class AreaChartControl : UserControl
	{
		public AreaChartControl()
		{
			InitializeComponent();
		}

		public List<AreaGridControlDataFormat> AreaGridData { get; set; }
	}
}
