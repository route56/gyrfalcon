using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesktopClient.ClientInterface;

namespace DesktopClient.ManualTest
{
	class CoreIdeaRefactored
	{
		internal void Start()
		{
			IClientInterface target = new ClientManager();

			target.Start();

			Console.WriteLine("Press enter to stop...");
			Console.ReadKey();

			target.Stop();
		}
	}
}
