using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopClient
{
	public interface IToolStripMenuItem
	{
		bool IsSnoozed { get; set; }
		TimeSpan Duration { get; set; }
	}
}
