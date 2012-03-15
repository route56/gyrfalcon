using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopClient.ClientInterface
{
	public interface IClientInterface
	{
		IProxy Proxy { get; }
		IGetFocused GetFocused { get; }
		IDashboard Dashboard { get; }
		ISettings Settings { get; }
		ISnooze Snooze { get; }
		IAlert Alert { get; }
		IOfflineTask OfflineTask { get; }
		IStatus Status { get; }

		void Start();
		void Stop();
	}

	public interface ISettings
	{
		string GetSettingEditorUrl();

		string[] OfflineTaskCategory { get; }
	}

	public interface IStatus
	{
		bool IsEverythingOk { get; }
		DateTime LastSuccessfulTransmission { get; }
		string ClientVersion { get; }
		string VersionHistoryURL { get; }

		event Action StatusChanged;
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
		bool IsSnoozed { get; }

		void Sleep(TimeSpan duration);

		void Wakeup();

		/// <summary>
		/// Will be called on successful snooze completion. If Wakeup() is called, this is not triggered.
		/// </summary>
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
		// This should change. It should return URL
		void LaunchDashboard();
	}

	public interface IGetFocused
	{
		void GetFocused(DateTime startTime, DateTime endTime);

		event Action OnFocusTimeCompletion;
	}
}
