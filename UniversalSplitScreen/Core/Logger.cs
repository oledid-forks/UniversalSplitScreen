﻿using System;
using System.IO;

namespace UniversalSplitScreen.Core
{
	public static class Logger
	{
		private static readonly string filePath;
		private static readonly StreamWriter stream;

		static Logger()
		{
			filePath = Path.Combine(Path.GetDirectoryName(
						System.Reflection.Assembly.GetExecutingAssembly().Location),
						"log.txt");

			stream = new StreamWriter(path: filePath, append: false)
			{
				AutoFlush = true
			};
		}

		public static void WriteLine(string msg)
		{
			Console.WriteLine(msg);
			stream.Write(msg + "\r\n");
		}

		public static void WriteLine(object obj) => WriteLine(obj.ToString());
	}
}