using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopClient.ClientInterface
{
	public interface IToolStripMenuItem
	{
		bool IsSnoozed { get; set; }
		TimeSpan Duration { get; set; }
	}
}
