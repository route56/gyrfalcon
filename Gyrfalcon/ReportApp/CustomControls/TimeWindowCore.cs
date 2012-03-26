using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReportApp.CustomControls.TimeWindow;

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
		private ITimeWindow _timeWindow;

		public TimeWindowCore(DateTime start, DateTime end, UberSpan span)
		{
			switch (span)
			{
				case UberSpan.MultiYear:
					_timeWindow = new MultiYearTimeWindow(start, end);
					break;
				case UberSpan.Year:
					_timeWindow = new YearTimeWindow(start, end);
					break;
				case UberSpan.Month:
					_timeWindow = new MonthTimeWindow(start, end);
					break;
				case UberSpan.Week:
					_timeWindow = new WeekTimeWindow(start, end);
					break;
				case UberSpan.Day:
					_timeWindow = new DayTimeWindow(start, end);
					break;
				default:
					throw new ArgumentOutOfRangeException("Invalid UberSpan");
			}
		}

		public DateTime StartTime
		{
			get { return _timeWindow.StartTime; }
		}

		public DateTime EndTime
		{
			get { return _timeWindow.EndTime; }
		}

		public UberSpan GetUberSpan()
		{
			if (_timeWindow is DayTimeWindow)
			{
				return UberSpan.Day;
			}

			if (_timeWindow is WeekTimeWindow)
			{
				return UberSpan.Week;
			}

			if (_timeWindow is MonthTimeWindow)
			{
				return UberSpan.Month;
			}

			if (_timeWindow is YearTimeWindow)
			{
				return UberSpan.Year;
			}

			if (_timeWindow is MultiYearTimeWindow)
			{
				return UberSpan.MultiYear;
			}

			throw new InvalidOperationException("Couldn't figure out proper UberScope");
		}

		public void GoDay()
		{
			_timeWindow = _timeWindow.ToDayWindow();
		}

		public void GoWeek()
		{
			_timeWindow = _timeWindow.ToWeekWindow();
		}

		public void GoMonth()
		{
			_timeWindow = _timeWindow.ToMonthWindow();
		}

		public void GoYear()
		{
			_timeWindow = _timeWindow.ToYearWindow();
		}

		public void GoNext()
		{
			_timeWindow.GoNext();
		}

		public void GoPrevious()
		{
			_timeWindow.GoPrevious();
		}
	}
}
