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
			IClientInterface client = 
				//new MockDesktopClient.MockClientInterface();
				new ClientManager();
			client.Start();
			Application.Run(new Preferences(client));
			client.Stop();
		}
	}
}
