﻿using BigTableDataStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataStore;
using System.Linq;
using System.Collections.Generic;

namespace DataStore.Test
{
	[TestClass()]
	public class CompressorTest
	{
		[TestMethod()]
		public void CompressTest_SingleTime()
		{
			DataAtomGenerator gen = new DataAtomGenerator();
			Compressor target = new Compressor();

			gen.Time = DateTime.Now;
			gen.Process = "Foo";
			gen.Title = "Title";
			gen.Frequency = 1;
			int count = 4;

			var sequence = gen.RandomDataStreamTakeNow(count);

			var actual = target.Compress(sequence, a => a);

			Assert.AreEqual(1, actual.Count());
			Assert.AreEqual(gen.Frequency * count, actual.ElementAt(0).Frequency);
		}

		[TestMethod()]
		public void CompressTest_TwoTime()
		{
			DataAtomGenerator gen = new DataAtomGenerator();
			Compressor target = new Compressor();

			gen.Time = DateTime.Now.AddDays(-1);
			gen.Process = "Foo";
			gen.Title = "Title";
			gen.Frequency = 1;
			int count1 = 4;
			int count2 = 5;

			var sequence = gen.RandomDataStreamTakeNow(count1);

			gen.Time = gen.Time.Value.AddDays(1);

			sequence = sequence.Union(gen.RandomDataStreamTakeNow(count2)).ToArray();

			var actual = target.Compress(sequence, a => a);

			Assert.AreEqual(2, actual.Count());
			Assert.AreEqual(gen.Frequency * count1, actual.ElementAt(0).Frequency);
			Assert.AreEqual(gen.Frequency * count2, actual.ElementAt(1).Frequency);
		}

		[TestMethod()]
		public void CompressTest_TwoTypesOneIdle_IgnoresIdle()
		{
			DataAtomGenerator gen = new DataAtomGenerator();
			Compressor target = new Compressor();

			gen.Time = DateTime.Now;
			gen.Process = "Foo";
			gen.Title = "Title";

			var sequence = gen.RandomDataStreamTakeNow(10);

			gen.Process = "Idle";
			gen.Title = string.Empty;
			sequence = sequence.Union(gen.RandomDataStreamTakeNow(20)).ToArray();

			var actual = target.Compress(sequence, a => a);

			Assert.AreEqual(1, actual.Count());
		}
	}
}
