using DataStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using DataStore.Contract;
using System.Threading;

namespace DataStore.Test
{
	[TestClass()]
	public abstract class QueryStoreTest
	{
		public abstract IWriteStore Writer { get; set; }
		public abstract IQueryStore Reader { get; set; }

		//Use TestInitialize to run code before running each test
		[TestInitialize()]
		public abstract void MyTestInitialize();
		
		// Use TestCleanup to run code after each test has run
		[TestCleanup()]
		public abstract void MyTestCleanup();

		/// <summary>
		///A test for GetGroupedData
		///</summary>
		[TestMethod()]
		public void GetGroupedDataTest_AddOneExpectSame()
		{
			DateTime eventTime = new DateTime(2012, 2, 2, 10, 1, 1);
			string activity = "foo";
			long timeSpan = 100;

			using (Writer)
			{
				List<DataAtom> dataList = new List<DataAtom>();

				dataList.Add(new DataAtom() 
					{
						Time = eventTime,
						Process = activity,
						Title = "bar",
						Frequency = timeSpan
					});

				Writer.AddToAggregatedStore(dataList);
				Thread.Sleep(1000);
			}

			DateTime startTime = eventTime.AddHours(-1);
			DateTime endTime = eventTime.AddHours(1);

			List<GroupedDataFormat> actual = new List<GroupedDataFormat>(Reader.GetGroupedData(startTime, endTime));

			Assert.AreEqual(actual.Count, 1);
			Assert.AreEqual(actual[0].GroupBy.ToShortDateString(), eventTime.ToShortDateString());
			Assert.AreEqual(actual[0].Activity.Length, 1);
			Assert.AreEqual(actual[0].Activity[0], activity);
			Assert.AreEqual(actual[0].TimeSpan.Length, 1);
			Assert.AreEqual(actual[0].TimeSpan[0], timeSpan);
		}

		/// <summary>
		///A test for GetRankedData
		///</summary>
		[TestMethod()]
		public void GetRankedDataTest()
		{
			DateTime eventTime = new DateTime(2012, 2, 2, 10, 1, 1);
			string activity = "foo";
			long timeSpan = 100;

			using (Writer)
			{
				List<DataAtom> dataList = new List<DataAtom>();

				dataList.Add(new DataAtom()
				{
					Time = eventTime,
					Process = activity,
					Title = "bar",
					Frequency = timeSpan
				});

				Writer.AddToAggregatedStore(dataList);
				Thread.Sleep(1000);
			}

			DateTime startTime = eventTime.AddHours(-1);
			DateTime endTime = eventTime.AddHours(1);

			List<RankedDataFormat> actual = new List<RankedDataFormat>(Reader.GetRankedData(startTime, endTime));

			Assert.AreEqual(actual.Count, 1);
			Assert.AreEqual(actual[0].Rank, 1);
			Assert.AreEqual(actual[0].Activity, activity);
			Assert.AreEqual(actual[0].TimeSpan, timeSpan);
		}

		[TestMethod]
		public void RankedData_AddSome_GetByHourDayAndWeek()
		{
			DateTime eventTime = new DateTime(2012, 2, 2, 10, 1, 1);
			long frequency = 100; 
			using (Writer)
			{
				List<DataAtom> dataList = new List<DataAtom>()
				{
					new DataAtom()
					{
						Time = eventTime.AddHours(1), // Hour
						Process = "some",
						Title = "bar",
						Frequency = frequency
					},
					new DataAtom()
					{
						Time = eventTime.AddDays(1), // Next day
						Process = "some",
						Title = "bar",
						Frequency = frequency
					},
					new DataAtom()
					{
						Time = eventTime.AddDays(8), // Next week
						Process = "some",
						Title = "bar",
						Frequency = frequency
					}
				};

				Writer.AddToAggregatedStore(dataList);
				Thread.Sleep(1000);
			}

			List<RankedDataFormat> actual = new List<RankedDataFormat>(
				Reader.GetRankedData(eventTime, eventTime.AddHours(2)));

			Assert.AreEqual(actual.Count, 1);
			Assert.AreEqual(actual[0].TimeSpan, frequency, "Only one hour entry");

			actual = new List<RankedDataFormat>(
				Reader.GetRankedData(eventTime, eventTime.AddDays(2)));

			Assert.AreEqual(actual.Count, 1);
			Assert.AreEqual(actual[0].TimeSpan, 2 * frequency, "2 days entry aggregated");

			actual = new List<RankedDataFormat>(
				Reader.GetRankedData(eventTime, eventTime.AddDays(10)));

			Assert.AreEqual(actual.Count, 1);
			Assert.AreEqual(actual[0].TimeSpan, 3 * frequency, "3 days entry aggregated");
		}

		[TestMethod]
		public void RankedData_AddNone_ExpectNone()
		{
			DateTime eventTime = new DateTime(2012, 2, 2, 10, 1, 1);
			List<RankedDataFormat> actual = new List<RankedDataFormat>(
				Reader.GetRankedData(eventTime, eventTime.AddHours(2)));
			Assert.AreEqual(actual.Count, 0);

			actual = new List<RankedDataFormat>(
				Reader.GetRankedData(eventTime, eventTime.AddDays(2)));
			Assert.AreEqual(actual.Count, 0);

			actual = new List<RankedDataFormat>(
				Reader.GetRankedData(eventTime, eventTime.AddDays(10)));
			Assert.AreEqual(actual.Count, 0);
		}

		[TestMethod]
		public void RankedData_AddLotOfData_GetByHourDayAndWeek()
		{
			DateTime eventTime = new DateTime(2012, 2, 2, 10, 1, 1);
			List<DataAtom> dataList = new List<DataAtom>();

			for (int i = 0; i < 1000; i++)
			{
				int mod = i % 10;
				dataList.Add(new DataAtom()
					{
						Time = eventTime.AddHours(i),
						Process = mod.ToString() + "Process",
						Title = mod.ToString() + "Title",
						Frequency = mod + 1
					});
			}

			using (Writer)
			{
				Writer.AddToAggregatedStore(dataList);
				Thread.Sleep(1000);
			}

			Assert.Inconclusive("Based on input, validate output below");
			//List<RankedDataFormat> actual = new List<RankedDataFormat>(
			//    _reader.GetRankedData(eventTime, eventTime.AddHours(2)));

			//Assert.AreEqual(actual.Count, 1);
			//Assert.AreEqual(actual[0].TimeSpan, frequency, "Only one hour entry");

			//actual = new List<RankedDataFormat>(
			//    _reader.GetRankedData(eventTime, eventTime.AddDays(2)));

			//Assert.AreEqual(actual.Count, 1);
			//Assert.AreEqual(actual[0].TimeSpan, 2 * frequency, "2 days entry aggregated");

			//actual = new List<RankedDataFormat>(
			//    _reader.GetRankedData(eventTime, eventTime.AddDays(10)));

			//Assert.AreEqual(actual.Count, 1);
			//Assert.AreEqual(actual[0].TimeSpan, 3 * frequency, "3 days entry aggregated");
		}
		/*
		 * Request for Week, Day, Hour for same underlying data.
		 * After above request is made, then add new data and see things change!
		 * Aggregation done over same file
		 */
		/*
		 * Insert into query store. Retrieve aggregated.
		 * Tests can be called for day,week,month,year
		 * Case can be data present or not present
		 * Ordering of data.
		 * Values of aggregation
		 */
		// month -> per day. day -> per hour
		// week -> group by per day -> timespan
		// TODO Who will and when will be per day/per week summary generated?
		// total time span
	}
}
