using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStore.Contract;
using DataStore;
using System.Timers;
using System.IO;

namespace BigTableDataStore
{
	public class QueryStore : IQueryStore
	{
		private string _storeFile = string.Empty;
		private List<DataAtom> _inMemoryList = new List<DataAtom>();
		private FileSystemWatcher _watcher = new FileSystemWatcher();

		public QueryStore()
		{
			StoreFile = RootFolder.GetStoreFile();
		}

		public IEnumerable<DataStore.GroupedDataFormat> GetGroupedData(DateTime startTime, DateTime endTime)
		{
			return new DataFormatConvertor(GetData(startTime, endTime)).ToGroupedDataFormat();
		}

		public IEnumerable<DataStore.RankedDataFormat> GetRankedData(DateTime startTime, DateTime endTime)
		{
			return new DataFormatConvertor(GetData(startTime, endTime)).ToRankedDataFormat();
		}

		public string StoreFile 
		{ 
			get 
			{ 
				return _storeFile; 
			} 
			set 
			{
				lock (_storeFile)
				{
					_storeFile = Path.GetFullPath(value);

					if (File.Exists(_storeFile))
					{
						LoadDataFromFile();
					}
					else
					{
						Directory.CreateDirectory(Path.GetDirectoryName(_storeFile));
						File.Create(_storeFile).Dispose();
					}

					_watcher.Path = Path.GetDirectoryName(_storeFile);
					_watcher.NotifyFilter = NotifyFilters.LastWrite;
					_watcher.Filter = Path.GetFileName(_storeFile);
					_watcher.Changed += new FileSystemEventHandler(_watcher_Changed);
					_watcher.EnableRaisingEvents = true;
				}
			}
		}

		void _watcher_Changed(object sender, FileSystemEventArgs e)
		{
			LoadDataFromFile();
		}

		private IEnumerable<DataAtom> GetData(DateTime start, DateTime end)
		{
			lock (_inMemoryList)
			{
				return _inMemoryList.Where(data => data.Time >= start && data.Time <= end); 
			}
		}

		private void LoadDataFromFile()
		{
			//int offset = _inMemoryList.Count;
			// This should be optimized
			// Create few performance load tests, real life type event. 1/4/8hr tests
			lock (_storeFile)
			{
				List<DataAtom> allFile = new List<DataAtom>();

				if (File.Exists(_storeFile))
				{
					using (StreamReader sr = File.OpenText(_storeFile))
					{
						string line;
						while ((line = sr.ReadLine()) != null)
						{
							allFile.Add(DataAtom.FromString(line));
						}
					} 
				}

				lock (_inMemoryList)
				{
					_inMemoryList.Clear();
					_inMemoryList = allFile;
				}
			}
		}
	}
}
