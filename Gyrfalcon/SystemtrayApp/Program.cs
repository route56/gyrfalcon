using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DesktopClient.ClientInterface;

namespace SystemtrayApp
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			ClientManager client = new ClientManager();
			Application.Run(new Form1(client));
			//Application.Run(new OfflineTimeForm(client));
		}
	}
}
