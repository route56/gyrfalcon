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
					// Below resets axis. http://www.xtremevbtalk.com/archive/index.php/t-321203.html
					chart1.ChartAreas[0].AxisY.Maximum = double.NaN;
					chart1.ChartAreas[0].AxisY.Minimum = double.NaN;

					if (_barGrid.Count() == 0)
					{
						return;
					}

					var avg = _barGrid.Average(s => s.TimeSpan);

					long factor = 1;

					if (avg > 60 * 60)
					{
						factor = 60 * 60;
						chart1.ChartAreas[0].AxisY.LabelStyle.Format = "{0} hr";
					}
					else if (avg > 60)
					{
						factor = 60;
						chart1.ChartAreas[0].AxisY.LabelStyle.Format = "{0} min";
					}
					else
					{
						factor = 1;
						chart1.ChartAreas[0].AxisY.LabelStyle.Format = "{0} sec";
					}

					// Create a data series
					Series series1 = new Series();

					foreach (var item in _barGrid)
					{
						series1.Points.Add(new DataPoint() 
							{ 
								AxisLabel = item.Activity,
								YValues = new double[] { item.TimeSpan / factor }
							});
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
