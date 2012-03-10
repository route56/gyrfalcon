using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopClient.ClientInterface
{
	class StatusManager : IStatus
	{
		public bool IsEverythingOk
		{
			get { throw new NotImplementedException(); }
		}

		public DateTime LastSuccessfulTransmission
		{
			get { throw new NotImplementedException(); }
		}

		public string ClientVersion
		{
			get { throw new NotImplementedException(); }
		}

		public string VersionHistoryURL
		{
			get { throw new NotImplementedException(); }
		}

		public event Action StatusChanged;
	}
}
