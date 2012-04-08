using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.TimeWindow;

namespace DataStore
{
	public class DataFormatConvertor
	{
		private IEnumerable<DataAtom> _list;

		public DataFormatConvertor(IEnumerable<DataAtom> list)
		{
			_list = list;
		}

		/// <summary>
		/// aggregation based on group by activity and over timespan
		/// </summary>
		/// <returns></returns>
		public IEnumerable<RankedDataFormat> ToRankedDataFormat()
		{
			int rank = 1;
			var result = _list
				.GroupBy(s => s.Process)
				.Select(r => new
				{
					Activity = r.FirstOrDefault().Process,
					TimeSpan = r.Sum(d => d.Frequency)
				})
				.OrderByDescending(s => s.TimeSpan)
				.Select(r => new RankedDataFormat()
				{
					Rank = rank++,
					Activity = r.Activity,
					TimeSpan = r.TimeSpan
				});

			// TODO Figure out why this didn't work
			// int rank = 1;
			//foreach (var item in result)
			//{
			//    item.Rank = rank++;
			//}

			return result;
		}

		/// <summary>
		/// Aggregates based on time and activity and returns in grouped data format
		/// </summary>
		/// <returns></returns>
		public IEnumerable<GroupedDataFormat> ToGroupedDataFormat()
		{
			var start = _list.Min(s => s.Time);
			var end = _list.Max(s => s.Time);

			ITimeWindow window = new DayTimeWindow(start);
			if (end > window.EndTime)
			{
				window = window.ToWeekWindow();

				if (end > window.EndTime)
				{
					return ToGroupedDataFormatPerWeek();
				}
				else
				{
					return ToGroupedDataFormatPerDay();
				}
			}
			else
			{
				return ToGroupedDataFormatPerHour();
			}
		}

		private IEnumerable<GroupedDataFormat> ToGroupedDataFormatPerHour()
		{
			var result = _list
				.GroupBy(s => new 
					{ 
						ATime = new DateTime(s.Time.Year, s.Time.Month, s.Time.Day, s.Time.Hour, 0, 0),
						AActivity = s.Process 
					})
				.Select(r => new
					{
						Time = r.FirstOrDefault().Time,
						Activity = r.FirstOrDefault(),
						TimeSpan = r.Sum(s => s.Frequency),
					})
				.GroupBy(s => 
					new DateTime(s.Time.Year, s.Time.Month, s.Time.Day, s.Time.Hour, 0, 0))
				.Select(r => new GroupedDataFormat()
					{
						GroupBy = r.FirstOrDefault().Time,
						Activity = r.Select(g => g.Activity.Process).ToArray(),
						TimeSpan = r.Select(g => g.TimeSpan).ToArray()
					});
			return result;
		}

		private IEnumerable<GroupedDataFormat> ToGroupedDataFormatPerDay()
		{
			var actual = _list
				.GroupBy(s => new DateTime(s.Time.Year, s.Time.Month, s.Time.Day, 0, 0, 0))
				.Select(r => new GroupedDataFormat()
				{
					GroupBy = r.FirstOrDefault().Time,
					Activity = r.Select(s => s.Process).ToArray(),
					TimeSpan = r.Select(s => s.Frequency).ToArray()
				});
			return actual;
		}

		private IEnumerable<GroupedDataFormat> ToGroupedDataFormatPerWeek()
		{
			var actual = _list
				.GroupBy(s => new
				{
					ATime = new DayTimeWindow(s.Time).ToWeekWindow().StartTime,
					AActivity = s.Process
				})
				.Select(r => new
				{
					Time = r.FirstOrDefault().Time,
					Activity = r.FirstOrDefault(),
					TimeSpan = r.Sum(s => s.Frequency),
				})
				.GroupBy(s => new DayTimeWindow(s.Time).ToWeekWindow().StartTime)
				.Select(r => new GroupedDataFormat()
				{
					GroupBy = new DayTimeWindow(r.FirstOrDefault().Time).ToWeekWindow().StartTime,
					Activity = r.Select(g => g.Activity.Process).ToArray(),
					TimeSpan = r.Select(g => g.TimeSpan).ToArray()
				});
			return actual;
		}
	}
}
