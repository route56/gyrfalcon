using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopClient.ClientInterface
{
	public class StatusManager : IStatus
	{
		public StatusManager()
		{
			_lastSuccessfulTransmission = DateTime.Now;
		}

		public bool IsEverythingOk
		{
			get { return true; }
		}

		public DateTime LastSuccessfulTransmission
		{
			get { return _lastSuccessfulTransmission; }
			set 
			{ 
				_lastSuccessfulTransmission = value;

				if (StatusChanged != null)
				{
					StatusChanged();
				}
			}
		}

		public string ClientVersion
		{
			get { return "Gyrfalcon 0.2"; }
		}

		public string VersionHistoryURL
		{
			get { return "http://gyrfalcon.codeplex.com/releases/"; }
		}

		public event Action StatusChanged;

		private DateTime _lastSuccessfulTransmission;
	}
}
