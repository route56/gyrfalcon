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
	public partial class AreaGridControl : UserControl
	{
		private IEnumerable<GroupedDataFormat> _areaGridData;
		public AreaGridControl()
		{
			InitializeComponent();
		}

		public IEnumerable<GroupedDataFormat> AreaGridData
		{
			get { return _areaGridData; }
			set
			{
				_areaGridData = value;
				areaGridControlDataFormatBindingSource.DataSource = _areaGridData;
			}
		}
	}
}
