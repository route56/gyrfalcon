using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProcessMonitor;
using System.Diagnostics;
using System.Configuration;

namespace DesktopClient
{
	public class OfflineDialogInterface
	{
		public delegate void OnSubmitCallback(ProcessData ProcessData);

		public OnSubmitCallback OnSubmit { get; set; }

		public ProcessData ProcessData { get; set; }

		public void Submit(string mainCategory, string subCategory, DateTime startTime, DateTime endTime)
		{
			ProcessData = new GenericProcessData()
				{
					Name = mainCategory,
					//TitleList = new List<string>() { subCategory },
					StartTime = startTime,
					EndTime = endTime
				};

			Debug.Assert(OnSubmit != null, "OnSumbit should be assigned a value beforehand");
			OnSubmit(ProcessData);
			// This needs to be queued to the producer thread
			//throw new NotImplementedException();
		}

		public List<string> GetButtonText()
		{
			List<string> buttonsText = new List<string>();

			// TODOL can we use collection objects to get rid of these keys. meta config for these keys??
			string[] keys = { "button1", "button2", "button3", "button4", "button5", "button6" };

			foreach (string key in keys)
			{
				buttonsText.Add(ConfigurationManager.AppSettings[key]);
			}

			return buttonsText;
		}
	}
}
