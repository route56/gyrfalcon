﻿using DataStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace DataStore.Test
{
	[TestClass()]
	public class QueryStoreTest
	{
		string _tempFileStorepath;
		private WriteStore _writer;
		private QueryStore _reader; 
		//Use TestInitialize to run code before running each test
		[TestInitialize()]
		public void MyTestInitialize()
		{
			_tempFileStorepath = Path.GetRandomFileName();
			Directory.CreateDirectory(_tempFileStorepath);
			_writer = new WriteStore();
			_reader = new QueryStore();

			FilePaths fileProvider = new FilePaths(_tempFileStorepath);
			_writer.FilePathProvider = fileProvider;
			_reader.FilePathProvider = fileProvider;
		}
		
		// Use TestCleanup to run code after each test has run
		[TestCleanup()]
		public void MyTestCleanup()
		{
			Directory.Delete(_tempFileStorepath, true);
			_tempFileStorepath = string.Empty;
			_writer = null;
			_reader = null;
		}

		/*
		 * Insert into query store. Retrieve aggregated.
		 * Tests can be called for day,week,month,year
		 * Case can be data present or not present
		 * Ordering of data.
		 * Values of aggregation
		 */

		/// <summary>
		///A test for GetGroupedData
		///</summary>
		[TestMethod()]
		public void GetGroupedDataTest_AddOneExpectSame()
		{
			DateTime eventTime = new DateTime(2012, 2, 2, 10, 1, 1);
			string activity = "foo";
			long timeSpan = 100;

			using (_writer)
			{
				List<DataAtom> dataList = new List<DataAtom>();

				dataList.Add(new DataAtom() 
					{
						Time = eventTime,
						Process = activity,
						Title = "bar",
						Frequency = timeSpan
					});

				_writer.AddToAggregatedStore(dataList);
			}

			DateTime startTime = eventTime.AddHours(-1);
			DateTime endTime = eventTime.AddHours(1);

			List<GroupedDataFormat> actual = new List<GroupedDataFormat>(_reader.GetGroupedData(startTime, endTime));

			Assert.AreEqual(actual.Count, 1);
			Assert.AreEqual(actual[0].Activity, activity);
			Assert.AreEqual(actual[0].TimeSpan, timeSpan);
			// Assert.AreEqual(actual[0].GroupBy,  ?????
			// month -> per day. day -> per hour
			// week -> group by per day -> timespan
			// total time span
		}

		/// <summary>
		///A test for GetRankedData
		///</summary>
		[TestMethod()]
		public void GetRankedDataTest()
		{
			WriteStore writer = new WriteStore();
			QueryStore reader = new QueryStore(); // TODO: Initialize to an appropriate value
			DateTime startTime = new DateTime(); // TODO: Initialize to an appropriate value
			DateTime endTime = new DateTime(); // TODO: Initialize to an appropriate value
			IEnumerable<RankedDataFormat> expected = null; // TODO: Initialize to an appropriate value
			IEnumerable<RankedDataFormat> actual;
			actual = reader.GetRankedData(startTime, endTime);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}
	}
}