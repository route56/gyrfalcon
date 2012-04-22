using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStore.Contract;

namespace DataStore
{
	public class StubQueryStore : IQueryStore
	{
		public IEnumerable<GroupedDataFormat> GetGroupedData(DateTime _startTime, DateTime _endTime)
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

		public IEnumerable<RankedDataFormat> GetRankedData(DateTime _startTime, DateTime _endTime)
		{
			flip = !flip;

			if (flip)
			{
				return BarOne();
			}
			else
			{
				return BarTwo();
			}
		}

		static bool flip = false;
		internal List<GroupedDataFormat> AreaOne()
		{
			return new List<GroupedDataFormat>()
				{
					new GroupedDataFormat()
					{
						GroupBy = new DateTime(2012,2,2,2,2,2),
						TimeSpan = new long[] { 20, 10},
						Activity = new string[] {"Visual studio", "Chrome"}
					},
					new GroupedDataFormat()
					{
						GroupBy = new DateTime(2012,2,3,2,2,2),
						TimeSpan = new long[] { 5 },
						Activity = new string[] { "Visual studio" }
					},
					new GroupedDataFormat()
					{
						GroupBy = new DateTime(2012,2,4,2,2,2),
						TimeSpan = new long[] { 21, 10 },
						Activity = new string[] { "Visual studio", "Chess" }
					}
				};
		}

		internal List<GroupedDataFormat> AreaTwo()
		{
			return new List<GroupedDataFormat>()
				{
					new GroupedDataFormat()
					{
						GroupBy = new DateTime(2012,2,2,2,2,2),
						TimeSpan = new long[] { 10, 30},
						Activity = new string[] {"flipped", "again"}
					},
					new GroupedDataFormat()
					{
						GroupBy = new DateTime(2012,2,3,2,2,2),
						TimeSpan = new long[] { 5, 3 },
						Activity = new string[] { "king", "again" }
					},
					new GroupedDataFormat()
					{
						GroupBy = new DateTime(2012,2,4,2,2,2),
						TimeSpan = new long[] { 9, 30, 40 },
						Activity = new string[] { "knight", "chess", "sacrifice" }
					}
				};
		}

		internal List<RankedDataFormat> BarOne()
		{
			return new List<RankedDataFormat>()
			{
				new RankedDataFormat()
				{
					Rank = 1,
					TimeSpan = 60,
					Activity = "hjge"
				},
				new RankedDataFormat()
				{
					Rank = 2,
					TimeSpan = 50,
					Activity = "qsc"
				},
				new RankedDataFormat()
				{
					Rank = 3,
					TimeSpan = 45,
					Activity = "gfd"
				},
				new RankedDataFormat()
				{
					Rank = 4,
					TimeSpan = 30,
					Activity = "asd"
				},
				new RankedDataFormat()
				{
					Rank = 5,
					TimeSpan = 25,
					Activity = "qwe"
				},
				new RankedDataFormat()
				{
					Rank = 6,
					TimeSpan = 20,
					Activity = "JALJS"
				}
			};
		}

		internal List<RankedDataFormat> BarTwo()
		{
			return new List<RankedDataFormat>()
			{
				new RankedDataFormat()
				{
					Rank = 1,
					TimeSpan = 44,
					Activity = "hjge Flipped"
				},
				new RankedDataFormat()
				{
					Rank = 2,
					TimeSpan = 34,
					Activity = "qsc Flipped"
				},

				new RankedDataFormat()
				{
					Rank = 3,
					TimeSpan = 29,
					Activity = "asd"
				},
				new RankedDataFormat()
				{
					Rank = 4,
					TimeSpan = 20,
					Activity = "qwe Flipped"
				},
				new RankedDataFormat()
				{
					Rank = 5,
					TimeSpan = 10,
					Activity = "JALJS"
				}
			};
		}
	}
}
