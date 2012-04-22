using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.TimeWindow;
using System.IO;
using DataStore.Contract;

namespace DataStore
{
	public class QueryStore : IQueryStore
	{
		public QueryStore()
		{
			FilePathProvider = new FilePaths();
		}

		public IEnumerable<GroupedDataFormat> GetGroupedData(DateTime startTime, DateTime endTime)
		{
			return new DataFormatConvertor(GetData(startTime, endTime)).ToGroupedDataFormat();
		}

		public IEnumerable<RankedDataFormat> GetRankedData(DateTime startTime, DateTime endTime)
		{
			return new DataFormatConvertor(GetData(startTime, endTime)).ToRankedDataFormat();
		}

		private IEnumerable<DataAtom> GetData(DateTime start, DateTime end)
		{
			return FilePathProvider.GetFilesToRead(start, end)
				.SelectMany(file => LoadDataFromFile(file))
				.Where(data => data.Time >= start && data.Time <= end);
		}

		private IEnumerable<DataAtom> LoadDataFromFile(string file)
		{
			List<DataAtom> data = new List<DataAtom>();

			using (StreamReader sr = File.OpenText(file))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					data.Add(DataAtom.FromString(line));
				}
			}

			return data;
		}

		public FilePaths FilePathProvider { get; set; }
	}
}
