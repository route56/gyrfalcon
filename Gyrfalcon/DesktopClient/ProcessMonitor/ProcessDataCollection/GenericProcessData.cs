using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopClient.ProcessMonitor
{
	public class GenericProcessData : ProcessData
	{
		private List<GenericProcessData> _subProcess;

		public override List<IProcessData> GetSubProcessData()
		{
			if (_subProcess == null)
			{
				_subProcess = new List<GenericProcessData>();
			}

			return _subProcess.Cast<IProcessData>().ToList(); // Or _subProcess.ConvertAll(x => (IProcessData)x); http://stackoverflow.com/questions/1817300/convert-list-of-derived-class-objects-to-list-of-base-class-objects
		}
	}
}
