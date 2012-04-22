using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStore.Contract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace DataStore.Test
{
	[TestClass()]
	public class DataStoreTest : QueryStoreTest
	{
		string _tempFileStorepath;
		public override IWriteStore Writer { get; set; }
		public override IQueryStore Reader { get; set; }

		//Use TestInitialize to run code before running each test
		[TestInitialize()]
		public override void MyTestInitialize()
		{
			_tempFileStorepath = Path.GetRandomFileName();
			Directory.CreateDirectory(_tempFileStorepath);
			FilePaths fileProvider = new FilePaths(_tempFileStorepath);

			Writer = new WriteStore() { FilePathProvider = fileProvider };
			Reader = new QueryStore() { FilePathProvider = fileProvider };
		}

		// Use TestCleanup to run code after each test has run
		[TestCleanup()]
		public override void MyTestCleanup()
		{
			Directory.Delete(_tempFileStorepath, true);
			_tempFileStorepath = string.Empty;
			Writer = null;
			Reader = null;
		}
	}
}
