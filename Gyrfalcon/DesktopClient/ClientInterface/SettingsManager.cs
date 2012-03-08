﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopClient.ClientInterface
{
	class SettingsManager : ISettings
	{
		public string[] OfflineTaskCategory
		{
			get { return _offlineTasksCategories; }
		}

		// TODO This has to be queried and picked from service.
		private string[] _offlineTasksCategories = new string[] 
						{
							"Meetings",
							"Phone",
							"Discussions",
							"Share",
							"Hard",
							"Coded"
						};

		string ISettings.GetSettingEditorUrl()
		{
			throw new NotImplementedException();
		}
	}
}
