using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MockGyrfalconService;
using System.Windows.Forms.DataVisualization.Charting;
using ReportApp.CustomControls;
using DataStore;
using DataStore.Contract;

namespace ReportApp
{
	public partial class Dashboard : Form
	{
		private IQueryStore _queryStore;
		private DateTime _startTime;
		private DateTime _endTime;
		private string _summary = "Activity chart";
		private string _barName = "all activity";
		private string _groupByName = "by day";

		public Dashboard()
		{
			InitializeComponent();

			combinedControl1.Start = DateTime.Now;

			_startTime = combinedControl1.Start;
			_endTime = combinedControl1.End;

			combinedControl1.Summary = _summary;
			combinedControl1.BarName = _barName;
			combinedControl1.GroupByName = _groupByName;

			_queryStore = new BigTableDataStore.QueryStore();
				//new QueryStore();
			combinedControl1.AreaGridData = _queryStore.GetGroupedData(_startTime, _endTime);
			combinedControl1.BarGridData = _queryStore.GetRankedData(_startTime, _endTime);

			combinedControl1.TimeWindowChanged += new EventHandler(combinedControl1_TimeWindowChanged);

			combinedControl1.Refresh();
		}

		void combinedControl1_TimeWindowChanged(object sender, EventArgs e)
		{
			_startTime = combinedControl1.Start;
			_endTime = combinedControl1.End;

			combinedControl1.AreaGridData = _queryStore.GetGroupedData(_startTime, _endTime);
			combinedControl1.BarGridData = _queryStore.GetRankedData(_startTime, _endTime);

			combinedControl1.Refresh();
		}
	}
}
