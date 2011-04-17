using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessMonitor
{
	public class ProcessData
	{
		public int TempData;

		public override bool Equals(object obj)
		{
			ProcessData pd = obj as ProcessData;
			return pd != null && this.TempData == pd.TempData;
		}
	}
}
