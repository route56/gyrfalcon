using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopClient.ClientStorage
{
	public class DataAtom
	{
		public DateTime Time;
		public string Process
		{
			get { return _process; }
			set { _process = value.Replace('\t', ' '); }
		}

		public string Title
		{
			get { return _title; }
			set { _title = value.Replace('\t', ' '); }
		}
		public int Frequency;
		private string _process;
		private string _title;

		public override string ToString()
		{
			return string.Format("{0}\t{1}\t{2}\t{3}", Time, Process, Title, Frequency);
		}

		public static DataAtom FromString(string str)
		{
			try
			{
				string[] arr = str.Split('\t');

				return new DataAtom()
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
