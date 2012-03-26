using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportApp.CustomControls.TimeWindow
{
	public interface ITimeWindow
	{
		DateTime StartTime { get; }
		DateTime EndTime { get; }

		ITimeWindow ToDayWindow();
		ITimeWindow ToWeekWindow();
		ITimeWindow ToMonthWindow();
		ITimeWindow ToYearWindow();
		ITimeWindow ToMultiYearWindow();

		void GoNext();
		void GoPrevious();
	}
}
