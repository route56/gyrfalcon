using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesktopClient.ProcessMonitor;

namespace DesktopClient.Test.ProcessMonitor
{
	[TestClass]
	public class ClassifierTest
	{
		[TestMethod]
		public void GetClassificationResult_Basic_ExpectedCount()
		{
			string[,] input = { 
					{ "Hello World", "3" }, 
					{ "Hello1 World", "2" }, 
					{ "Hello World", "1" }, 
					{ "Hello1 World1", "3" }, 
					{ "Hello1 World", "5" }, 
					{ "Hello World", "1" }
							  };

			int[,] expected = { 
					{ 0, 5 }, 
					{ 1, 7}, 
					{ 3, 3}
							  };

			TestClassifier(input, expected);
		}

		private void TestClassifier(string[,] input, int[,] expected)
		{
			Classifier cs = new Classifier();

			for (int i = 0; i < input.GetLength(0); i++)
			{
				cs.Add(i, input[i, 0], Int32.Parse(input[i, 1]));
			}

			int[,] actual = cs.GetClassificationResult();

			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void GetClassificationResult_OneItem_ExpectedCount()
		{
			string[,] input = { {"hello", "3" }};

			int[,] expected = { { 0, 3 } };

			TestClassifier(input, expected);
		}

		[TestMethod]
		public void GetClassificationResult_TwoItems_ExpectedCount()
		{
			string[,] input = { { "hello", "3" }, {"World", "2"} };

			int[,] expected = { { 0, 3 }, {1, 2} };

			TestClassifier(input, expected);
		}

		[TestMethod]
		public void GetClassificationResult_TwoSameItems_ExpectedCount()
		{
			string[,] input = { { "hello", "3" }, { "hello", "2" } };

			int[,] expected = { { 0, 5 } };

			TestClassifier(input, expected);
		}

		[TestMethod]
		public void GetClassificationResult_FourAlternateItems_ExpectedCount()
		{
			string[,] input = { { "hello", "3" }, { "World", "2" }, {"hello", "7"}, {"World", "10"} };

			int[,] expected = { { 0, 10 }, { 1, 12 } };

			TestClassifier(input, expected);
		}

		[TestMethod]
		public void GetClassificationResult_5Items3Distinct_ExpectedCount()
		{
			string[,] input = { 
							  { "hello", "3" }, 
							  { "world", "2" },
							  { "hello", "5" },
							  { "how", "8" },
							  { "hello", "100" }
							  };

			int[,] expected = { { 0, 108 }, { 1, 2 }, {3, 8} };

			TestClassifier(input, expected);
		}

		[TestMethod]
		public void GetClassificationResult_5DistinctItems_ExpectedCount()
		{
			string[,] input = { 
							  { "hello", "3" }, 
							  { "world", "2" },
							  { "how", "5" },
							  { "are", "8" },
							  { "you", "100" }
							  };

			int[,] expected = { { 0, 3 }, { 1, 2 }, { 2, 5 }, { 3, 8 }, {4, 100} };

			TestClassifier(input, expected);
		}

		[TestMethod]
		public void GetClassificationResult_5Items3Distinct_InsertOrderDifferent_ExpectedCount()
		{
			string[,] input = { 
							  { "hello", "3" }, 
							  { "world", "2" },
							  { "hello", "5" },
							  { "how", "8" },
							  { "hello", "100" }
							  };

			int[,] expected = { { 0, 108 }, { 1, 2 }, { 3, 8 } };

			Classifier cs = new Classifier();

			for (int i = input.GetLength(0) - 1; i >= 0 ; i--)
			{
				cs.Add(i, input[i, 0], Int32.Parse(input[i, 1]));
			}

			int[,] actual = cs.GetClassificationResult();

			CollectionAssert.AreEqual(expected, actual);
		}
	}
}
