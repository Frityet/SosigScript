using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SosigScript.Resources;

namespace SosigScript.Common
{
	public static class Extensions
	{
		public static IEnumerable<DirectoryInfo> ToDirectories(this IEnumerable<string> dirs) =>
			(from dir in dirs where Directory.Exists(dir) select new DirectoryInfo(dir));

		public static bool GUIDExists(this IEnumerable<ResourceMetadata> database, string guid) =>
			database.Any(metadata => metadata.GUID == guid);

	}

	public static class Delegates
	{
		public delegate void UnityEventDelegate();
	}

	public static class Exceptions
	{
		public class ResourceAlreadyLoadedException : Exception
		{
			public ResourceAlreadyLoadedException(string message) : base(message) { }
		}
	}
}
