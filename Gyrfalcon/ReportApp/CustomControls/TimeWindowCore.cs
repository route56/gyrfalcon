using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportApp.CustomControls
{
	public enum UberSpan
	{
		MultiYear,
		Year,
		Month,
		Week,
		Day
	}

	public class TimeWindowCore
	{
		private DateTime _startTime;
		private DateTime _endTime;

		public DateTime StartTime
		{
			get
			{
				return _startTime;
			}
			set
			{
				// keep only date. no time.
				_startTime = new DateTime(value.Year, value.Month, value.Day);
			}
		}

		public DateTime EndTime
		{
			get
			{
				return _endTime;
			}
			set
			{
				// end time max
				_endTime = new DateTime(value.Year, value.Month, value.Day, 23, 59, 59, 999);
			}
		}

		public UberSpan GetUberSpan()
		{
			if (StartTime > EndTime)
			{
				throw new InvalidOperationException("Start time is greater than end time");
			}

			TimeSpan diff = EndTime.Subtract(StartTime);

			if (diff.Days == 0)
			{
				return UberSpan.Day;
			}

			throw new NotImplementedException();
		}

		public void GoDay()
		{
			switch (GetUberSpan())
			{
				case UberSpan.MultiYear:
					break;
				case UberSpan.Year:
					break;
				case UberSpan.Month:
					break;
				case UberSpan.Week:
					break;
				case UberSpan.Day:
					// No change needed
					return;
				default:
					break;
			}
			throw new NotImplementedException();
		}

		public void GoWeek()
		{
			switch (GetUberSpan())
			{
				case UberSpan.MultiYear:
					break;
				case UberSpan.Year:
					break;
				case UberSpan.Month:
					break;
				case UberSpan.Week:
					break;
				case UberSpan.Day:
					{
						int daysToSubtract = StartTime.DayOfWeek - DayOfWeek.Sunday;
						int daysToAdd = DayOfWeek.Saturday - EndTime.DayOfWeek;

						StartTime = StartTime.AddDays(-daysToSubtract);
						EndTime = EndTime.AddDays(daysToAdd);

						return;
					}
				default:
					break;
			}
			throw new NotImplementedException();
		}

		public void GoMonth()
		{
			switch (GetUberSpan())
			{
				case UberSpan.MultiYear:
					break;
				case UberSpan.Year:
					break;
				case UberSpan.Month:
					break;
				case UberSpan.Week:
					break;
				case UberSpan.Day:
					{
						DateTime firstDayOfTheMonth = new DateTime(StartTime.Year, StartTime.Month, 1);

						StartTime = firstDayOfTheMonth;
						EndTime = firstDayOfTheMonth.AddMonths(1).AddDays(-1);

						return;
					}
				default:
					break;
			}
			throw new NotImplementedException();
		}

		public void GoYear()
		{
			switch (GetUberSpan())
			{
				case UberSpan.MultiYear:
					break;
				case UberSpan.Year:
					break;
				case UberSpan.Month:
					break;
				case UberSpan.Week:
					break;
				case UberSpan.Day:
					{
						StartTime = new DateTime(StartTime.Year, 1, 1);
						EndTime = new DateTime(EndTime.Year, 12, 31);
						return;
					}
				default:
					break;
			}
			throw new NotImplementedException();
		}
	}
}
