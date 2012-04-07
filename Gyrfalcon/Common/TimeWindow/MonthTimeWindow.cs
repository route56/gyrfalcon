using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.TimeWindow
{
	public class MonthTimeWindow : ITimeWindow
	{
		private DateTime _startTime;
		private DateTime _endTime;

		public MonthTimeWindow(DateTime start, DateTime end)
		{
			this._startTime = start;
			this._endTime = end;
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
			return new DayTimeWindow(_startTime).ToWeekWindow();
		}

		public ITimeWindow ToMonthWindow()
		{
			return this;
		}

		public ITimeWindow ToYearWindow()
		{
			return new DayTimeWindow(_startTime).ToYearWindow();
		}

		public void GoNext()
		{
			_startTime = _startTime.AddMonths(1);
			_endTime = _startTime.AddMonths(1).AddDays(-1);
		}

		public void GoPrevious()
		{
			_startTime = _startTime.AddMonths(-1);
			_endTime = _startTime.AddMonths(1).AddDays(-1);
		}
	}
}
