using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportApp.CustomControls.TimeWindow
{
	class DayTimeWindow : ITimeWindow
	{
		private DateTime _startTime;
		private DateTime _endTime;

		public DayTimeWindow(DateTime date)
		{
			_startTime = new DateTime(date.Year, date.Month, date.Day);
			_endTime = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
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
			return this;
		}

		public ITimeWindow ToWeekWindow()
		{
			DateTime start = _startTime.AddDays(DayOfWeek.Sunday - _startTime.DayOfWeek);
			DateTime end = _endTime.AddDays(DayOfWeek.Saturday - _endTime.DayOfWeek);

			return new WeekTimeWindow(start, end);
		}

		public ITimeWindow ToMonthWindow()
		{
			DateTime start = new DateTime(_startTime.Year, _startTime.Month, 1);
			DateTime end = start.AddMonths(1).AddDays(-1);

			return new MonthTimeWindow(start, end);
		}

		public ITimeWindow ToYearWindow()
		{
			DateTime start = new DateTime(_startTime.Year, 1, 1);
			DateTime end = new DateTime(_endTime.Year, 12, 31);

			return new YearTimeWindow(start, end);
		}

		public void GoNext()
		{
			_startTime = _startTime.AddDays(1);
			_endTime = _endTime.AddDays(1);
		}

		public void GoPrevious()
		{
			_startTime = _startTime.AddDays(-1);
			_endTime = _endTime.AddDays(-1);
		}
	}
}
