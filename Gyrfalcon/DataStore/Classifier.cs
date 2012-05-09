using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStore
{
	public class Classifier
	{
		class Info
		{
			public int Index { get; set; }
			public string Key { get; set; }
			public long Frequency { get; set; }
		}

		IList<Info> source = new List<Info>();

		public void Add(int index, string key, long frequency)
		{
			source.Add(new Info() { Index = index, Key = key, Frequency = frequency });
		}

		// TODO Refactor this signature. Return IEnumerable<DataAtom>. Get Rid of Add method too.
		public long[,] GetClassificationResult()
		{
			if (source.Count > 0)
			{
				var result = source.GroupBy(s => s.Key)
					.Select(r => new Info()
					{
						Index = r.Min(t => t.Index),
						Key = r.Key,
						Frequency = r.Sum(t => t.Frequency)
					})
					.OrderBy(r => r.Index)
					.ToArray();

				long[,] ans = new long[result.Length, 2];

				for (int i = 0; i < result.Length; i++)
				{
					ans[i, 0] = result[i].Index;
					ans[i, 1] = result[i].Frequency;
				}

				return ans;
			}
			else
			{
				return new long[0,0];
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input">Input should have atleast one element</param>
		/// <returns></returns>
		public static IEnumerable<DataAtom> GetClassificationResult(IEnumerable<DataAtom> input)
		{
			return input.GroupBy(s => s.Process + s.Title)
				.Select(r => new DataAtom()
				{
					Process = r.First().Process,
					Title = r.First().Title,
					Time = r.Min(t => t.Time),
					Frequency = r.Sum(t => t.Frequency)
				});
		}
	}
}
