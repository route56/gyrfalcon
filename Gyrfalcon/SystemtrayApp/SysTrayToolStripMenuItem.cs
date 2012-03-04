using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DesktopClient.ClientInterface;

namespace SystemtrayApp
{
	class SysTrayToolStripMenuItem : IToolStripMenuItem
	{
		private ToolStripMenuItem _menuItem;

		public ToolStripMenuItem MenuItem 
		{
			get { return _menuItem; } 
		}

		public SysTrayToolStripMenuItem(ToolStripMenuItem menuItem)
		{
			_menuItem = menuItem;
		}

		public bool IsSnoozed
		{
			get
			{
				return _menuItem.Checked;
			}
			set
			{
				_menuItem.Checked = value;
			}
		}

		public TimeSpan Duration { get; set; }
	}
}
