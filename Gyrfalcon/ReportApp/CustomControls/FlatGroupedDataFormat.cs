using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStore;

namespace ReportApp.CustomControls
{
	public class FlatGroupedDataFormat
	{
		public DateTime GroupBy { get; set; }
		public GroupWindowType GroupWindow { get; set; }
		public long TimeSpan { get; set; }
		public string Activity { get; set; }

		public static IEnumerable<FlatGroupedDataFormat> ConvertFromGroupedDataFormat(IEnumerable<GroupedDataFormat> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException();
			}

			foreach (var item in source)
			{
				var activityAndTimespan = item.Activity.Zip(item.TimeSpan,
					(a, t) => new { Act = a, TimeSp = t });

				foreach (var actAndTime in activityAndTimespan)
				{
					yield return new FlatGroupedDataFormat()
					{
						GroupBy = item.GroupBy,
						GroupWindow = item.GroupWindow,
						Activity = actAndTime.Act,
						TimeSpan = actAndTime.TimeSp
					};
				}
			}
		}
	}
}