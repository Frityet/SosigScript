using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SosigScript.Common
{
	/// <summary>
	///     Extensions to classes
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		///     Turns a string collection into a DirectoryInfo collection
		/// </summary>
		/// <param name="dirs">String Collection of directory paths</param>
		/// <returns>Collection of DirectoryInfos</returns>
		public static IEnumerable<DirectoryInfo> ToDirectories(this IEnumerable<string> dirs) =>
			from dir in dirs where Directory.Exists(dir) select new DirectoryInfo(dir);

	}

	/// <summary>
	///     Common delegates
	/// </summary>
	public static class Delegates
	{
		/// <summary>
		///     Delegate for a Unity event function, returns nothing with no arguments
		/// </summary>
		public delegate void UnityEventDelegate();
	}

	/// <summary>
	///     Exceptions
	/// </summary>
	public static class Exceptions
	{
		/// <summary>
		///     Resource already loaded
		/// </summary>
		public class ResourceAlreadyLoadedException : Exception
		{
			public ResourceAlreadyLoadedException(string message) : base(message)
			{
			}
		}
	}
}
