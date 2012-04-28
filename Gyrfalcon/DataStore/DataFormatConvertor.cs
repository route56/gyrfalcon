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
			if (_list.Count() == 0)
			{
				return new List<RankedDataFormat>();
			}

			var result = _list
				.GroupBy(s => s.Process)
				.Select(r => new
				{
					Activity = r.FirstOrDefault().Process,
					TimeSpan = r.Sum(d => d.Frequency)
				})
				.OrderByDescending(s => s.TimeSpan)
				.Select((r, i) => 
					{
						return new RankedDataFormat()
						{
							Rank = i + 1,
							Activity = r.Activity,
							TimeSpan = r.TimeSpan
						};
					});

			return result;
		}

		/// <summary>
		/// Aggregates based on time and activity and returns in grouped data format
		/// </summary>
		/// <returns></returns>
		public IEnumerable<GroupedDataFormat> ToGroupedDataFormat()
		{
			if (_list.Count() == 0)
			{
				return new List<GroupedDataFormat>();
			}

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
			return ToGroupedDataFormat(s => new DateTime(s.Year, s.Month, s.Day, s.Hour, 0, 0),
				GroupWindowType.Hour);
		}

		private IEnumerable<GroupedDataFormat> ToGroupedDataFormatPerDay()
		{
			return ToGroupedDataFormat(s => new DateTime(s.Year, s.Month, s.Day, 0, 0, 0),
				GroupWindowType.Day);
		}

		private IEnumerable<GroupedDataFormat> ToGroupedDataFormatPerWeek()
		{
			return ToGroupedDataFormat(s => new DayTimeWindow(s).ToWeekWindow().StartTime,
				GroupWindowType.Week);
		}

		private IEnumerable<GroupedDataFormat> ToGroupedDataFormat(Func<DateTime, DateTime> filter, GroupWindowType groupWindow)
		{
			var result = _list
				.GroupBy(s => new
				{
					ATime = filter(s.Time),
					AActivity = s.Process
				})
				.Select(r => new
				{
					Time = r.FirstOrDefault().Time,
					Activity = r.FirstOrDefault(),
					TimeSpan = r.Sum(s => s.Frequency),
				})
				.GroupBy(s => filter(s.Time))
				.Select(r => new GroupedDataFormat()
				{
					GroupBy = filter(r.FirstOrDefault().Time),
					GroupWindow = groupWindow,
					Activity = r.Select(g => g.Activity.Process).ToArray(),
					TimeSpan = r.Select(g => g.TimeSpan).ToArray()
				});
			return result;
		}
	}
}
