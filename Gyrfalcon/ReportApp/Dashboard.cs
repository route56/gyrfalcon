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

			combinedControl1.Start = DateTime.Now;

			combinedControl1.Summary = _summary = "Activity chart";
			combinedControl1.BarName = _barName = "all activity";
			combinedControl1.GroupByName = _groupByName = "by day";

			_mockService = new MockService();
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
			_startTime = combinedControl1.Start;
			_endTime = combinedControl1.End;

			combinedControl1.AreaGridData = _mockService.GetAreaGridData(_startTime, _endTime);
			combinedControl1.BarGridData = _mockService.GetBarGridData(_startTime, _endTime);

			combinedControl1.Refresh();
		}
	}

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
						TimeSpan = 20,
						Activity = "Visual studio"
					},
					new AreaGridControlDataFormat()
					{
						GroupBy = "Day 1",
						TimeSpan = 10,
						Activity = "Chrome"
					},
					new AreaGridControlDataFormat()
					{
						GroupBy = "Day 2",
						TimeSpan = 5,
						Activity = "Visual studio"
					},
					new AreaGridControlDataFormat()
					{
						GroupBy = "Day 3",
						TimeSpan = 21,
						Activity = "Visual studio"
					},
					new AreaGridControlDataFormat()
					{
						GroupBy = "Day 3",
						TimeSpan = 10,
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
						TimeSpan = 30,
						Activity = "Flipped"
					},
					new AreaGridControlDataFormat()
					{
						GroupBy = "Day 4",
						TimeSpan = 20,
						Activity = "Chrome Flipped"
					},
					new AreaGridControlDataFormat()
					{
						GroupBy = "Day 5",
						TimeSpan = 5,
						Activity = "Visual studio Flipped"
					},
					new AreaGridControlDataFormat()
					{
						GroupBy = "Day 5",
						TimeSpan = 20,
						Activity = "Chrome"
					},
					new AreaGridControlDataFormat()
					{
						GroupBy = "Day 6",
						TimeSpan = 10,
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
					TimeSpan = 60,
					Activity = "hjge"
				},
				new BarGridControlDataFormat()
				{
					Rank = 2,
					TimeSpan = 50,
					Activity = "qsc"
				},
				new BarGridControlDataFormat()
				{
					Rank = 3,
					TimeSpan = 45,
					Activity = "gfd"
				},
				new BarGridControlDataFormat()
				{
					Rank = 4,
					TimeSpan = 30,
					Activity = "asd"
				},
				new BarGridControlDataFormat()
				{
					Rank = 5,
					TimeSpan = 25,
					Activity = "qwe"
				},
				new BarGridControlDataFormat()
				{
					Rank = 6,
					TimeSpan = 20,
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
					TimeSpan = 44,
					Activity = "hjge Flipped"
				},
				new BarGridControlDataFormat()
				{
					Rank = 2,
					TimeSpan = 34,
					Activity = "qsc Flipped"
				},

				new BarGridControlDataFormat()
				{
					Rank = 3,
					TimeSpan = 29,
					Activity = "asd"
				},
				new BarGridControlDataFormat()
				{
					Rank = 4,
					TimeSpan = 20,
					Activity = "qwe Flipped"
				},
				new BarGridControlDataFormat()
				{
					Rank = 5,
					TimeSpan = 10,
					Activity = "JALJS"
				}
			};
		}
	}

}
