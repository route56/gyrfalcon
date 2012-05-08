using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStore;

namespace BigTableDataStore
{
	public class Compressor
	{
		/* Compress per hour. Per Day?. Per Week?
		 * Write in same file or new file?
		 * Remove from original file or keep it.
		 */
		public IOrderedEnumerable<DataAtom> Compress(IOrderedEnumerable<DataAtom> sequence, Func<DateTime, DateTime> filter)
		{
			List<DataAtom> compressed = new List<DataAtom>();

			var grouped = sequence.GroupBy(s => filter(s.Time));

			foreach (var group in grouped)
			{
				Classifier classify = new Classifier();
				DataAtom[] input = group.ToArray();

				for (int i = 0; i < input.Length; i++)
				{
					classify.Add(0, input[i].Process + input[i].Title, input[i].Frequency);
				}

				long[,] result = classify.GetClassificationResult();

				for (int i = 0; i < result.GetLength(0); i++)
				{
					input[(int)result[i, 0]].Frequency = result[i, 1];
					compressed.Add(input[(int)result[i, 0]]);
				}
			}

			return compressed.OrderBy(s => s.Time);
		}
	}
}
