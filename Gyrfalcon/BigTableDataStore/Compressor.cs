using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStore;
using System.IO;

namespace BigTableDataStore
{
	public class Compressor
	{
		public IOrderedEnumerable<DataAtom> Compress(IEnumerable<DataAtom> sequence, Func<DateTime, DateTime> filter)
		{
			return sequence
				.Where(s => !(s.Process == "Idle" && string.IsNullOrEmpty(s.Title)))
				.GroupBy(s => filter(s.Time))
				.SelectMany(r => Classifier.GetClassificationResult(r))
				.OrderBy(s => s.Time);
		}

		/// <summary>
		/// Creates compressed file. If source == dest, this will replace source file
		/// </summary>
		/// <param name="source"></param>
		/// <param name="dest"></param>
		/// <param name="filter"></param>
		public void CompressFile(string source, string dest, Func<DateTime, DateTime> filter)
		{
			List<DataAtom> bigList = new List<DataAtom>();

			using (StreamReader sr = File.OpenText(source))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					bigList.Add(DataAtom.FromString(line));
				}
			}

			var compressed = Compress(bigList, filter);

			string tempfile = Path.GetRandomFileName();

			using (StreamWriter sw = File.CreateText(tempfile))
			{
				foreach (var item in compressed)
				{
					sw.WriteLine(item.ToString());
				}
			}

			File.Copy(tempfile, dest, true);
			File.Delete(tempfile);
		}
	}
}
