using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.TimeWindow;

namespace ReportApp.CustomControls
{
	public enum UberSpan
	{
		Year,
		Month,
		Week,
		Day
	}

	public class TimeWindowCore
	{
		private ITimeWindow _timeWindow;

		public TimeWindowCore(DateTime start)
		{
			_timeWindow = new DayTimeWindow(start);
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
