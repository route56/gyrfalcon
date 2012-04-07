using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.TimeWindow;
using System.IO;

namespace DataStore
{
	public class QueryStore : IQueryStore
	{
		public QueryStore()
		{
			FilePathProvider = new FilePaths(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
		}

		public IEnumerable<GroupedDataFormat> GetGroupedData(DateTime startTime, DateTime endTime)
		{
			List<GroupedDataFormat> ans = new List<GroupedDataFormat>();
			IEnumerable<DataAtom> dataList = GetData(startTime, endTime);

			//// TODO this time window logic is voilating DRY
			//ITimeWindow window = new DayTimeWindow(startTime);
			//if (endTime <= window.EndTime)
			//{
			//    // per hour
			//    //dataList
			//    //new GroupedDataFormat() { GroupBy = hour
			//}
			//else
			//{
			//    window = window.ToWeekWindow();
			//    if (endTime <= window.EndTime)
			//    {
			//        // per day (max 7)
			//    }
			//    else
			//    {
			//        // per week
			//    }
			//}

			return ans;
		}

		private IEnumerable<DataAtom> GetData(DateTime start, DateTime end)
		{
			return GetFilesToRead(start, end)
				.SelectMany(file => LoadDataFromFile(file))
				.Where(data => data.Time >= start && data.Time <= end);
		}

		private List<string> GetFilesToRead(DateTime start, DateTime end)
		{
			if (start > end)
			{
				throw new ArgumentException("Start time is greater than end time");
			}

			ITimeWindow window = new DayTimeWindow(start);
			List<string> fileList = new List<string>();

			if (end > window.EndTime)
			{
				window = window.ToWeekWindow();

				if (end > window.EndTime)
				{
					fileList.AddRange(FilePathProvider.GetAllWeeksBetween(start, end));
				}
				else
				{
					fileList.AddRange(FilePathProvider.GetAllDaysForWeek(window.StartTime));
				}
			}
			else
			{
				fileList.AddRange(FilePathProvider.GetAllHourForDay(window.StartTime));
			}
			return fileList;
		}

		private IEnumerable<DataAtom> LoadDataFromFile(string file)
		{
			List<DataAtom> data = new List<DataAtom>();

			using (StreamReader sr = File.OpenText(file))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					data.Add(DataAtom.FromString(line));
				}
			}

			return data;
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
		#endregion

		public FilePaths FilePathProvider { get; set; }
	}
}
