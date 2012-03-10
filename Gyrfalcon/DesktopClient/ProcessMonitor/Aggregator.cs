using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopClient.ProcessMonitor
{
	public class Aggregator
	{
		private List<Tuple<int, int>> aggregation = new List<Tuple<int, int>>();

		private int start = 0;
		private int count = 0;
		private string lastItemA;
		private string lastItemB;

		public void Add(string itemA, string itemB)
		{
			if (count == 0)
			{
				lastItemA = itemA;
				lastItemB = itemB;
				count = 1;
			}
			else
			{
				if (lastItemA == itemA && lastItemB == itemB)
				{
					count++;
				}
				else
				{
					aggregation.Add(new Tuple<int, int>(start, count));

					start = start + count;
					lastItemA = itemA;
					lastItemB = itemB;
					count = 1;
				}
			}
		}

		public int[,] GetAggregationResult()
		{
			aggregation.Add(new Tuple<int, int>(start, count));

			int[,] result = new int[aggregation.Count, 2];

			for (int i = 0; i < aggregation.Count; i++)
			{
				result[i, 0] = aggregation[i].Item1;
				result[i, 1] = aggregation[i].Item2;
			}

			aggregation.RemoveAt(aggregation.Count - 1);

			return result;
		}
	}
}
