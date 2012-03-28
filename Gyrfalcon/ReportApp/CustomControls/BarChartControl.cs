using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ReportApp.CustomControls
{
	public partial class BarChartControl : UserControl
	{
		private List<BarGridControlDataFormat> _barGrid;
		public BarChartControl()
		{
			InitializeComponent();
		}

		public List<BarGridControlDataFormat> BarChartData
		{
			get { return _barGrid; }
			set
			{
				_barGrid = value;

				if (_barGrid != null)
				{
					chart1.Series.Clear();

					// Create a data series
					Series series1 = new Series();

					foreach (var item in _barGrid)
					{
						series1.Points.Add(new DataPoint() { AxisLabel = item.Activity, YValues = new double[] { item.TimeSpan } });
					}

					chart1.Series.Add(series1);
				}
			}
		}
	}
}
