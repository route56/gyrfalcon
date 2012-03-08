using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesktopClient.ClientInterface;

namespace MockDesktopClient
{
	class MockProxy : IProxy
	{
		public MockProxy()
		{
			Server = "MockServer";
			Port = 111;
			Username = "MockUser";
			Password = "MockPassword";
		}

		public string Server { get; set;}

		public int Port { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }
	}
}
