using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BigTableDataStore
{
	static class RootFolder
	{
		private const string Folder = "GyrfalconBigTableData";
		private const string StoreFile = "BigTable.txt";

		public static string GetStoreFile()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
					Folder, StoreFile);
		}
	}
}
