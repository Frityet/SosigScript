using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SosigScript.Common
{
	public static class Extensions
	{
		public static IEnumerable<DirectoryInfo> ToDirectories(this IEnumerable<string> dirs) =>
			(from dir in dirs where Directory.Exists(dir) select new DirectoryInfo(dir));


	}
}
