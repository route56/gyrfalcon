using ProcessMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataSender;
using System.Threading;
using System.Collections.Generic;
using System.Collections;

namespace TestSystemTrayInterface
{
    
    
    /// <summary>
    ///This is a test class for ClientQueueProducerTest and is intended
    ///to contain all ClientQueueProducerTest Unit Tests
    ///</summary>
	[TestClass()]
	public class ClientQueueProducerConsumerTest
	{


		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


		/// <summary>
		///A test for ClientQueueProducer Constructor
		///</summary>
		[TestMethod()]
		public void ClientQueueProducerConstructorTest()
		{
			Queue<ProcessData> queue = new Queue<ProcessData>();

			//ConcurrentQueue
			ClientQueueProducer producer = new ClientQueueProducer(queue);
			ClientQueueConsumer consumer = new ClientQueueConsumer(queue);

			List<ProcessData> actual = null;
			Thread prodThread = new Thread(() => { ProducerThread(producer); });
			Thread consThread = new Thread(() => { actual = ConsumerThread(consumer); });

			prodThread.Start();
			consThread.Start();

			prodThread.Join();
			consThread.Join();

			// Verification stuff
			List<ProcessData> expected = new List<ProcessData>();
			for (int i = 0; i < 100; i++)
			{
				expected.Add(new ProcessData() { TempData = i });
			}

			CollectionAssert.AreEqual(expected, actual, "consumer should be able to consume all");
		}

		private void ProducerThread(ClientQueueProducer producer)
		{
			for (int i = 0; i < 100; i++)
			{
				producer.Add(new ProcessData() { TempData = i });
				Thread.Sleep(10);
			}
		}

		private List<ProcessData> ConsumerThread(ClientQueueConsumer consumer)
		{
			List<ProcessData> list = new List<ProcessData>();
			while (consumer.QueueCount > 0) // this is flawed test case! This can bail out before queue was filled
			{
				list.AddRange(consumer.Consume());
				Thread.Sleep(100);
			}

			return list;
		}
	}
}
