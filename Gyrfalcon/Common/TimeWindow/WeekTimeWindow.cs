using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.TimeWindow
{
	public class WeekTimeWindow : ITimeWindow
	{
		private DateTime _startTime;
		private DateTime _endTime;

		public WeekTimeWindow(DateTime start, DateTime end)
		{
			_startTime = new DateTime(start.Year, start.Month, start.Day);
			_endTime = new DateTime(end.Year, end.Month, end.Day, 23, 59, 59, 999);
		}


		public DateTime StartTime
		{
			get { return _startTime; }
		}

		public DateTime EndTime
		{
			get { return _endTime; }
		}

		public ITimeWindow ToDayWindow()
		{
			return new DayTimeWindow(_startTime);
		}

		public ITimeWindow ToWeekWindow()
		{
			return this;
		}

		public ITimeWindow ToMonthWindow()
		{
			return new DayTimeWindow(_startTime).ToMonthWindow();
		}

		public ITimeWindow ToYearWindow()
		{
			return new DayTimeWindow(_startTime).ToYearWindow();
		}

		public void GoNext()
		{
			_startTime = _startTime.AddDays(7);
			_endTime = _endTime.AddDays(7);
		}

		public void GoPrevious()
		{
			_startTime = _startTime.AddDays(-7);
			_endTime = _endTime.AddDays(-7);
		}
	}
}
