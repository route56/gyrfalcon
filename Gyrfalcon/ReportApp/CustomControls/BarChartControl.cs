using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using DataStore;

namespace ReportApp.CustomControls
{
	public partial class BarChartControl : UserControl
	{
		private IEnumerable<RankedDataFormat> _barGrid;
		private int _size = 10;

		public BarChartControl()
		{
			InitializeComponent();
		}

		public IEnumerable<RankedDataFormat> BarChartData
		{
			get { return _barGrid; }
			set
			{
				_barGrid = value;

				if (_barGrid != null)
				{
					_barGrid = _barGrid.Take(_size);

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

		public int BarSize 
		{
			get
			{
				return _size;
			}
			set
			{
				if (value > 0)
				{
					_size = value;
				}
			}
		}
	}
}
