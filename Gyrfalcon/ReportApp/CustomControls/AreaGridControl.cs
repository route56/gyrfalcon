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
	public partial class AreaGridControl : UserControl
	{
		public AreaGridControl()
		{
			InitializeComponent();
		}

		public List<AreaGridControlDataFormat> AreaGridData { get; set; }
	}

	public class AreaGridControlDataFormat
	{
		public string GroupBy { get; set; }
		public string TimeSpan { get; set; }
		public string Activity { get; set; }
	}
}
