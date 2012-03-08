using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesktopClient.ClientInterface;

namespace MockDesktopClient
{
	public class MockClientInterface : IClientInterface
	{
		public MockClientInterface()
		{
			Proxy = new MockProxy();
			GetFocused = new MockGetFocused();
			Dashboard = new MockDashboard();
			Settings = new MockSettings();
			Snooze = new MockSnooze();
			Alert = new MockAlert();
			OfflineTask = new MockOfflineTask();
			Status = new MockStatus();
		}

		public IProxy Proxy { get; private set; }
		public IGetFocused GetFocused { get; private set; }
		public IDashboard Dashboard { get; private set; }
		public ISettings Settings { get; private set; }
		public ISnooze Snooze { get; private set; }
		public IAlert Alert { get; private set; }
		public IOfflineTask OfflineTask { get; private set; }
		public IStatus Status { get; private set; }
	}
}
