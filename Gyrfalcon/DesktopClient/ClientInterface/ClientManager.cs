using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopClient.ClientInterface
{
	public class ClientManager : IClientInterface
	{
		//private IProxy proxy;
		//private IGetFocused getFocused;
		//private IDashboard dashboard;
		//private ISettings settings;
		private ISnooze snooze;
		//private IAlert alert;
		//private IOfflineTask offlineTask;
		//private IStatus status;

		public ClientManager()
		{
			snooze = new SnoozeManager();
		}

		public IProxy Proxy
		{
			get { throw new NotImplementedException(); }
		}

		public IGetFocused GetFocused
		{
			get { throw new NotImplementedException(); }
		}

		public IDashboard Dashboard
		{
			get { throw new NotImplementedException(); }
		}

		public ISettings Settings
		{
			get { throw new NotImplementedException(); }
		}

		public ISnooze Snooze
		{
			get { return snooze; }
		}

		public IAlert Alert
		{
			get { throw new NotImplementedException(); }
		}

		public IOfflineTask OfflineTask
		{
			get { throw new NotImplementedException(); }
		}

		public IStatus Status
		{
			get { throw new NotImplementedException(); }
		}
	}
}
