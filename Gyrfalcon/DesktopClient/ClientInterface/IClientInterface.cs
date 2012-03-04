using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DesktopClient.ClientInterface
{
	public interface IClientInterface : 
		IProxy, IGetFocused, IDashboard, ISettings, ISnooze, 
		IAlert, IOfflineTask, IStatus
	{
	}

	public interface ISettings
	{
		void LaunchSettingEditor();

		string[] OfflineTaskCategory { get; }
	}

	public interface IStatus
	{
		bool IsEverythingOk { get; }
		DateTime LastSuccessfulTransmission { get; }
		string ClientVersion { get; }
		string VersionHistoryURL { get; }
	}

	public interface IAlert
	{
		/// <summary>
		/// Alert message
		/// </summary>
		event Action<string> AlertMessenger;

		/// <summary>
		/// Notify that system is idle since
		/// </summary>
		event Action<DateTime> AlertSystemIsIdle;

		/// <summary>
		/// Notify that system is not idle
		/// </summary>
		event Action<DateTime> AlertSystemIsBusy;
	}

	public interface IOfflineTask
	{
		string OfflineCategoryEditUrl { get; }

		void SubmitOfflineTaskDetails(DateTime startTime, DateTime endTime, string category, string details);
	}

	public interface ISnooze
	{
		bool IsSnoozed { get; set; }

		TimeSpan Duration { get; set; }

		event Action OnSnoozeCompletion;
	}

	public interface IProxy
	{
		string Server { get; set; }
		int Port { get; set; }
		string Username { get; set; }
		string Password { get; set; }
	}

	public interface IDashboard
	{
		void LaunchDashboard();
	}

	public interface IGetFocused
	{
		void GetFocused(DateTime startTime, DateTime endTime);

		event Action OnFocusTimeCompletion;
	}
}
