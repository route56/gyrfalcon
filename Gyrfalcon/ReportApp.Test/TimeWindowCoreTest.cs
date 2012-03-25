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
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoDay_SetDays_ShouldntChange()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoWeek_SetDays_Goes()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoMonth_SetDays_Goes()
		{
			Assert.Inconclusive();
		}

		[TestMethod]
		public void GoYear_SetDays_Goes()
		{
			Assert.Inconclusive();
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


			//switch (UberSpan)
			//{
			//    case UberSpan.MultiYear:
			//        break;
			//    case UberSpan.Year:
			//        break;
			//    case UberSpan.Month:
			//        break;
			//    case UberSpan.Week:
			//        break;
			//    case UberSpan.Day:
			//        break;
			//    default:
			//        break;
		//}

		#endregion
	}
}