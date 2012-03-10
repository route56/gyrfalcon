using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesktopClient.ProcessMonitor;

namespace DesktopClient.Test.ProcessMonitor
{
	[TestClass]
	public class AggregatorTest
	{
		[TestMethod]
		public void GetAggregationResult_NormalInput_ProperCount()
		{
			string[,] input = { 
					{ "Hello", "World" }, 
					{ "Hello", "World" }, 
					{ "Hello", "World" }, 
					{ "Hello1", "World" }, 
					{ "Hello1", "World" }, 
					{ "Hello1", "World1" }, 
					{ "Hello1", "World" }, 
					{ "Hello1", "World" }, 
					{ "Hello", "World" }
							  };

			int[,] expected = { 
					{ 0, 3 }, 
					{ 3, 2}, 
					{ 5, 1}, 
					{ 6, 2}, 
					{ 8, 1}
							  };

			TestTarget(input, expected);
		}

		[TestMethod]
		public void GetAggregationResult_OneInput_ProperCount()
		{
			string[,] input = { { "Hello", "World" } };

			int[,] expected = { { 0, 1 } };

			TestTarget(input, expected);
		}

		[TestMethod]
		public void GetAggregationResult_TwoInput_ProperCount()
		{
			string[,] input = { { "Hello", "World" }, { "Hello", "World" } };

			int[,] expected = { { 0, 2 } };

			TestTarget(input, expected);
		}

		[TestMethod]
		public void GetAggregationResult_TwoDifferentInput_ProperCount()
		{
			string[,] input = { { "Hello", "World" }, { "Hello1", "World" } };

			int[,] expected = { { 0, 1 }, {1, 1} };

			TestTarget(input, expected);
		}

		[TestMethod]
		public void GetAggregationResult_ZeroInput_ProperCount()
		{
			string[,] input = new string[0, 2];

			int[,] expected = { { 0, 0 } };

			TestTarget(input, expected);
		}

		private void TestTarget(string[,] input, int[,] expected)
		{
			Aggregator target = new Aggregator();

			for (int i = 0; i < input.GetLength(0); i++)
			{
				target.Add(input[i, 0], input[i, 1]);
			}

			int[,] actual = target.GetAggregationResult();

			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void GetAggregationResult_RealSample_ProperCount()
		{
			string[,] input = {
{"EXCEL",	"Microsoft Excel - FeatureAreas.xlsx"},
{"EXCEL",	"Microsoft Excel - FeatureAreas.xlsx"},
{"EXCEL",	"Microsoft Excel - FeatureAreas.xlsx"},
{"EXCEL",	"Microsoft Excel - FeatureAreas.xlsx"},
{"EXCEL",	"Microsoft Excel - FeatureAreas.xlsx"},
{"EXCEL",	"Microsoft Excel - FeatureAreas.xlsx"},
{"EXCEL",	"Microsoft Excel - FeatureAreas.xlsx"},
{"EXCEL",	"Microsoft Excel - FeatureAreas.xlsx"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"Idle",	""},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	""},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	""},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	""},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"},
{"devenv",	"Gyrfalcon - Microsoft Visual Studio"}
};

			int[,] expected = 
			{
{0,	8},
{8,	15},
{23,	1},
{24,	1},
{25,	1},
{26,	7},
{33,	1},
{34,	1},
{35,	1},
{36,	2}
			};

			TestTarget(input, expected);
		}

		[TestMethod]
		public void GetAggregationResult_CalledMultipleTimes_ProperCountInEnd()
		{
			string[,] input = { 
					{ "Hello", "World" }, 
					{ "Hello", "World" }, 
					{ "Hello", "World" }, 
					{ "Hello1", "World" }, 
					{ "Hello1", "World" }, 
					{ "Hello1", "World1" }, 
					{ "Hello1", "World" }, 
					{ "Hello1", "World" }, 
					{ "Hello", "World" }
							  };

			int[,] expected = { 
					{ 0, 3 }, 
					{ 3, 2}, 
					{ 5, 1}, 
					{ 6, 2}, 
					{ 8, 1}
							  };

			Aggregator target = new Aggregator();

			for (int i = 0; i < input.GetLength(0); i++)
			{
				target.Add(input[i, 0], input[i, 1]);

				target.GetAggregationResult();
			}

			int[,] actual = target.GetAggregationResult();

			CollectionAssert.AreEqual(expected, actual);
		}
	}
}
