using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportApp.CustomControls;

namespace ReportApp.Test
{
	[TestClass]
	public class TimeWindowCoreTest
	{
		[TestMethod]
		public void StartTimeEndTime_SetToNow_IgnoresTimepart()
		{
			TimeWindowCore target = new TimeWindowCore(DateTime.Now);

			Assert.AreEqual(target.StartTime.Hour, 0);
			Assert.AreEqual(target.StartTime.Minute, 0);
			Assert.AreEqual(target.StartTime.Second, 0);
			Assert.AreEqual(target.StartTime.Millisecond, 0);

			Assert.AreEqual(target.EndTime.Hour, 23);
			Assert.AreEqual(target.EndTime.Minute, 59);
			Assert.AreEqual(target.EndTime.Second, 59);
			Assert.AreEqual(target.EndTime.Millisecond, 999);
		}

		[TestMethod]
		public void StartTime_GetModify_OriginalDoesntChange()
		{
			var now = DateTime.Now;

			TimeWindowCore target = new TimeWindowCore(now);

			target.StartTime.AddDays(1);

			Assert.AreEqual(now.Day, target.StartTime.Day);
		}

		#region Day
		[TestMethod]
		public void GetUberSpan_SetDates_Day()
		{
			TimeWindowCore target = new TimeWindowCore(new DateTime(2012, 2, 3));
			Assert.AreEqual(target.GetUberSpan(), UberSpan.Day, "Same day treated as one day");

			target = new TimeWindowCore(new DateTime(2012, 2, 29));
			Assert.AreEqual(target.GetUberSpan(), UberSpan.Day, "Leap year check :P Azure went down 8 hrs on this day!");
		}

		[TestMethod]
		public void StartEnd_SetDates_Day()
		{
			AssertDatesMatch(new TimeWindowCore(new DateTime(2012, 2, 3)),
				new DateTime(2012, 2, 3),
				new DateTime(2012, 2, 3),
				"Start and end should have same date");

			AssertDatesMatch(new TimeWindowCore(new DateTime(2012, 2, 29)),
				new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				"Leap year check");
		}

		[TestMethod]
		public void GoDay_SetDays_ShouldntChange()
		{
			AssertDatesMatch(new TimeWindowCore(new DateTime(2012, 2, 3)),
				new DateTime(2012, 2, 3),
				new DateTime(2012, 2, 3),
				"Day level shouldn't change");

			AssertDatesMatch(new TimeWindowCore(new DateTime(2012, 2, 29)),
				new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				"Leap year check Day level shouldn't change");
		}

		[TestMethod]
		public void GoWeek_SetDays_Goes()
		{
			var target = new TimeWindowCore(new DateTime(2012, 2, 10));
			target.GoWeek();
			AssertDatesMatch(target,
				new DateTime(2012, 2, 5),
				new DateTime(2012, 2, 11),
				"10th feb is friday and falls in week Sun 5 to Sat 11");

			target = new TimeWindowCore(new DateTime(2012, 2, 5));
			target.GoWeek();
			AssertDatesMatch(target,
				new DateTime(2012, 2, 5),
				new DateTime(2012, 2, 11),
				"5th feb is Sunday and falls in week Sun 5 to Sat 11");

			target = new TimeWindowCore(new DateTime(2012, 2, 29));
			target.GoWeek();
			AssertDatesMatch(target,
				new DateTime(2012, 2, 26),
				new DateTime(2012, 3, 3),
				"Leap year check. Falls between 26th Feb Sun, 3rd March");
		}

		[TestMethod]
		public void GoMonth_SetDays_Goes()
		{
			var target = new TimeWindowCore(new DateTime(2012, 2, 10));
			target.GoMonth();

			AssertDatesMatch(target,
				new DateTime(2012, 2, 1),
				new DateTime(2012, 2, 29),
				"10th feb falls in feb month");

			target = new TimeWindowCore(new DateTime(2012, 3, 5));
			target.GoMonth();
			AssertDatesMatch(target,
				new DateTime(2012, 3, 1),
				new DateTime(2012, 3, 31),
				"5th march in March");

			target = new TimeWindowCore(new DateTime(2012, 2, 29));
			target.GoMonth();
			AssertDatesMatch(target,
				new DateTime(2012, 2, 1),
				new DateTime(2012, 2, 29),
				"Leap year check. Falls in Feb");
		}

		[TestMethod]
		public void GoYear_SetDays_Goes()
		{
			var target = new TimeWindowCore(new DateTime(2007, 2, 10));
			target.GoYear();
			AssertDatesMatch(target,
				new DateTime(2007, 1, 1),
				new DateTime(2007, 12, 31),
				"2007 year");

			target = new TimeWindowCore(new DateTime(2010, 3, 5));
			target.GoYear();
			AssertDatesMatch(target,
				new DateTime(2010, 1, 1),
				new DateTime(2010, 12, 31),
				"2010 year");

			target = new TimeWindowCore(new DateTime(2012, 2, 29));
			target.GoYear();
			AssertDatesMatch(target,
				new DateTime(2012, 1, 1),
				new DateTime(2012, 12, 31),
				"Leap year check. 2012 year");
		}

		[TestMethod]
		public void GoNext_SetDays_Goes()
		{
			var target = new TimeWindowCore(new DateTime(2012, 2, 29));
			target.GoNext();
			AssertDatesMatch(target,
				new DateTime(2012, 3, 1),
				new DateTime(2012, 3, 1),
				"Leap year check. Next day is March");

			target = new TimeWindowCore(new DateTime(2012, 2, 15));
			target.GoNext();
			AssertDatesMatch(target,
				new DateTime(2012, 2, 16),
				new DateTime(2012, 2, 16),
				"Simple some inbetween month day");

			target = new TimeWindowCore(new DateTime(2012, 2, 1));
			target.GoNext();
			AssertDatesMatch(target,
				new DateTime(2012, 2, 2),
				new DateTime(2012, 2, 2),
				"1st day of month");

			target = new TimeWindowCore(new DateTime(2012, 12, 31));
			target.GoNext();
			AssertDatesMatch(target,
				new DateTime(2013, 1, 1),
				new DateTime(2013, 1, 1),
				"last day of year");
		}

		[TestMethod]
		public void GoPrevious_SetDays_Goes()
		{
			var target = new TimeWindowCore(new DateTime(2013, 1, 1));
			target.GoPrevious();
			AssertDatesMatch(target,
				new DateTime(2012, 12, 31),
				new DateTime(2012, 12, 31),
				"1st day year");

			target = new TimeWindowCore(new DateTime(2012, 3, 1));
			target.GoPrevious();
			AssertDatesMatch(target,
				new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				"1st day of month");

			target = new TimeWindowCore(new DateTime(2012, 3, 15));
			target.GoPrevious();
			AssertDatesMatch(target,
				new DateTime(2012, 3, 14),
				new DateTime(2012, 3, 14),
				"some mid day in month");
		}

		#endregion

		#region week
		[TestMethod]
		public void GetUberSpan_SetDates_Week()
		{
			var target = new TimeWindowCore(new DateTime(2012, 2, 2));
			target.GoWeek();
			Assert.AreEqual(target.GetUberSpan(), UberSpan.Week, "Week expected");

			target = new TimeWindowCore(new DateTime(2012, 3, 4));
			target.GoWeek();
			Assert.AreEqual(target.GetUberSpan(), UberSpan.Week, "Week expected");

			target = new TimeWindowCore(new DateTime(2012, 3, 5));
			target.GoWeek();
			Assert.AreEqual(target.GetUberSpan(), UberSpan.Week, "Week expected");
		}

		[TestMethod]
		public void StartEnd_SetDates_Week()
		{
			var target = new TimeWindowCore(new DateTime(2012, 2, 13));
			target.GoWeek();

			AssertDatesMatch(target,
				new DateTime(2012, 2, 12),
				new DateTime(2012, 2, 18),
				"Setting two consecutive dates spans into week");

			target = new TimeWindowCore(new DateTime(2012, 2, 27));
			target.GoWeek();

			AssertDatesMatch(target,
				new DateTime(2012, 2, 26),
				new DateTime(2012, 3, 3),
				"Around month end");
		}

		[TestMethod]
		public void GoDay_SetWeek_Goes()
		{
			var target = new TimeWindowCore(new DateTime(2012, 2, 21));
			target.GoWeek();
			target.GoDay();

			AssertDatesMatch(target,
				new DateTime(2012, 2, 19),
				new DateTime(2012, 2, 19),
				"21 Feb will expand to 19 to 25 feb, will change then to 19 feb");

			target = new TimeWindowCore(new DateTime(2012, 3, 2));
			target.GoWeek();
			target.GoDay();

			AssertDatesMatch(target,
				new DateTime(2012, 2, 26),
				new DateTime(2012, 2, 26),
				"2nd march will expand to 26feb - 3 mar which will change to 26 feb");
		}

		[TestMethod]
		public void GoWeek_SetWeek_ShouldntChange()
		{
			var target = new TimeWindowCore(new DateTime(2012, 2, 21));
			target.GoWeek();
			target.GoWeek();

			AssertDatesMatch(target,
				new DateTime(2012, 2, 19),
				new DateTime(2012, 2, 25),
				"21 Feb will expand to 19 to 25 feb, will remain unchanged");

			target = new TimeWindowCore(new DateTime(2012, 3, 2));
			target.GoWeek();
			target.GoWeek();

			AssertDatesMatch(target,
				new DateTime(2012, 2, 26),
				new DateTime(2012, 3, 3),
				"2nd march will expand to 26feb - 3 mar will remain unchanged");
		}

		[TestMethod]
		public void GoMonth_SetWeek_Goes()
		{
			var target = new TimeWindowCore(new DateTime(2012, 2, 21));
			target.GoWeek();
			target.GoMonth();

			AssertDatesMatch(target,
				new DateTime(2012, 2, 1),
				new DateTime(2012, 2, 29),
				"21 Feb will expand to 19 to 25 feb, will change to feb month");

			target = new TimeWindowCore(new DateTime(2012, 3, 2));
			target.GoWeek();
			target.GoMonth();

			AssertDatesMatch(target,
				new DateTime(2012, 2, 1),
				new DateTime(2012, 2, 29),
				"2nd march will expand to 26feb - 3 mar will change to feb month");
		}

		[TestMethod]
		public void GoYear_SetWeek_Goes()
		{
			var target = new TimeWindowCore(new DateTime(2012, 2, 21));
			target.GoWeek();
			target.GoYear();

			AssertDatesMatch(target,
				new DateTime(2012, 1, 1),
				new DateTime(2012, 12, 31),
				"21 Feb will expand to 19 to 25 feb, will change to 2012 year");

			target = new TimeWindowCore(new DateTime(2012, 1, 1));
			target.GoWeek();
			target.GoYear();

			AssertDatesMatch(target,
				new DateTime(2012, 1, 1),
				new DateTime(2012, 12, 31),
				"1st jan was sunday, hence week was 1 to 7, year will remain same");

			target = new TimeWindowCore(new DateTime(2011, 1, 1));
			target.GoWeek();
			target.GoYear();

			AssertDatesMatch(target,
				new DateTime(2010, 1, 1),
				new DateTime(2010, 12, 31),
				"1st jan was saturday, hence week ended in 1st jan, year will become 2010");
		}

		[TestMethod]
		public void GoNext_SetWeek_Goes()
		{
			var target = new TimeWindowCore(new DateTime(2012, 2, 20));
			target.GoWeek();
			target.GoNext();
			AssertDatesMatch(target,
				new DateTime(2012, 2, 26),
				new DateTime(2012, 3, 3),
				"19-25 feb week, shifted to 26 - 3 mar");

			target = new TimeWindowCore(new DateTime(2010, 12, 30));
			target.GoWeek();
			target.GoNext();
			AssertDatesMatch(target,
				new DateTime(2011,1,2),
				new DateTime(2011,1,8),
				"1st jan 2011 was sat, next week is 2-8 jan");

			target = new TimeWindowCore(new DateTime(2012,1,13));
			target.GoWeek();
			target.GoNext();
			AssertDatesMatch(target,
				new DateTime(2012,1,15),
				new DateTime(2012, 1, 21),
				"Simple happy case");
		}

		[TestMethod]
		public void GoPrevious_SetWeek_Goes()
		{
			var target = new TimeWindowCore(new DateTime(2012, 2, 10));
			target.GoWeek();
			target.GoPrevious();
			AssertDatesMatch(target,
				new DateTime(2012, 1, 29),
				new DateTime(2012, 2, 4),
				"Month boundry case");
		}

		#endregion

		#region Month
		[TestMethod]
		public void GetUberSpan_SetDates_Month()
		{
			var target = new TimeWindowCore(new DateTime(2012, 3, 3));
			target.GoMonth();
			Assert.AreEqual(target.GetUberSpan(), UberSpan.Month);

			target = new TimeWindowCore(new DateTime(2012, 3, 2));
			target.GoMonth();
			Assert.AreEqual(target.GetUberSpan(), UberSpan.Month);

			target = new TimeWindowCore(new DateTime(2012, 3, 1));
			target.GoMonth();
			Assert.AreEqual(target.GetUberSpan(), UberSpan.Month);
		}

		[TestMethod]
		public void StartEnd_SetDates_Month()
		{
			TimeWindowCore target = new TimeWindowCore(new DateTime(2012, 2, 29));
			target.GoMonth();
			AssertDatesMatch(target,
				new DateTime(2012,2,1),
				new DateTime(2012, 2,29),
				"last day of month");

			target = new TimeWindowCore(new DateTime(2012, 2, 1));
			target.GoMonth();
			AssertDatesMatch(target,
				new DateTime(2012, 2, 1),
				new DateTime(2012, 2, 29),
				"1st day of month");
		}

		[TestMethod]
		public void GoDay_SetMonth_Goes()
		{
			TimeWindowCore target = new TimeWindowCore(new DateTime(2012,2,6));
			target.GoMonth();
			target.GoDay();

			AssertDatesMatch(target,
				new DateTime(2012,2,1),
				new DateTime(2012, 2, 1),
				"1st day on month");
		}

		[TestMethod]
		public void GoWeek_SetMonth_Goes()
		{
			TimeWindowCore target = new TimeWindowCore(new DateTime(2012,2,2));
			target.GoMonth();
			target.GoWeek();
			AssertDatesMatch(target,
				new DateTime(2012,1,29),
				new DateTime(2012,2,4),
				"1st week of feb");
		}

		[TestMethod]
		public void GoMonth_SetMonth_ShouldntChange()
		{
			TimeWindowCore target = new TimeWindowCore(new DateTime(2012, 2, 2));
			target.GoMonth();
			target.GoMonth();
			AssertDatesMatch(target,
				new DateTime(2012, 2, 1),
				new DateTime(2012, 2, 29),
				"remains feb");
		}

		[TestMethod]
		public void GoYear_SetMonth_Goes()
		{
			TimeWindowCore target = new TimeWindowCore(new DateTime(2012, 2, 2));
			target.GoMonth();
			target.GoYear();

			AssertDatesMatch(target,
				new DateTime(2012, 1, 1),
				new DateTime(2012, 12, 31),
				"2012 year");
		}

		[TestMethod]
		public void GoNext_SetMonth_Goes()
		{
			TimeWindowCore target = new TimeWindowCore(new DateTime(2012, 1, 2));
			target.GoMonth();
			target.GoNext();
			AssertDatesMatch(target,
				new DateTime(2012, 2, 1),
				new DateTime(2012, 2, 29),
				"End date 31 jan. add month? 29 feb");

			target.GoNext();
			AssertDatesMatch(target,
				new DateTime(2012, 3, 1),
				new DateTime(2012, 3, 31),
				"end date 29 feb 2012 -> march");

			target.GoNext();
			AssertDatesMatch(target,
				new DateTime(2012, 4, 1),
				new DateTime(2012, 4, 30),
				"end date 30 april 2012");
		}

		[TestMethod]
		public void GoPrevious_SetMonth_Goes()
		{
			TimeWindowCore target = new TimeWindowCore(new DateTime(2012, 5, 2));
			target.GoMonth();
			target.GoPrevious();
			AssertDatesMatch(target,
				new DateTime(2012, 4, 1),
				new DateTime(2012, 4, 30),
				"End date 31 may. 30 april");

			target.GoPrevious();
			AssertDatesMatch(target,
				new DateTime(2012, 3, 1),
				new DateTime(2012, 3, 31),
				"End date 30 april. 31 march");

			target.GoPrevious();
			AssertDatesMatch(target,
				new DateTime(2012, 2, 1),
				new DateTime(2012, 2, 29),
				"end date 31 March 2012 -> 29 feb");

			target.GoPrevious();
			AssertDatesMatch(target,
				new DateTime(2012, 1, 1),
				new DateTime(2012, 1, 31),
				"end date 29 feb 2012 -> 31 jan");

			target.GoPrevious();
			AssertDatesMatch(target,
				new DateTime(2011, 12, 1),
				new DateTime(2011, 12, 31),
				"End date 31 Jan. 31 dec");
		}
		#endregion

		#region Year
		[TestMethod]
		public void GetUberSpan_SetDates_Year()
		{
			var target = new TimeWindowCore(new DateTime(2012, 1, 1));
			target.GoYear();
			Assert.AreEqual(target.GetUberSpan(), UberSpan.Year);

			target = new TimeWindowCore(new DateTime(2011, 3, 15));
			target.GoYear();
			Assert.AreEqual(target.GetUberSpan(), UberSpan.Year);
		}

		[TestMethod]
		public void StartEnd_SetDates_Year()
		{
			TimeWindowCore target = new TimeWindowCore(new DateTime(2011, 4, 10));
			target.GoYear();

			AssertDatesMatch(target,
				new DateTime(2011, 1, 1),
				new DateTime(2011, 12, 31),
				"2011 year");

		}

		[TestMethod]
		public void GoDay_SetYear_Goes()
		{
			TimeWindowCore target = new TimeWindowCore(new DateTime(2011, 4, 10));
			target.GoYear();
			target.GoDay();

			AssertDatesMatch(target,
				new DateTime(2011, 1, 1),
				new DateTime(2011, 1, 1),
				"1st Jan");
		}

		[TestMethod]
		public void GoWeek_SetYear_Goes()
		{
			TimeWindowCore target = new TimeWindowCore(new DateTime(2011, 4, 10));
			target.GoYear();
			target.GoWeek();

			AssertDatesMatch(target,
				new DateTime(2010, 12, 26),
				new DateTime(2011, 1, 1),
				"1st Jan was sat");
		}

		[TestMethod]
		public void GoMonth_SetYear_Goes()
		{
			TimeWindowCore target = new TimeWindowCore(new DateTime(2010, 4, 10));
			target.GoYear();
			target.GoMonth();

			AssertDatesMatch(target,
				new DateTime(2010, 1, 1),
				new DateTime(2010, 1, 31),
				"Jan month");
		}

		[TestMethod]
		public void GoYear_SetYear_ShouldntChange()
		{
			TimeWindowCore target = new TimeWindowCore(new DateTime(2010, 4, 10));
			target.GoYear();
			target.GoYear();

			AssertDatesMatch(target,
				new DateTime(2010, 1, 1),
				new DateTime(2010, 12, 31),
				"remains same");
		}

		[TestMethod]
		public void GoNext_SetYear_Goes()
		{
			TimeWindowCore target = new TimeWindowCore(new DateTime(2010, 4, 10));
			target.GoYear();

			target.GoNext();
			AssertDatesMatch(target,
				new DateTime(2011, 1, 1),
				new DateTime(2011, 12, 31),
				"2011 year");
		}

		[TestMethod]
		public void GoPrevious_SetYear_Goes()
		{
			TimeWindowCore target = new TimeWindowCore(new DateTime(2012, 4, 10));
			target.GoYear();

			target.GoPrevious();
			AssertDatesMatch(target,
				new DateTime(2011, 1, 1),
				new DateTime(2011, 12, 31),
				"2011 year");
		}

		#endregion

		#region Helper Asserts
		private void AssertDatesMatch(TimeWindowCore target, DateTime startExpected, DateTime endExpected, string msg)
		{
			DateTime startActual = target.StartTime;
			DateTime endActual = target.EndTime;

			Assert.AreEqual(startExpected.Year, startActual.Year, msg);
			Assert.AreEqual(startExpected.Month, startActual.Month, msg);
			Assert.AreEqual(startExpected.Day, startActual.Day, msg);

			Assert.AreEqual(endExpected.Year, endActual.Year, msg);
			Assert.AreEqual(endExpected.Month, endActual.Month, msg);
			Assert.AreEqual(endExpected.Day, endActual.Day, msg);
		}
		#endregion
	}
}