using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStore
{
	public class Classifier
	{
		// 1st occurence index
		// key
		// count towards end.
		private Dictionary<string, Tuple<int, long>> dictionary = new Dictionary<string, Tuple<int, long>>();
		public void Add(int index, string key, long frequency)
		{
			if (dictionary.ContainsKey(key))
			{
				var oldVal = dictionary[key];

				dictionary[key] = new Tuple<int, long>(Math.Min(index, oldVal.Item1), oldVal.Item2 + frequency);
			}
			else
			{
				dictionary.Add(key, new Tuple<int,long>(index, frequency));
			}
		}

		public long[,] GetClassificationResult()
		{
			if (dictionary.Count > 0)
			{
				long[][] resultJagged = new long[dictionary.Count][];

				var values = dictionary.Values.ToArray();

				for (int i = 0; i < values.Length; i++)
				{
					resultJagged[i] = new long[] { values[i].Item1, values[i].Item2 };
				}

				// http://stackoverflow.com/questions/232395/how-do-i-sort-a-two-dimensional-array-in-c
				Sort<long>(resultJagged, 0);

				return ToRectangular<long>(resultJagged);
			}
			else
			{
				return new long[0,0];
			}
		}

		private static void Sort<T>(T[][] data, int col)
		{
			Comparer<T> comparer = Comparer<T>.Default;
			Array.Sort<T[]>(data, (x, y) => comparer.Compare(x[col], y[col]));
		}

		private static T[,] ToRectangular<T>(T[][] array)
		{
			int height = array.Length, width = array[0].Length;
			T[,] rect = new T[height, width];
			for (int i = 0; i < height; i++)
			{
				T[] row = array[i];
				for (int j = 0; j < width; j++)
				{
					rect[i, j] = row[j];
				}
			}
			return rect;
		}
	}
}
