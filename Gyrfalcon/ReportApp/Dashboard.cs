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

namespace ReportApp
{
	class MockService
	{
		static bool flip = false;

		internal List<AreaGridControlDataFormat> GetAreaGridData(DateTime _startTime, DateTime _endTime)
		{
			flip = !flip;

			if (flip)
			{
				return AreaOne();
			}
			else
			{
				return AreaTwo();
			}
		}

		internal List<AreaGridControlDataFormat> AreaOne()
		{
			return new List<AreaGridControlDataFormat>()
				{
					new AreaGridControlDataFormat()
					{
						GroupBy = "Day 1",
						TimeSpan = "2h 3m",
						Activity = "Visual studio"
					},
					new AreaGridControlDataFormat()
					{
						GroupBy = "Day 1",
						TimeSpan = "1h 3m",
						Activity = "Chrome"
					},
					new AreaGridControlDataFormat()
					{
						GroupBy = "Day 2",
						TimeSpan = "30m",
						Activity = "Visual studio"
					},
					new AreaGridControlDataFormat()
					{
						GroupBy = "Day 3",
						TimeSpan = "2h 3m",
						Activity = "Visual studio"
					},
					new AreaGridControlDataFormat()
					{
						GroupBy = "Day 3",
						TimeSpan = "1h 30m",
						Activity = "Chess"
					}
				};
		}

		internal List<AreaGridControlDataFormat> AreaTwo()
		{
			return new List<AreaGridControlDataFormat>()
				{
					new AreaGridControlDataFormat()
					{
						GroupBy = "Day 4",
						TimeSpan = "4h",
						Activity = "Flipped"
					},
					new AreaGridControlDataFormat()
					{
						GroupBy = "Day 4",
						TimeSpan = "2h",
						Activity = "Chrome Flipped"
					},
					new AreaGridControlDataFormat()
					{
						GroupBy = "Day 5",
						TimeSpan = "30m",
						Activity = "Visual studio Flipped"
					},
					new AreaGridControlDataFormat()
					{
						GroupBy = "Day 5",
						TimeSpan = "2h 3m",
						Activity = "Visual studio Flipped"
					},
					new AreaGridControlDataFormat()
					{
						GroupBy = "Day 6",
						TimeSpan = "1h 30m",
						Activity = "Chess Flipped"
					}
				};
		}

		internal List<BarGridControlDataFormat> GetBarGridData(DateTime _startTime, DateTime _endTime)
		{
			if (flip)
			{
				return BarOne();
			}
			else
			{
				return BarTwo();
			}
		}

		internal List<BarGridControlDataFormat> BarOne()
		{
			return new List<BarGridControlDataFormat>()
			{
				new BarGridControlDataFormat()
				{
					Rank = 1,
					TimeSpan = "1h 2m",
					Activity = "hjge"
				},
				new BarGridControlDataFormat()
				{
					Rank = 2,
					TimeSpan = "1h 2m",
					Activity = "qsc"
				},
				new BarGridControlDataFormat()
				{
					Rank = 3,
					TimeSpan = "1h 2m",
					Activity = "gfd"
				},
				new BarGridControlDataFormat()
				{
					Rank = 4,
					TimeSpan = "1h 2m",
					Activity = "asd"
				},
				new BarGridControlDataFormat()
				{
					Rank = 5,
					TimeSpan = "1h 2m",
					Activity = "qwe"
				},
				new BarGridControlDataFormat()
				{
					Rank = 6,
					TimeSpan = "1h 2m",
					Activity = "JALJS"
				}
			};
		}

		internal List<BarGridControlDataFormat> BarTwo()
		{
			return new List<BarGridControlDataFormat>()
			{
				new BarGridControlDataFormat()
				{
					Rank = 1,
					TimeSpan = "1h 2m Flipped",
					Activity = "hjge Flipped"
				},
				new BarGridControlDataFormat()
				{
					Rank = 2,
					TimeSpan = "1h 2m Flipped",
					Activity = "qsc Flipped"
				},

				new BarGridControlDataFormat()
				{
					Rank = 3,
					TimeSpan = "Flipped 1h 2m",
					Activity = "asd"
				},
				new BarGridControlDataFormat()
				{
					Rank = 4,
					TimeSpan = "1h 2m",
					Activity = "qwe Flipped"
				},
				new BarGridControlDataFormat()
				{
					Rank = 5,
					TimeSpan = "Flipped 1h 2m",
					Activity = "JALJS"
				}
			};
		}
	}

	public partial class Dashboard : Form
	{
		private MockService _mockService;
		private DateTime _startTime;
		private DateTime _endTime;
		private string _summary;
		private string _barName;
		private string _groupByName;

		public Dashboard()
		{
			InitializeComponent();
			DateTime now = DateTime.Now;

			combinedControl1.StartTime = _startTime = now.AddDays(-1);
			combinedControl1.EndTime = _endTime = now;

			combinedControl1.Summary = _summary = "Activity chart";
			combinedControl1.BarName = _barName = "all activity";
			combinedControl1.GroupByName = _groupByName = "by day";

			combinedControl1.AreaGridData = _mockService.GetAreaGridData(_startTime, _endTime);
			combinedControl1.BarGridData = _mockService.GetBarGridData(_startTime, _endTime);

			combinedControl1.TimeWindowChanged += new EventHandler(combinedControl1_TimeWindowChanged);

			combinedControl1.Refresh();
			// this.Controls.AddRange(new TimeSpanPicker().NewTimeSpan());

			//this.Controls.AddRange(new System.Windows.Forms.Control[] { new GridData().NewGrid() });

			//this.Controls.AddRange(new System.Windows.Forms.Control[] { new BarChart().NewChart() });
		}

		void combinedControl1_TimeWindowChanged(object sender, EventArgs e)
		{
			_startTime = combinedControl1.StartTime;
			_endTime = combinedControl1.EndTime;

			combinedControl1.AreaGridData = _mockService.GetAreaGridData(_startTime, _endTime);
			combinedControl1.BarGridData = _mockService.GetBarGridData(_startTime, _endTime);

			combinedControl1.Refresh();
		}
	}
}
