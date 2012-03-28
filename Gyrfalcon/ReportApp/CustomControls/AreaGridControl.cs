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
		private List<AreaGridControlDataFormat> _areaGridData;
		public AreaGridControl()
		{
			InitializeComponent();
		}

		public List<AreaGridControlDataFormat> AreaGridData
		{
			get { return _areaGridData; }
			set
			{
				_areaGridData = value;
				areaGridControlDataFormatBindingSource.DataSource = _areaGridData;
			}
		}
	}

	public class AreaGridControlDataFormat
	{
		public string GroupBy { get; set; }
		public long TimeSpan { get; set; }
		public string Activity { get; set; }
	}
}
