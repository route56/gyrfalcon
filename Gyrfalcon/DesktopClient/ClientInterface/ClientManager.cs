using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesktopClient.ProcessMonitor;

namespace DesktopClient.ClientInterface
{
	public class ClientManager : IClientInterface
	{
		private ProcessMonitorLauncher _processMon;

		public ClientManager()
		{
			Proxy = null;
			GetFocused = null;
			Dashboard = new DashboardManager();
			Settings = null;
			Snooze = null; // new SnoozeManager();
			Alert = new AlertManager();
			OfflineTask = null; //new OfflineDialogInterface();
			Status = new StatusManager();
		}

		public IProxy Proxy { get; private set; }
		public IGetFocused GetFocused { get; private set; }
		public IDashboard Dashboard { get; private set; }
		public ISettings Settings { get; private set; }
		public ISnooze Snooze { get; private set; }
		public IAlert Alert { get; private set; }
		public IOfflineTask OfflineTask { get; private set; }
		public IStatus Status { get; private set; }


		public void Start()
		{
			_processMon = new ProcessMonitorLauncher();
			_processMon.Start();
		}

		public void Stop()
		{
			_processMon.Stop();
		}
	}
}
