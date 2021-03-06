﻿using DesktopClient.ProcessMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DesktopClient.DataSender;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Concurrent;

namespace DesktopClient.Test.ProcessMonitor
{
	//[TestClass()]
	public class ClientQueueProducerConsumerTest
	{
		class StopThread
		{
			public bool Value;
		}

		class ProcessDataMock : ProcessData
		{
			public int TempData { get; set; }

			public override bool Equals(object obj)
			{
				ProcessDataMock pd = obj as ProcessDataMock;
				return pd != null && this.TempData == pd.TempData;
			}

			public override List<IProcessData> GetSubProcessData()
			{
				throw new NotImplementedException();
			}
		}


		/// <summary>
		///A test for ClientQueueProducer Constructor
		///</summary>
		//[TestMethod()]
		public void ClientQueueProducerConstructorTestBasic()
		{
			ClientQueueProducerConstructorTest(new Random());
		}

		/// <summary>
		///A test for ClientQueueProducer Constructor
		///</summary>
		//[TestMethod()]
		public void ClientQueueProducerConstructorTestStress()
		{
			for (int i = 0; i < 100; i++)
			{
				ClientQueueProducerConstructorTest(new Random()); 
			}
		}

		public void ClientQueueProducerConstructorTest(Random rand)
		{
			ConcurrentQueue<ProcessData> queue = new ConcurrentQueue<ProcessData>();

			TimeSpan producerThread = new TimeSpan(0, 0, 0, 0, rand.Next() % 10);
			TimeSpan consumerThread = new TimeSpan(0, 0, 0, 0, rand.Next() % 100);
			TimeSpan testThread = TimeSpan.FromTicks(10 * (producerThread + consumerThread + new TimeSpan(0, 0, 0, 0, rand.Next() % 100)).Ticks);

			TimeSpan oneTick = new TimeSpan(1);

			producerThread += oneTick;
			consumerThread += oneTick;
			testThread += oneTick;

			//ConcurrentQueue
			ClientQueueProducer producer = new ClientQueueProducer(queue);
			ClientQueueConsumer consumer = new ClientQueueConsumer(queue);

			StopThread requestProducerToStop = new StopThread() { Value = false };
			StopThread requestConsumerToStop = new StopThread() { Value = false };

			int prodCount = 0;
			List<ProcessData> actual = null;

			Thread prodThread = new Thread(() => { prodCount = ProducerThread(producer, requestProducerToStop, producerThread); });
			Thread consThread = new Thread(() => { actual = ConsumerThread(consumer, requestConsumerToStop, consumerThread); });

			prodThread.Start();
			consThread.Start();

			// Let threads work
			Thread.Sleep(testThread);
			
			requestProducerToStop.Value = true;
			prodThread.Join();

			requestConsumerToStop.Value = true;
			consThread.Join();

			// Verification stuff
			List<ProcessData> expected = new List<ProcessData>();
			for (int i = 0; i < prodCount; i++)
			{
				expected.Add(new ProcessDataMock() { TempData = i });
			}

			CollectionAssert.AreEqual(expected, actual, "consumer should be able to consume all producerThread.Ticks = {0}, consumerThread.Ticks = {1},  testThread.Ticks {2}", producerThread.Ticks, consumerThread.Ticks, testThread.Ticks);
		}

		private int ProducerThread(ClientQueueProducer producer, StopThread requestProducerToStop, TimeSpan producerThread)
		{
			int i = 0;

			while(requestProducerToStop.Value == false)
			{
				producer.Add(new ProcessDataMock() { TempData = i });
				Thread.Sleep(producerThread);
				i++;
			}

			return i;
		}

		private List<ProcessData> ConsumerThread(ClientQueueConsumer consumer, StopThread requestConsumerToStop, TimeSpan consumerThread)
		{
			List<ProcessData> list = new List<ProcessData>();

			while (requestConsumerToStop.Value == false || consumer.CanConsumeMore())
			{
				list.AddRange(consumer.Consume());
				Thread.Sleep(consumerThread);
			}

			return list;
		}
	}
}
