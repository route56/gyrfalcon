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

					Dictionary<string, List<Tuple<DateTime, long>>> map = ConvertGroupedDataFormatToSeriesLegendFormat();

					foreach (var activity in map.Keys)
					{
						Series series1 = new Series();
						foreach (var data in map[activity])
						{
							series1.Points.Add(new DataPoint() { AxisLabel = data.Item1.ToShortDateString(), YValues = new double[] { data.Item2 } });
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

		/// <summary>
		/// Logic to convert groupeddataformat to series and legends for chart
		/// </summary>
		/// <returns></returns>
		private Dictionary<string, List<Tuple<DateTime, long>>> ConvertGroupedDataFormatToSeriesLegendFormat()
		{
			// TODO write UT and refactor this region
			Dictionary<string, List<Tuple<DateTime, long>>> map = new Dictionary<string, List<Tuple<DateTime, long>>>();

			foreach (var item in _areaGrid)
			{
				for (int i = 0; i < item.Activity.Length; i++)
				{
					if (map.ContainsKey(item.Activity[i]))
					{
						map[item.Activity[i]].Add(new Tuple<DateTime, long>(item.GroupBy, item.TimeSpan[i]));
					}
					else
					{
						map[item.Activity[i]] = new List<Tuple<DateTime, long>>() { new Tuple<DateTime, long>(item.GroupBy, item.TimeSpan[i]) };
					}
				}
			}
			return map;
		}
	}
}
