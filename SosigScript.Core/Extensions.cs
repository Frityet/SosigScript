using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SosigScript.Common
{
	public static class Extensions
	{
		public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
		{
			foreach (T item in collection)
			{
				action(item);
			}
		}

		public static IEnumerable<DirectoryInfo> ToDirectories(this IEnumerable<string> dirs) =>
			(from dir in dirs where Directory.Exists(dir) select new DirectoryInfo(dir));

	}
}
