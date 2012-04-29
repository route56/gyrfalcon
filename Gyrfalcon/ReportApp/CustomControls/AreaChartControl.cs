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
		private IEnumerable<string> _top10Ranked;

		public AreaChartControl()
		{
			InitializeComponent();
		}

		private static string GetAxisString(DateTime time, GroupWindowType type)
		{
			switch (type)
			{
				case GroupWindowType.Hour:
					return time.ToShortDateString() + " " + time.Hour;
				case GroupWindowType.Day:
					return time.ToShortDateString();
				case GroupWindowType.Week:
					ITimeWindow window = new DayTimeWindow(time);
					window = window.ToWeekWindow();
					return window.StartTime.ToShortDateString() + " " + window.EndTime.ToShortDateString();
				default:
					throw new ArgumentException();
			}
		}

		internal void SetAreaGridData(IEnumerable<GroupedDataFormat> areaGridData, IEnumerable<RankedDataFormat> barGridData)
		{
			if (areaGridData == null || barGridData == null)
			{
				throw new ArgumentNullException();
			}

			_top10Ranked = barGridData.Take(10).Select(s => s.Activity);

			_areaGrid = areaGridData;

			chart1.Series.Clear();
			chart1.Legends.Clear();
			// Below resets axis. http://www.xtremevbtalk.com/archive/index.php/t-321203.html
			chart1.ChartAreas[0].AxisY.Maximum = double.NaN;
			chart1.ChartAreas[0].AxisY.Minimum = double.NaN;

			Dictionary<string, List<FlatGroupedDataFormat>> map = new Dictionary<string, List<FlatGroupedDataFormat>>();
			
			var flatList = FlatGroupedDataFormat.ConvertFromGroupedDataFormat(_areaGrid)
								.Where(s => _top10Ranked.Contains(s.Activity));

			if (flatList.Count() == 0)
			{
				return;
			}

			var min = flatList.Min(s => s.GroupBy);
			var max = flatList.Max(s => s.GroupBy);
			var avg = flatList.Average(s => s.TimeSpan);
			var type = flatList.ElementAtOrDefault(0).GroupWindow;
			long factor = 1;

			if (avg > 60*60)
			{
				factor = 60 * 60;
				chart1.ChartAreas[0].AxisY.LabelStyle.Format = "{0} hr";
			}
			else if( avg > 60)
			{
				factor = 60;
				chart1.ChartAreas[0].AxisY.LabelStyle.Format = "{0} min";
			}
			else
			{
				factor = 1;
				chart1.ChartAreas[0].AxisY.LabelStyle.Format = "{0} sec";
			}

			foreach (var item in flatList)
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
						AxisLabel = GetAxisString(data.GroupBy, data.GroupWindow),
						YValues = new double[] { data.TimeSpan / factor }
					});
				}

				series1.LegendText = activity;
				chart1.Series.Add(series1);
				chart1.Legends.Add(new Legend() { Name = activity, Docking = Docking.Bottom });
			}

			Series zeroSeries = new Series();

			for (DateTime curr = min; curr <= max; curr = GetNext(curr, type))
			{
				zeroSeries.Points.Add(new DataPoint()
				{
					AxisLabel = GetAxisString(curr, type),
					YValues = new double[] { 0 }
				});
			}

			zeroSeries.LegendText = "";
			chart1.Series.Add(zeroSeries);
			chart1.Legends.Add(new Legend() { Name = "", Docking = Docking.Bottom });

			chart1.AlignDataPointsByAxisLabel();
			foreach (var series in chart1.Series)
			{
				series.ChartType = SeriesChartType.StackedArea;
			}
		}

		private DateTime GetNext(DateTime curr, GroupWindowType type)
		{
			switch (type)
			{
				case GroupWindowType.Hour:
					return curr.AddHours(1);
				case GroupWindowType.Day:
					return curr.AddDays(1);
				case GroupWindowType.Week:
					return curr.AddDays(7);
				default:
					throw new ArgumentException();
			}
		}
	}
}
