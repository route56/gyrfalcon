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
using Common.TimeWindow;

namespace ReportApp.CustomControls
{
	public partial class AreaChartControl : UserControl
	{
		private IEnumerable<GroupedDataFormat> _areaGrid;
		public AreaChartControl()
		{
			InitializeComponent();
		}

		public IEnumerable<GroupedDataFormat> AreaGridData 
		{
			get { return _areaGrid; }
			set
			{
				_areaGrid = value;

				if (_areaGrid != null)
				{
					chart1.Series.Clear();
					chart1.Legends.Clear();

					Dictionary<string, List<FlatGroupedDataFormat>> map = new Dictionary<string, List<FlatGroupedDataFormat>>();

					foreach (var item in FlatGroupedDataFormat.ConvertFromGroupedDataFormat(_areaGrid))
					{
						if (map.ContainsKey(item.Activity))
						{
							map[item.Activity].Add(item);
						}
						else
						{
							map[item.Activity] = new List<FlatGroupedDataFormat>() { item };
						}
					}

					foreach (var activity in map.Keys)
					{
						Series series1 = new Series();
						foreach (var data in map[activity])
						{
							series1.Points.Add(new DataPoint()
							{
								AxisLabel = GetAxisString(data),
								YValues = new double[] { data.TimeSpan }
							});
						}

						series1.LegendText = activity;
						chart1.Series.Add(series1);
						chart1.Legends.Add(new Legend() { Name = activity, Docking = Docking.Bottom });
					}

					chart1.AlignDataPointsByAxisLabel();
					foreach (var series1 in chart1.Series)
					{
						series1.ChartType = SeriesChartType.StackedArea;
					}
				}
			}
		}

		private static string GetAxisString(FlatGroupedDataFormat data)
		{
			switch (data.GroupWindow)
			{
				case GroupWindowType.Hour:
					return data.GroupBy.ToShortDateString() + " " + data.GroupBy.Hour;
				case GroupWindowType.Day:
					return data.GroupBy.ToShortDateString();
				case GroupWindowType.Week:
					ITimeWindow window = new DayTimeWindow(data.GroupBy);
					window = window.ToWeekWindow();
					return window.StartTime.ToShortDateString() + " " + window.EndTime.ToShortDateString();
				default:
					throw new ArgumentException();
			}
		}
	}
}
