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
		[TestMethod]
		public void Basic_ListAtom_RankedDataFormat()
		{
			#region Setup
			DataAtomGenerator gen = new DataAtomGenerator();

			gen.Process = "Foo";
			gen.Frequency = 10; // 30

			var sequence = gen.RandomDataStreamTakeNow(3);

			gen.Process = "Qux";
			gen.Frequency = 25; // 50
			sequence = sequence.Union(gen.RandomDataStreamTakeNow(2)).ToArray();

			gen.Process = "Bar";
			gen.Frequency = 50; // 200
			sequence = sequence.Union(gen.RandomDataStreamTakeNow(4)).ToArray();

			gen.Process = "Pqr";
			gen.Frequency = 20; // 20
			sequence = sequence.Union(gen.RandomDataStreamTakeNow(1)).ToArray();

			List<RankedDataFormat> expected = new List<RankedDataFormat>()
			{
				new RankedDataFormat()
				{
					Rank = 1,
					Activity = "Bar",
					TimeSpan = 200
				},
				new RankedDataFormat()
				{
					Rank = 2,
					Activity = "Qux",
					TimeSpan = 50
				},
				new RankedDataFormat()
				{
					Rank = 3,
					Activity = "Foo",
					TimeSpan = 30
				},
				new RankedDataFormat()
				{
					Rank = 4,
					Activity = "Pqr",
					TimeSpan = 20
				}
			};
			#endregion

			var actual = new List<RankedDataFormat>(new DataFormatConvertor(sequence).ToRankedDataFormat());

			#region Verify
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
		public void ToRankedDataFormat_AssignedMultipleTimes_RankDoesntChange()
		{
			#region Setup
			List<DataAtom> list = new List<DataAtom>()
			{
				new DataAtom()
				{
					Time = DateTime.Now.AddDays(1),
					Process = "Foo",
					Frequency = 10
				},
				new DataAtom()
				{
					Time = DateTime.Now.AddDays(-1),
					Process = "Bar",
					Frequency = 20
				}
			};

			List<RankedDataFormat> expected = new List<RankedDataFormat>()
			{
				new RankedDataFormat()
				{
					Rank = 1,
					Activity = "Bar",
					TimeSpan = 20
				},
				new RankedDataFormat()
				{
					Rank = 2,
					Activity = "Foo",
					TimeSpan = 10
				}
			};
			#endregion

			var actual = new DataFormatConvertor(list).ToRankedDataFormat();
			var actual2 = actual;

			RDFAssertAsExpected(expected,  new List<RankedDataFormat>(actual));
			RDFAssertAsExpected(expected, new List<RankedDataFormat>(actual2));
		}

		private static void RDFAssertAsExpected(List<RankedDataFormat> expected, List<RankedDataFormat> actual)
		{
			Assert.AreEqual(expected.Count, actual.Count);
			for (int i = 0; i < actual.Count; i++)
			{
				Assert.AreEqual(expected[i].Rank, actual[i].Rank);
				Assert.AreEqual(expected[i].TimeSpan, actual[i].TimeSpan);
				Assert.AreEqual(expected[i].Activity, actual[i].Activity);
			}
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
					Activity = new[] { "Bar", "Foo" },
					TimeSpan = new long[] { 20, 10 } // aggregated over start - end, but grouped by group by
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

			#region verify
			Assert.AreEqual(expected.Count, actual.Count());

			var zipped = expected.Zip(actual, (e, a) => new { Expected = e, Actual = a });

			foreach (var item in zipped)
			{
				CollectionAssert.AreEqual(item.Expected.Activity, item.Actual.Activity);
				CollectionAssert.AreEqual(item.Expected.TimeSpan, item.Actual.TimeSpan);
				Assert.AreEqual(item.Expected.GroupBy.Year, item.Actual.GroupBy.Year);
				Assert.AreEqual(item.Expected.GroupBy.Month, item.Actual.GroupBy.Month);
				Assert.AreEqual(item.Expected.GroupBy.Day, item.Actual.GroupBy.Day);
				Assert.AreEqual(item.Expected.GroupBy.Hour, item.Actual.GroupBy.Hour);
			}
			#endregion

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
					Activity = new[] { "Bar", "Foo" },
					TimeSpan = new long[] { 20, 10 } // aggregated over start - end, but grouped by group by
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
				Assert.AreEqual(item.Expected.GroupBy.Year, item.Actual.GroupBy.Year);
				Assert.AreEqual(item.Expected.GroupBy.Month, item.Actual.GroupBy.Month);
				Assert.AreEqual(item.Expected.GroupBy.Day, item.Actual.GroupBy.Day);
				Assert.AreEqual(item.Expected.GroupBy.Hour, item.Actual.GroupBy.Hour);
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
					Activity = new[] { "Bar", "Foo"},
					TimeSpan = new long[] { 20, 10 } // aggregated over start - end, but grouped by group by
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
					Activity = new[] { "Bar", "Foo" },
					TimeSpan = new long[] { 20, 10} // aggregated over start - end, but grouped by group by
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

		[TestMethod]
		public void ToGroupedDataFormat_ByDay_SameActivityOnSameDayAggregated()
		{
			DateTime date = new DateTime(2012, 1, 1, 1, 1, 1);

			List<DataAtom> list = new List<DataAtom>()
			{
				new DataAtom()
				{
					Process = "a",
					Time = date.AddHours(1), 
					Frequency = 10,
				},
				new DataAtom()
				{
					Process = "b",
					Time = date.AddHours(2),
					Frequency = 10
				},
				new DataAtom()
				{
					Process = "a",
					Time = date.AddHours(3),
					Frequency = 10,
				},
				new DataAtom()
				{
					Process = "a",
					Time = date.AddDays(1),
					Frequency = 10,
				},
				new DataAtom()
				{
					Process = "b",
					Time = date.AddDays(2),
					Frequency = 10,
				}
			};

			List<GroupedDataFormat> expected = new List<GroupedDataFormat>()
			{
				new GroupedDataFormat()
				{
					GroupWindow = GroupWindowType.Day,
					GroupBy = date,
					Activity = new string[] { "a", "b" },
					TimeSpan = new long[] { 20, 10 }
				},
				new GroupedDataFormat()
				{
					GroupWindow = GroupWindowType.Day,
					GroupBy = date.AddDays(1),
					Activity = new string[] { "a"},
					TimeSpan = new long[] { 10 }
				},
				new GroupedDataFormat()
				{
					GroupWindow = GroupWindowType.Day,
					GroupBy = date.AddDays(2),
					Activity = new string[] { "b" },
					TimeSpan = new long[] { 10 }
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
		public void GDFRDF_ConvertedPerHour()
		{
			List<GroupedDataFormat> data = new List<GroupedDataFormat>()
			{
				new GroupedDataFormat()
				{
					TimeSpan = new long[] { 20, 10 }
				},
				new GroupedDataFormat()
				{
					TimeSpan = new long[] { 10 }
				},
				new GroupedDataFormat()
				{
					TimeSpan = new long[] { 10 }
				}
			};

			List<GroupedDataFormat> expected = new List<GroupedDataFormat>()
			{
				new GroupedDataFormat()
				{
					TimeSpan = new long[] { 2, 1 }
				},
				new GroupedDataFormat()
				{
					TimeSpan = new long[] { 1 }
				},
				new GroupedDataFormat()
				{
					TimeSpan = new long[] { 1 }
				}
			};

			var actual = data.Select(s => 
				{
					s.TimeSpan = s.TimeSpan.Select(r => r / 10).ToArray();
					return s; 
				});

			Assert.IsTrue(expected.Zip(actual, (e, a) => new { Expected = e, Actual = a })
				.All(a => 
					a.Expected.TimeSpan.SequenceEqual(a.Actual.TimeSpan))); // Yay! I'm LINQ expert
		}
	}
}
