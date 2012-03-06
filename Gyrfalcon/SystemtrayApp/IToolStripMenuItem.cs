using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemtrayApp
{
	public interface IToolStripMenuItem
	{
		bool IsSnoozed { get; set; }
		TimeSpan Duration { get; set; }
	}
}
