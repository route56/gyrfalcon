using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStore
{
	public class QueryStore : IQueryStore
	{
		Dictionary<Tuple<DateTime, DateTime>, IEnumerable<DataAtom>> _cache = new Dictionary<Tuple<DateTime, DateTime>, IEnumerable<DataAtom>>(); //Logic

		public QueryStore()
		{
			FilePathProvider = new FilePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
		}

		public IEnumerable<GroupedDataFormat> GetGroupedData(DateTime startTime, DateTime endTime)
		{
			//IEnumerable<DataAtom> dataList = GetData(startTime, endTime);

			//// logic
			//foreach (var item in dataList)
			//{
			//    item.Process;
			//}

			//TODO: group data
			throw new System.NotImplementedException("group data");
		}

		private IEnumerable<DataAtom> GetData(DateTime start, DateTime end)
		{
			//if (start > end)
			//{
			//    throw new ArgumentException("Start time is greater than end time");
			//}

			//IEnumerable<string> filesToRead = GetFileList(start, end); // Logic
			//string baseFolder = GetBaseFolder();

			//foreach (var item in filesToRead)
			//{
			//    Load(System.IO.Path.Combine(baseFolder, item));
			//}

			

			var span = end.Subtract(start);

			if (span.Days == 0)
			{
				// day. reading at max 24 files
				//FilePathProvider.GetHourSummary(
				
			}
			else if(span.Days <= 7)
			{
				// week. reading at max 7 files if summary is present. else, 7X24 worst case
				//FilePathProvider.GetDaySummary(
			}
			else if(span.Days <= 31)
			{
				// month. reading at max 31  files if summary is present. else 31X24
				//FilePathProvider.GetDaySummary
			}
			else
			{
				// year! reading at max 12 files if summary. else 12X365X24 files!!
				//FilePathProvider.GetMonthSummary
			}

			//TODO: gets data atom list within timeframe
			throw new System.NotImplementedException("gets data atom list within timeframe");
		}

		public IEnumerable<RankedDataFormat> GetRankedData(DateTime startTime, DateTime endTime)
		{
			// ComputeRanks(); // Logic

			//TODO: ranked data
			throw new System.NotImplementedException("ranked data");
		}

		#region mock data
		static bool flip = false;
		internal List<GroupedDataFormat> AreaOne()
		{
			return new List<GroupedDataFormat>()
				{
					new GroupedDataFormat()
					{
						GroupBy = "Day 1",
						TimeSpan = 20,
						Activity = "Visual studio"
					},
					new GroupedDataFormat()
					{
						GroupBy = "Day 1",
						TimeSpan = 10,
						Activity = "Chrome"
					},
					new GroupedDataFormat()
					{
						GroupBy = "Day 2",
						TimeSpan = 5,
						Activity = "Visual studio"
					},
					new GroupedDataFormat()
					{
						GroupBy = "Day 3",
						TimeSpan = 21,
						Activity = "Visual studio"
					},
					new GroupedDataFormat()
					{
						GroupBy = "Day 3",
						TimeSpan = 10,
						Activity = "Chess"
					}
				};
		}

		internal List<GroupedDataFormat> AreaTwo()
		{
			return new List<GroupedDataFormat>()
				{
					new GroupedDataFormat()
					{
						GroupBy = "Day 4",
						TimeSpan = 30,
						Activity = "Flipped"
					},
					new GroupedDataFormat()
					{
						GroupBy = "Day 4",
						TimeSpan = 20,
						Activity = "Chrome Flipped"
					},
					new GroupedDataFormat()
					{
						GroupBy = "Day 5",
						TimeSpan = 5,
						Activity = "Visual studio Flipped"
					},
					new GroupedDataFormat()
					{
						GroupBy = "Day 5",
						TimeSpan = 20,
						Activity = "Chrome"
					},
					new GroupedDataFormat()
					{
						GroupBy = "Day 6",
						TimeSpan = 10,
						Activity = "Chess Flipped"
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
		#endregion

		public FilePaths FilePathProvider { get; set; }
	}
}
