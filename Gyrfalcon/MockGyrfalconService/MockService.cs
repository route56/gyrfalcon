using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MockGyrfalconService
{
	public class MockService
	{
		public List<DataFormat> GetActivityDetails(DateTime startTime, DateTime endTime)
		{
			List<DataFormat> result = new List<DataFormat>();

			using (System.IO.StreamReader sr = System.IO.File.OpenText(@"MockData.txt"))
			{
				string line;

				while ((line = sr.ReadLine()) != null)
				{
					string[] words = line.Split('\t');

					DataFormat data = new DataFormat()
					{
						Rank = Int32.Parse(words[0]),
						TimeSpent = long.Parse(words[1]),
						Activity = words[2],
						Document = words[3],
						Overview = words[4],
						Category = words[5],
						Productivity = Int32.Parse(words[6])
					};

					result.Add(data);
				}
			}

			return result;
		}

		public List<DataFormat> GetActivityGrouped(DateTime startTime, DateTime endTime)
		{
			var details = GetActivityDetails(startTime, endTime);

			Dictionary<string, DataFormat> map = new Dictionary<string,DataFormat>();

			foreach (var item in details)
			{
				if (map.ContainsKey(item.Activity))
				{
					map[item.Activity].TimeSpent += item.TimeSpent;
				}
				else
				{
					map.Add(item.Activity, item);
				}
			}

			return map.Values.ToList();
		}
	}
}
