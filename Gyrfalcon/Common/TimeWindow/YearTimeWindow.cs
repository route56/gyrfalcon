using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.TimeWindow
{
	public class YearTimeWindow : ITimeWindow
	{
		private DateTime _startTime;
		private DateTime _endTime;

		public YearTimeWindow(DateTime start, DateTime end)
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
			return new DayTimeWindow(_startTime).ToMonthWindow();
		}

		public ITimeWindow ToYearWindow()
		{
			return this;
		}

		public void GoNext()
		{
			_startTime = _startTime.AddYears(1);
			_endTime = _startTime.AddYears(1).AddDays(-1);
		}

		public void GoPrevious()
		{
			_startTime = _startTime.AddYears(-1);
			_endTime = _startTime.AddYears(1).AddDays(-1);
		}
	}
}
