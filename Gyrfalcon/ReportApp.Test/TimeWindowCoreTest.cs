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
			DateTime now = DateTime.Now;

			TimeWindowCore target = new TimeWindowCore(now, now, UberSpan.Day);

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
			DateTime now = DateTime.Now;
			TimeWindowCore target = new TimeWindowCore(now, now, UberSpan.Day);

			target.StartTime.AddDays(1);

			Assert.AreEqual(now.Day, target.StartTime.Day);
		}

		#region Day
		[TestMethod]
		public void GetUberSpan_SetDates_Day()
		{
			AssertOnUberSpan(new DateTime(2012, 2, 3),
				new DateTime(2012, 2, 3),
				UberSpan.Day,
				UberSpan.Day,
				"Same day treated as one day");

			AssertOnUberSpan(new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				UberSpan.Day,
				UberSpan.Day,
				"Leap year check :P Azure went down 8 hrs on this day!");
		}

		[TestMethod]
		public void StartEnd_SetDates_Day()
		{
			AssertOnStartEndDates(new DateTime(2012, 2, 3),
				new DateTime(2012, 2, 3),
				UberSpan.Day,
				new DateTime(2012, 2, 3),
				new DateTime(2012, 2, 3),
				"Start and end should have same date");

			AssertOnStartEndDates(new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				UberSpan.Day,
				new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				"Leap year check");
		}

		[TestMethod]
		public void GoDay_SetDays_ShouldntChange()
		{
			AssertOnGoDay(new DateTime(2012, 2, 3),
				new DateTime(2012, 2, 3),
				UberSpan.Day,
				new DateTime(2012, 2, 3),
				new DateTime(2012, 2, 3),
				"Day level shouldn't change");

			AssertOnGoDay(new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				UberSpan.Day,
				new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				"Leap year check Day level shouldn't change");
		}

		[TestMethod]
		public void GoWeek_SetDays_Goes()
		{
			AssertOnGoWeek(new DateTime(2012, 2, 10),
				new DateTime(2012, 2, 10),
				UberSpan.Day,
				new DateTime(2012, 2, 5),
				new DateTime(2012, 2, 11),
				"10th feb is friday and falls in week Sun 5 to Sat 11");

			AssertOnGoWeek(new DateTime(2012, 2, 5),
				new DateTime(2012, 2, 5),
				UberSpan.Day,
				new DateTime(2012, 2, 5),
				new DateTime(2012, 2, 11),
				"5th feb is Sunday and falls in week Sun 5 to Sat 11");

			AssertOnGoWeek(new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				UberSpan.Day,
				new DateTime(2012, 2, 26),
				new DateTime(2012, 3, 3),
				"Leap year check. Falls between 26th Feb Sun, 3rd March");
		}

		[TestMethod]
		public void GoMonth_SetDays_Goes()
		{
			AssertOnGoMonth(new DateTime(2012, 2, 10),
				new DateTime(2012, 2, 10),
				UberSpan.Day,
				new DateTime(2012, 2, 1),
				new DateTime(2012, 2, 29),
				"10th feb falls in feb month");

			AssertOnGoMonth(new DateTime(2012, 3, 5),
				new DateTime(2012, 3, 5),
				UberSpan.Day,
				new DateTime(2012, 3, 1),
				new DateTime(2012, 3, 31),
				"5th march in March");

			AssertOnGoMonth(new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				UberSpan.Day,
				new DateTime(2012, 2, 1),
				new DateTime(2012, 2, 29),
				"Leap year check. Falls in Feb");
		}

		[TestMethod]
		public void GoYear_SetDays_Goes()
		{
			AssertOnGoYear(new DateTime(2007, 2, 10),
				new DateTime(2007, 2, 10),
				UberSpan.Day,
				new DateTime(2007, 1, 1),
				new DateTime(2007, 12, 31),
				"2007 year");

			AssertOnGoYear(new DateTime(2010, 3, 5),
				new DateTime(2010, 3, 5),
				UberSpan.Day,
				new DateTime(2010, 1, 1),
				new DateTime(2010, 12, 31),
				"2010 year");

			AssertOnGoYear(new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				UberSpan.Day,
				new DateTime(2012, 1, 1),
				new DateTime(2012, 12, 31),
				"Leap year check. 2012 year");
		}

		[TestMethod]
		public void GoNext_SetDays_Goes()
		{
			AssertOnGoNext(new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				UberSpan.Day,
				new DateTime(2012, 3, 1),
				new DateTime(2012, 3, 1),
				"Leap year check. Next day is March");

			AssertOnGoNext(new DateTime(2012, 2, 15),
				new DateTime(2012, 2, 15),
				UberSpan.Day,
				new DateTime(2012, 2, 16),
				new DateTime(2012, 2, 16),
				"Simple some inbetween month day");

			AssertOnGoNext(new DateTime(2012, 2, 1),
				new DateTime(2012, 2, 1),
				UberSpan.Day,
				new DateTime(2012, 2, 2),
				new DateTime(2012, 2, 2),
				"1st day of month");

			AssertOnGoNext(new DateTime(2012, 12, 31),
				new DateTime(2012, 12, 31),
				UberSpan.Day,
				new DateTime(2013, 1, 1),
				new DateTime(2013, 1, 1),
				"last day of year");
		}

		[TestMethod]
		public void GoPrevious_SetDays_Goes()
		{
			AssertOnGoPrevious(new DateTime(2013, 1, 1),
				new DateTime(2013, 1, 1),
				UberSpan.Day,
				new DateTime(2012, 12, 31),
				new DateTime(2012, 12, 31),
				"1st day year");

			AssertOnGoPrevious(new DateTime(2012, 3, 1),
				new DateTime(2012, 3, 1),
				UberSpan.Day,
				new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				"1st day of month");

			AssertOnGoPrevious(new DateTime(2012, 3, 15),
				new DateTime(2012, 3, 15),
				UberSpan.Day,
				new DateTime(2012, 3, 14),
				new DateTime(2012, 3, 14),
				"some mid day in month");
		}

		#endregion

		#region week
		[TestMethod]
		public void GetUberSpan_SetDates_Week()
		{
			AssertOnUberSpan(new DateTime(2012, 2, 2),
				new DateTime(2012, 2, 3),
				UberSpan.Week,
				UberSpan.Week,
				"Two day treated as partial week");

			AssertOnUberSpan(new DateTime(2012, 3, 4),
				new DateTime(2012, 3, 10),
				UberSpan.Week,
				UberSpan.Week,
				"Sunday beginning and end");

			AssertOnUberSpan(new DateTime(2012, 3, 5),
				new DateTime(2012, 3, 8),
				UberSpan.Week,
				UberSpan.Week,
				"mid week");
		}

		[TestMethod]
		public void StartEnd_SetDates_Week()
		{
			Assert.Inconclusive("TODO");
			AssertOnStartEndDates(new DateTime(2012, 2, 13),
				new DateTime(2012, 2, 14),
				UberSpan.Week,
				new DateTime(2012, 2, 12),
				new DateTime(2012, 2, 18),
				"Setting two consecutive dates spans into week");

			AssertOnStartEndDates(new DateTime(2012, 2, 27),
				new DateTime(2012, 2, 28),
				UberSpan.Week,
				new DateTime(2012, 2, 26),
				new DateTime(2012, 3, 3),
				"Around month end");
		}

		[TestMethod]
		public void GoDay_SetWeek_Goes()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoWeek_SetWeek_ShouldntChange()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoMonth_SetWeek_Goes()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoYear_SetWeek_Goes()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoNext_SetWeek_Goes()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoPrevious_SetWeek_Goes()
		{
			Assert.Inconclusive();
		}

		#endregion

		#region Month
		[TestMethod]
		public void GetUberSpan_SetDates_Month()
		{
			AssertOnUberSpan(new DateTime(2012, 3, 3),
				new DateTime(2012, 3, 4),
				UberSpan.Month,
				UberSpan.Month,
				"Adjacent over weekend, sat, then sunday treated as month");

			AssertOnUberSpan(new DateTime(2012, 3, 2),
				new DateTime(2012, 3, 15),
				UberSpan.Month,
				UberSpan.Month,
				"Sub month");

			AssertOnUberSpan(new DateTime(2012, 3, 1),
				new DateTime(2012, 3, 31),
				UberSpan.Month,
				UberSpan.Month,
				"Complete month");
		}

		[TestMethod]
		public void StartEnd_SetDates_Month()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoDay_SetMonth_Goes()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoWeek_SetMonth_Goes()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoMonth_SetMonth_ShouldntChange()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoYear_SetMonth_Goes()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoNext_SetMonth_Goes()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoPrevious_SetMonth_Goes()
		{
			Assert.Inconclusive();
		}

		#endregion

		#region Year
		[TestMethod]
		public void GetUberSpan_SetDates_Year()
		{
			AssertOnUberSpan(new DateTime(2012, 1, 1),
				new DateTime(2012, 3, 31),
				UberSpan.Year,
				UberSpan.Year,
				"Quarter, but treated as year");

			AssertOnUberSpan(new DateTime(2011, 3, 15),
				new DateTime(2011, 9, 26),
				UberSpan.Year,
				UberSpan.Year,
				"Sub year");
		}

		[TestMethod]
		public void StartEnd_SetDates_Year()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoDay_SetYear_Goes()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoWeek_SetYear_Goes()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoMonth_SetYear_Goes()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoYear_SetYear_ShouldntChange()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoNext_SetYear_Goes()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoPrevious_SetYear_Goes()
		{
			Assert.Inconclusive();
		}

		#endregion

		#region Multiyear
		[TestMethod]
		public void GetUberSpan_SetDates_MultiYear()
		{
			AssertOnUberSpan(new DateTime(2011, 5, 6),
				new DateTime(2012, 3, 12),
				UberSpan.MultiYear,
				UberSpan.MultiYear,
				"Two years 2011, 2012");

			AssertOnUberSpan(new DateTime(2011, 12, 31),
				new DateTime(2012, 1, 1),
				UberSpan.MultiYear,
				UberSpan.MultiYear,
				"Two years 2011, 2012");
		}

		[TestMethod]
		public void StartEnd_SetDates_Multiyear()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoDay_SetMultiYear_Goes()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoWeek_SetMultiYear_Goes()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoMonth_SetMultiYear_Goes()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoYear_SetMultiYear_Goes()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoNext_SetMulti_Goes()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoPrevious_SetMulti_Goes()
		{
			Assert.Inconclusive();
		}

		#endregion

		#region Helper Asserts
		void AssertOnUberSpan(DateTime start, DateTime end, UberSpan span, UberSpan expected, string msg)
		{
			TimeWindowCore target = new TimeWindowCore(start, end, span);

			Assert.AreEqual(target.GetUberSpan(), expected, msg);
		}

		private void AssertOnStartEndDates(DateTime startSet, DateTime endSet, UberSpan span, DateTime startExpected, DateTime endExpected, string msg)
		{
			TimeWindowCore target = new TimeWindowCore(startSet, endSet, span);

			AssertDatesMatch(startExpected, endExpected, target, msg);
		}

		private void AssertDatesMatch(DateTime startExpected, DateTime endExpected, TimeWindowCore target, string msg)
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

		private void AssertOnGoDay(DateTime startSet, DateTime endSet, UberSpan span, DateTime startExpected, DateTime endExpected, string msg)
		{
			TimeWindowCore target = new TimeWindowCore(startSet, endSet, span);

			target.GoDay();

			AssertDatesMatch(startExpected, endExpected, target, msg);
		}

		private void AssertOnGoWeek(DateTime startSet, DateTime endSet, UberSpan span, DateTime startExpected, DateTime endExpected, string msg)
		{
			TimeWindowCore target = new TimeWindowCore(startSet, endSet, span);

			target.GoWeek();

			AssertDatesMatch(startExpected, endExpected, target, msg);
		}

		private void AssertOnGoMonth(DateTime startSet, DateTime endSet, UberSpan span, DateTime startExpected, DateTime endExpected, string msg)
		{
			TimeWindowCore target = new TimeWindowCore(startSet, endSet, span);

			target.GoMonth();

			AssertDatesMatch(startExpected, endExpected, target, msg);
		}

		private void AssertOnGoYear(DateTime startSet, DateTime endSet, UberSpan span, DateTime startExpected, DateTime endExpected, string msg)
		{
			TimeWindowCore target = new TimeWindowCore(startSet, endSet, span);

			target.GoYear();

			AssertDatesMatch(startExpected, endExpected, target, msg);
		}

		private void AssertOnGoNext(DateTime startSet, DateTime endSet, UberSpan span, DateTime startExpected, DateTime endExpected, string msg)
		{
			TimeWindowCore target = new TimeWindowCore(startSet, endSet, span);

			target.GoNext();

			AssertDatesMatch(startExpected, endExpected, target, msg);
		}

		private void AssertOnGoPrevious(DateTime startSet, DateTime endSet, UberSpan span, DateTime startExpected, DateTime endExpected, string msg)
		{
			TimeWindowCore target = new TimeWindowCore(startSet, endSet, span);

			target.GoPrevious();

			AssertDatesMatch(startExpected, endExpected, target, msg);
		}
		#endregion
	}
}