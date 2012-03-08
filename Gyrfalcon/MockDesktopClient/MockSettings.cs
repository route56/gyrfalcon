using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesktopClient.ClientInterface;

namespace MockDesktopClient
{
	class MockSettings : ISettings
	{
		public string GetSettingEditorUrl()
		{
			return "http://stackoverflow.com";
		}

		public string[] OfflineTaskCategory
		{
			get { return _offlineTasksCategories; }
		}

		private string[] _offlineTasksCategories = new string[] 
						{
							"Mock",
							"Is",
							"Mocking",
							"UI",
							"Hard",
							"Coded"
						};
	}
}
