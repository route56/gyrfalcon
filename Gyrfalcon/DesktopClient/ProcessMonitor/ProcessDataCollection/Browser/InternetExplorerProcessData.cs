﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopClient.ProcessMonitor
{
	public class InternetExplorerProcessData : BrowserProcessData
	{
		public override List<IProcessData> GetSubProcessData()
		{
			throw new NotImplementedException();
		}
	}
}
