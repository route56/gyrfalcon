using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStore.Test
{
	class DataAtomGenerator
	{
		private Random _rand = new Random();
		DateTime _start = new DateTime(1995, 1, 1);

		private DateTime RandomDateTime()
		{
			int range = ((TimeSpan)(DateTime.Today - _start)).Days;

			return _start.AddDays(_rand.Next(range))
						.AddHours(_rand.Next(23))
						.AddMinutes(_rand.Next(59))
						.AddSeconds(_rand.Next(59));
		}

		private string RandomString()
		{
			int size = _rand.Next(10) + 1;

			StringBuilder builder = new StringBuilder();
			char ch;

			for (int i = 0; i < size; i++)
			{
				ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * _rand.NextDouble() + 65)));
				builder.Append(ch);
			}

			return builder.ToString();
		}

		public DateTime? Time { get; set; }
		public string Process { get; set; }
		public string Title { get; set; }
		public long? Frequency { get; set; }

		/// <summary>
		/// Infinite data stream. Use Take or TakeWhile to limit number of items
		/// </summary>
		/// <returns></returns>
		private IEnumerable<DataAtom> RandomDataStream()
		{
			while (true)
			{
				yield return new DataAtom()
				{
					Time = Time.HasValue ? Time.Value : RandomDateTime(),
					Process = Process != null ? Process : RandomString(),
					Title = Title != null ? Title : RandomString(),
					Frequency = Frequency.HasValue ? Frequency.Value : _rand.Next(10000)
				};
			}
		}

		/// <summary>
		/// Computes array for given size now
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		public DataAtom[] RandomDataStreamTakeNow(int size)
		{
			return RandomDataStream().Take(size).ToArray();
		}

		public DataAtom[] RandomDataStreamTakeWhileNow(Func<DataAtom, Boolean> predicate)
		{
			return RandomDataStream().TakeWhile(predicate).ToArray();
		}
	}
}
