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
			TimeWindowCore target = new TimeWindowCore();

			DateTime now = DateTime.Now;

			target.StartTime = now;
			target.EndTime = now;

			Assert.AreEqual(target.StartTime.Hour, 0);
			Assert.AreEqual(target.StartTime.Minute, 0);
			Assert.AreEqual(target.StartTime.Second, 0);
			Assert.AreEqual(target.StartTime.Millisecond, 0);

			Assert.AreEqual(target.EndTime.Hour, 23);
			Assert.AreEqual(target.EndTime.Minute, 59);
			Assert.AreEqual(target.EndTime.Second, 59);
			Assert.AreEqual(target.EndTime.Millisecond, 999);
		}

		#region Day
		[TestMethod]
		public void GetUberSpan_SetDates_Day()
		{
			AssertOnUberSpan(new DateTime(2012, 2, 3),
				new DateTime(2012, 2, 3),
				UberSpan.Day,
				"Same day treated as one day");

			AssertOnUberSpan(new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				UberSpan.Day,
				"Leap year check :P Azure went down 8 hrs on this day!");
		}

		[TestMethod]
		public void StartEnd_SetDates_Day()
		{
			AssertOnStartEndDates(new DateTime(2012, 2, 3),
				new DateTime(2012, 2, 3),
				new DateTime(2012, 2, 3),
				new DateTime(2012, 2, 3),
				"Start and end should have same date");

			AssertOnStartEndDates(new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				"Leap year check");
		}

		[TestMethod]
		public void GoDay_SetDays_ShouldntChange()
		{
			AssertOnGoDay(new DateTime(2012, 2, 3),
				new DateTime(2012, 2, 3),
				new DateTime(2012, 2, 3),
				new DateTime(2012, 2, 3),
				"Day level shouldn't change");

			AssertOnGoDay(new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				"Leap year check Day level shouldn't change");
		}

		[TestMethod]
		public void GoWeek_SetDays_Goes()
		{
			AssertOnGoWeek(new DateTime(2012, 2, 10),
				new DateTime(2012, 2, 10),
				new DateTime(2012, 2, 5),
				new DateTime(2012, 2, 11),
				"10th feb is friday and falls in week Sun 5 to Sat 11");

			AssertOnGoWeek(new DateTime(2012, 2, 5),
				new DateTime(2012, 2, 5),
				new DateTime(2012, 2, 5),
				new DateTime(2012, 2, 11),
				"5th feb is Sunday and falls in week Sun 5 to Sat 11");

			AssertOnGoWeek(new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 26),
				new DateTime(2012, 3, 3),
				"Leap year check. Falls between 26th Feb Sun, 3rd March");
		}

		[TestMethod]
		public void GoMonth_SetDays_Goes()
		{
			AssertOnGoMonth(new DateTime(2012, 2, 10),
				new DateTime(2012, 2, 10),
				new DateTime(2012, 2, 1),
				new DateTime(2012, 2, 29),
				"10th feb falls in feb month");

			AssertOnGoMonth(new DateTime(2012, 3, 5),
				new DateTime(2012, 3, 5),
				new DateTime(2012, 3, 1),
				new DateTime(2012, 3, 31),
				"5th march in March");

			AssertOnGoMonth(new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 1),
				new DateTime(2012, 2, 29),
				"Leap year check. Falls in Feb");
		}

		[TestMethod]
		public void GoYear_SetDays_Goes()
		{
			AssertOnGoYear(new DateTime(2007, 2, 10),
				new DateTime(2007, 2, 10),
				new DateTime(2007, 1, 1),
				new DateTime(2007, 12, 31),
				"2007 year");

			AssertOnGoYear(new DateTime(2010, 3, 5),
				new DateTime(2010, 3, 5),
				new DateTime(2010, 1, 1),
				new DateTime(2010, 12, 31),
				"2010 year");

			AssertOnGoYear(new DateTime(2012, 2, 29),
				new DateTime(2012, 2, 29),
				new DateTime(2012, 1, 1),
				new DateTime(2012, 12, 31),
				"Leap year check. 2012 year");
		}

		[TestMethod]
		public void GoNext_SetDays_Goes()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoPrevious_SetDays_Goes()
		{
			Assert.Inconclusive();
		}

		#endregion

		#region week
		[TestMethod]
		public void GetUberSpan_SetDates_Week()
		{
			AssertOnUberSpan(new DateTime(2012, 2, 2),
				new DateTime(2012, 2, 3),
				UberSpan.Week,
				"Two day treated as partial week");

			AssertOnUberSpan(new DateTime(2012, 3, 4),
				new DateTime(2012, 3, 10),
				UberSpan.Week,
				"Sunday beginning and end");

			AssertOnUberSpan(new DateTime(2012, 3, 5),
				new DateTime(2012, 3, 8),
				UberSpan.Week,
				"mid week");
		}

		[TestMethod]
		public void StartEnd_SetDates_Week()
		{
			Assert.Inconclusive();
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
				"Adjacent over weekend, sat, then sunday treated as month");

			AssertOnUberSpan(new DateTime(2012, 3, 2),
				new DateTime(2012, 3, 15),
				UberSpan.Month,
				"Sub month");

			AssertOnUberSpan(new DateTime(2012, 3, 1),
				new DateTime(2012, 3, 31),
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
				"Quarter, but treated as year");

			AssertOnUberSpan(new DateTime(2011, 3, 15),
				new DateTime(2011, 9, 26),
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
				"Two years 2011, 2012");

			AssertOnUberSpan(new DateTime(2011, 12, 31),
				new DateTime(2012, 1, 1),
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
		void AssertOnUberSpan(DateTime start, DateTime end, UberSpan expected, string msg)
		{
			TimeWindowCore target = new TimeWindowCore();

			target.StartTime = start;
			target.EndTime = end;

			Assert.AreEqual(target.GetUberSpan(), expected, msg);
		}

		private void AssertOnStartEndDates(DateTime startSet, DateTime endSet, DateTime startExpected, DateTime endExpected, string msg)
		{
			TimeWindowCore target = new TimeWindowCore();

			target.StartTime = startSet;
			target.EndTime = endSet;

			AssertDatesMatch(startExpected, endExpected, target, msg);
		}

		private static void AssertDatesMatch(DateTime startExpected, DateTime endExpected, TimeWindowCore target, string msg)
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

		private void AssertOnGoDay(DateTime startSet, DateTime endSet, DateTime startExpected, DateTime endExpected, string msg)
		{
			TimeWindowCore target = new TimeWindowCore();

			target.StartTime = startSet;
			target.EndTime = endSet;

			target.GoDay();

			AssertDatesMatch(startExpected, endExpected, target, msg);
		}

		private void AssertOnGoWeek(DateTime startSet, DateTime endSet, DateTime startExpected, DateTime endExpected, string msg)
		{
			TimeWindowCore target = new TimeWindowCore();

			target.StartTime = startSet;
			target.EndTime = endSet;

			target.GoWeek();

			AssertDatesMatch(startExpected, endExpected, target, msg);
		}

		private void AssertOnGoMonth(DateTime startSet, DateTime endSet, DateTime startExpected, DateTime endExpected, string msg)
		{
			TimeWindowCore target = new TimeWindowCore();

			target.StartTime = startSet;
			target.EndTime = endSet;

			target.GoMonth();

			AssertDatesMatch(startExpected, endExpected, target, msg);
		}

		private void AssertOnGoYear(DateTime startSet, DateTime endSet, DateTime startExpected, DateTime endExpected, string msg)
		{
			TimeWindowCore target = new TimeWindowCore();

			target.StartTime = startSet;
			target.EndTime = endSet;

			target.GoYear();

			AssertDatesMatch(startExpected, endExpected, target, msg);
		}
		#endregion
	}
}