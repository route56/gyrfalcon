using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopClient.ManualTest
{
	class MainData
	{
		public DateTime Time;
		public string Process;
		public string Title;
		public long Frequency;

		public override string ToString()
		{
			return string.Format("{0}\t{1}\t{2}\t{3}", Time, Process, Title, Frequency);
		}

		public static MainData FromString(string str)
		{
			try
			{
				string[] arr = str.Split('\t');

				return new MainData()
				{
					Time = DateTime.Parse(arr[0]),
					Process = arr[1],
					Title = arr[2],
					Frequency = Int32.Parse(arr[3])
				};
			}
			catch (Exception)
			{
				throw new ArgumentException();
			}
		}
	}
}
