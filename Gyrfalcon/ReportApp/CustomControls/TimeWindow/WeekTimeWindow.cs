using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportApp.CustomControls.TimeWindow
{
	class WeekTimeWindow : ITimeWindow
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
			throw new NotImplementedException();
		}

		public ITimeWindow ToWeekWindow()
		{
			throw new NotImplementedException();
		}

		public ITimeWindow ToMonthWindow()
		{
			throw new NotImplementedException();
		}

		public ITimeWindow ToYearWindow()
		{
			throw new NotImplementedException();
		}

		public ITimeWindow ToMultiYearWindow()
		{
			throw new NotImplementedException();
		}

		public void GoNext()
		{
			throw new NotImplementedException();
		}

		public void GoPrevious()
		{
			throw new NotImplementedException();
		}
	}
}
