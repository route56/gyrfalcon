using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStore.Contract;
using System.IO;

namespace BigTableDataStore
{
	public class WriteStore : IWriteStore
	{
		private string _storeFile;

		public WriteStore()
		{
			_storeFile = RootFolder.GetStoreFile();
		}

		public void AddToAggregatedStore(IEnumerable<DataStore.DataAtom> dataList)
		{
			lock (_storeFile)
			{
				using (StreamWriter sw = File.AppendText(_storeFile))
				{
					foreach (var item in dataList)
					{
						sw.WriteLine(item.ToString());
					}
				}
			}
		}

		public void Dispose()
		{
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
					_storeFile = value; 
				}
			} 
		}
	}
}
