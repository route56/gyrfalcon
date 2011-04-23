using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProcessMonitor;
using System.Diagnostics;

namespace DesktopClient
{
	public class OfflineDialogInterface
	{
		public delegate void OnSubmitCallback(ProcessData ProcessData);

		public OnSubmitCallback OnSubmit { get; set; }

		public ProcessData ProcessData { get; set; }

		public void Submit(string mainCategory, string subCategory, DateTime startTime, DateTime endTime)
		{
			ProcessData = new ProcessData()
				{
					Name = mainCategory,
					TitleList = new List<string>() { subCategory },
					StartTime = startTime,
					EndTime = endTime
				};

			Debug.Assert(OnSubmit != null, "OnSumbit should be assigned a value beforehand");
			OnSubmit(ProcessData);
			// This needs to be queued to the producer thread
			//throw new NotImplementedException();
		}




	}
}
