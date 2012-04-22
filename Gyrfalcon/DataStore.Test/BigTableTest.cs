using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStore.Contract;
using System.IO;

namespace DataStore.Test
{
	[TestClass()]
	public class BigTableTest : QueryStoreTest
	{
		string _tempFileStorepath;
		public override IWriteStore Writer { get; set; }
		public override IQueryStore Reader { get; set; }

		//Use TestInitialize to run code before running each test
		[TestInitialize()]
		public override void MyTestInitialize()
		{
			_tempFileStorepath = Path.GetRandomFileName();

			Writer = new BigTableDataStore.WriteStore() { StoreFile = _tempFileStorepath };
			Reader = new BigTableDataStore.QueryStore() { StoreFile = _tempFileStorepath };
		}

		// Use TestCleanup to run code after each test has run
		[TestCleanup()]
		public override void MyTestCleanup()
		{
			File.Delete(_tempFileStorepath);
			_tempFileStorepath = string.Empty;
			Writer = null;
			Reader = null;
		}
	}
}
