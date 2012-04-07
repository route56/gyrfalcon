using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.TimeWindow
{
	public interface ITimeWindow
	{
		DateTime StartTime { get; }
		DateTime EndTime { get; }

		ITimeWindow ToDayWindow();
		ITimeWindow ToWeekWindow();
		ITimeWindow ToMonthWindow();
		ITimeWindow ToYearWindow();

		void GoNext();
		void GoPrevious();
	}
}
