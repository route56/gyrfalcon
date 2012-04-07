using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.TimeWindow;

namespace DataStore.Test
{
	[TestClass]
	public class LINQTestPad
	{
		GroupedDataFormat gdf = new GroupedDataFormat()
		{
			GroupBy = DateTime.Now,
			Activity = new[] { "Foo", "Bar" },
			TimeSpan = new long[] { 10, 20 } // aggregated over start - end, but grouped by group by
		};

		RankedDataFormat rdf = new RankedDataFormat()
		{
			Rank = 1, // computed by sorting on TimeSpan
			Activity = "Bar",
			TimeSpan = 20 // aggregated over start - end
		};

		DataAtom atom = new DataAtom()
		{
			Time = DateTime.Now, // filter
			Process = "Fooo", // activity
			Title = "Foo title", // not bubbled yet
			Frequency = 10 // aggregated
		};

		List<DataAtom> hourSummary = new List<DataAtom>();
		List<DataAtom> daySummary = new List<DataAtom>();
		List<DataAtom> weekSummary = new List<DataAtom>();

		[TestMethod]
		public void Basic_ListAtom_RankedDataFormat()
		{
			#region Setup
			List<DataAtom> list = new List<DataAtom>()
			{
				new DataAtom()
				{
					Time = DateTime.Now, // filter
					Process = "Fooo", // activity
					Title = "Foo title", // not bubbled yet
					Frequency = 10 // aggregated
				},
				new DataAtom()
				{
					Time = DateTime.Now, // filter
					Process = "Bar", // activity
					Title = "Foo title", // not bubbled yet
					Frequency = 20 // aggregated
				},
				new DataAtom()
				{
					Time = DateTime.Now, // filter
					Process = "Bar", // activity
					Title = "Foo title", // not bubbled yet
					Frequency = 20 // aggregated
				},
				new DataAtom()
				{
					Time = DateTime.Now, // filter
					Process = "Qux", // activity
					Title = "Foo title", // not bubbled yet
					Frequency = 30 // aggregated
				},
				new DataAtom()
				{
					Time = DateTime.Now, // filter
					Process = "Qux", // activity
					Title = "Foo title", // not bubbled yet
					Frequency = 30 // aggregated
				}

			};

			List<RankedDataFormat> expected = new List<RankedDataFormat>()
			{
				new RankedDataFormat()
				{
					Rank = 1, // computed by sorting on TimeSpan
					Activity = "Qux",
					TimeSpan = 60 // aggregated over start - end
				},
				new RankedDataFormat()
				{
					Rank = 2, // computed by sorting on TimeSpan
					Activity = "Bar",
					TimeSpan = 40 // aggregated over start - end
				},
				new RankedDataFormat()
				{
					Rank = 3, // computed by sorting on TimeSpan
					Activity = "Fooo",
					TimeSpan = 10 // aggregated over start - end
				}
			};
			#endregion

			var ans = new DataFormatConvertor(list).ToRankedDataFormat();

			#region Verify
			var actual = new List<RankedDataFormat>(ans);

			for (int i = 0; i < actual.Count; i++)
			{
				actual[i].Rank = i + 1;
			}

			Assert.AreEqual(expected.Count, actual.Count);
			for (int i = 0; i < actual.Count; i++)
			{
				Assert.AreEqual(expected[i].Rank, actual[i].Rank);
				Assert.AreEqual(expected[i].TimeSpan, actual[i].TimeSpan);
				Assert.AreEqual(expected[i].Activity, actual[i].Activity);
			}
			#endregion
		}

		[TestMethod]
		public void Basic_ListAtom_GroupedDataFormat()
		{
			#region Setup
			DateTime date = new DateTime(2012,1,1,1,1,1);

			List<DataAtom> list = new List<DataAtom>()
			{
				new DataAtom()
				{
					Time = date.AddHours(1),
					Process = "Foo", // activity
					Title = "Foo title", // not bubbled yet
					Frequency = 10 // aggregated
				},
				new DataAtom()
				{
					Time = date.AddHours(1),
					Process = "Bar", // activity
					Title = "Foo title", // not bubbled yet
					Frequency = 20 // aggregated
				},
				new DataAtom()
				{
					Time = date.AddHours(2),
					Process = "Qux", // activity
					Title = "Foo title", // not bubbled yet
					Frequency = 30 // aggregated
				}
			};

			List<GroupedDataFormat> expected = new List<GroupedDataFormat>()
			{
				new GroupedDataFormat()
				{
					GroupBy = date.AddHours(1),
					Activity = new[] { "Foo", "Bar" },
					TimeSpan = new long[] { 10, 20 } // aggregated over start - end, but grouped by group by
				},
				new GroupedDataFormat()
				{
					GroupBy = date.AddHours(2),
					Activity = new[] { "Qux" },
					TimeSpan = new long[] { 30 } // aggregated over start - end, but grouped by group by
				}
			};
			#endregion


			var actual = list
				.GroupBy(s => s.Time)
				.Select(r => new GroupedDataFormat()
					{
						GroupBy = r.FirstOrDefault().Time,
						Activity = r.Select(s => s.Process).ToArray(),
						TimeSpan = r.Select(s => s.Frequency).ToArray()
					});

			Assert.AreEqual(expected.Count, actual.Count());

			var zipped = expected.Zip(actual, (e, a) => new { Expected = e, Actual = a });

			foreach (var item in zipped)
			{
				CollectionAssert.AreEqual(item.Expected.Activity, item.Actual.Activity);
				CollectionAssert.AreEqual(item.Expected.TimeSpan, item.Actual.TimeSpan);
				Assert.AreEqual(item.Expected.GroupBy, item.Actual.GroupBy);
			}

		}

		[TestMethod]
		public void Basic_ListAtom_GroupedDataFormat_Aggregates()
		{
			DateTime date = new DateTime(2012, 1, 1, 1, 1, 1);

			#region test data
			List<DataAtom> list = new List<DataAtom>()
			{
				new DataAtom()
				{
					Time = date.AddHours(1),
					Process = "Foo", // activity
					Title = "Foo title", // not bubbled yet
					Frequency = 5 // aggregated
				},
				new DataAtom()
				{
					Time = date.AddHours(1).AddMinutes(1),
					Process = "Foo", // activity
					Title = "Foo title", // not bubbled yet
					Frequency = 5 // aggregated
				},
				new DataAtom()
				{
					Time = date.AddHours(1),
					Process = "Bar", // activity
					Title = "Foo title", // not bubbled yet
					Frequency = 20 // aggregated
				},
				new DataAtom()
				{
					Time = date.AddHours(2),
					Process = "Qux", // activity
					Title = "Foo title", // not bubbled yet
					Frequency = 30 // aggregated
				}
			};

			List<GroupedDataFormat> expected = new List<GroupedDataFormat>()
			{
				new GroupedDataFormat()
				{
					GroupBy = date.AddHours(1),
					Activity = new[] { "Foo", "Bar" },
					TimeSpan = new long[] { 10, 20 } // aggregated over start - end, but grouped by group by
				},
				new GroupedDataFormat()
				{
					GroupBy = date.AddHours(2),
					Activity = new[] { "Qux" },
					TimeSpan = new long[] { 30 } // aggregated over start - end, but grouped by group by
				}
			};
			#endregion
			var actual = new DataFormatConvertor(list).ToGroupedDataFormat();

			Assert.AreEqual(expected.Count, actual.Count());

			var zipped = expected.Zip(actual, (e, a) => new { Expected = e, Actual = a });

			foreach (var item in zipped)
			{
				CollectionAssert.AreEqual(item.Expected.Activity, item.Actual.Activity);
				CollectionAssert.AreEqual(item.Expected.TimeSpan, item.Actual.TimeSpan);
				Assert.AreEqual(item.Expected.GroupBy, item.Actual.GroupBy);
			}
		}

		
		[TestMethod]
		public void Basic_ListAtom_GroupedDataFormat_ByDay()
		{
			DateTime date = new DateTime(2012, 1, 1, 1, 1, 1);

			List<DataAtom> list = new List<DataAtom>()
			{
				new DataAtom()
				{
					Time = date.AddHours(1).AddDays(1),
					Process = "Foo", // activity
					Title = "Foo title", // not bubbled yet
					Frequency = 10 // aggregated
				},
				new DataAtom()
				{
					Time = date.AddDays(1),
					Process = "Bar", // activity
					Title = "Foo title", // not bubbled yet
					Frequency = 20 // aggregated
				},
				new DataAtom()
				{
					Time = date.AddDays(2),
					Process = "Qux", // activity
					Title = "Foo title", // not bubbled yet
					Frequency = 30 // aggregated
				}
			};

			List<GroupedDataFormat> expected = new List<GroupedDataFormat>()
			{
				new GroupedDataFormat()
				{
					GroupBy = date.AddDays(1),
					Activity = new[] { "Foo", "Bar" },
					TimeSpan = new long[] { 10, 20 } // aggregated over start - end, but grouped by group by
				},
				new GroupedDataFormat()
				{
					GroupBy = date.AddDays(2),
					Activity = new[] { "Qux" },
					TimeSpan = new long[] { 30 } // aggregated over start - end, but grouped by group by
				}
			};

			var actual = new DataFormatConvertor(list).ToGroupedDataFormat();

			Assert.AreEqual(expected.Count, actual.Count());

			var zipped = expected.Zip(actual, (e, a) => new { Expected = e, Actual = a });

			foreach (var item in zipped)
			{
				CollectionAssert.AreEqual(item.Expected.Activity, item.Actual.Activity);
				CollectionAssert.AreEqual(item.Expected.TimeSpan, item.Actual.TimeSpan);
				Assert.AreEqual(item.Expected.GroupBy.Year, item.Actual.GroupBy.Year);
				Assert.AreEqual(item.Expected.GroupBy.Month, item.Actual.GroupBy.Month);
				Assert.AreEqual(item.Expected.GroupBy.Day, item.Actual.GroupBy.Day);
			}

		}

		

		[TestMethod]
		public void Basic_ListAtom_GroupedDataFormat_ByWeek()
		{
			// Aggregation if same activity???
			DateTime date = new DateTime(2012, 4, 1, 1, 1, 1);

			ITimeWindow window = new DayTimeWindow(date).ToWeekWindow();
			ITimeWindow windowNext = new DayTimeWindow(date).ToWeekWindow();
			windowNext.GoNext(); 

			List<DataAtom> list = new List<DataAtom>()
			{
				new DataAtom()
				{
					Time = window.StartTime.AddDays(1),
					Process = "Foo", // activity
					Title = "Foo title", // not bubbled yet
					Frequency = 5 // aggregated
				},
				new DataAtom()
				{
					Time = window.StartTime.AddDays(2),
					Process = "Foo", // activity
					Title = "Foo title", // not bubbled yet
					Frequency = 5 // aggregated
				},
				new DataAtom()
				{
					Time = window.StartTime.AddDays(3).AddMinutes(1),
					Process = "Bar", // activity
					Title = "Foo title", // not bubbled yet
					Frequency = 20 // aggregated
				},
				new DataAtom()
				{
					Time = windowNext.StartTime.AddDays(1),
					Process = "Qux", // activity
					Title = "Foo title", // not bubbled yet
					Frequency = 30 // aggregated
				}
			};

			List<GroupedDataFormat> expected = new List<GroupedDataFormat>()
			{
				new GroupedDataFormat()
				{
					GroupBy = window.StartTime,
					Activity = new[] { "Foo", "Bar" },
					TimeSpan = new long[] { 10, 20 } // aggregated over start - end, but grouped by group by
				},
				new GroupedDataFormat()
				{
					GroupBy = windowNext.StartTime,
					Activity = new[] { "Qux" },
					TimeSpan = new long[] { 30 } // aggregated over start - end, but grouped by group by
				}
			};

			var actual = new DataFormatConvertor(list).ToGroupedDataFormat();

			Assert.AreEqual(expected.Count, actual.Count());

			var zipped = expected.Zip(actual, (e, a) => new { Expected = e, Actual = a });

			foreach (var item in zipped)
			{
				CollectionAssert.AreEqual(item.Expected.Activity, item.Actual.Activity);
				CollectionAssert.AreEqual(item.Expected.TimeSpan, item.Actual.TimeSpan);
				Assert.AreEqual(item.Expected.GroupBy.Year, item.Actual.GroupBy.Year);
				Assert.AreEqual(item.Expected.GroupBy.Month, item.Actual.GroupBy.Month);
				Assert.AreEqual(item.Expected.GroupBy.Day, item.Actual.GroupBy.Day);
			}

		}

		

	}
}
