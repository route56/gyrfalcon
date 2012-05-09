using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Common.TimeWindow;

namespace BigTableCompressor
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 3)
			{
				PrintUsage();
				return;
			}

			string sourceFile = args[0];
			string targetFile = args[1];
			string command = args[2];

			if (File.Exists(sourceFile) == false)
			{
				throw new FileNotFoundException("Missing file", sourceFile);
			}

			BigTableDataStore.Compressor compressor = new BigTableDataStore.Compressor();

			switch (command.ToLower())
			{
				case "hour":
					compressor.CompressFile(sourceFile, targetFile, s => s.Date.AddHours(s.Hour));
					break;
				case "day":
					compressor.CompressFile(sourceFile, targetFile, s => s.Date);
					break;
				case "week":
					compressor.CompressFile(sourceFile, targetFile, s => new DayTimeWindow(s).ToWeekWindow().StartTime);
					break;
				case "month":
					compressor.CompressFile(sourceFile, targetFile, s => new DateTime(s.Year, s.Month, 1));
					break;
				case "year":
					compressor.CompressFile(sourceFile, targetFile, s => new DateTime(s.Year, 1, 1));
					break;
				default:
					PrintUsage();
					return;
			}
		}

		private static void PrintUsage()
		{
			Console.WriteLine("Usage:");
			Console.WriteLine("BigTableCompressor.exe sourceFile.txt compressedFile.txt <byTime>");
			Console.WriteLine("Where byTime can be: hour, day, week, month, year");
		}
	}
}
