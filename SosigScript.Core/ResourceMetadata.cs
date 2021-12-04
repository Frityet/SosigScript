using System.Collections.Generic;
using System.IO;
using MoonSharp.Interpreter;
using System.Linq;

namespace SosigScript
{
	/// <summary>
	///     Information about a resource
	/// </summary>
	public struct ResourceMetadata
	{
	    /// <summary>
	    ///     Name of the resource
	    /// </summary>
		public string Name { get; set; }

		/// <summary>
	    ///     GUID of the resource, should be
		///     <code>author.name</code>
		/// </summary>
		public string GUID { get; set; }

		/// <summary>
		///     Author of the resource
		/// </summary>
		public string Author { get; set; }

		/// <summary>
		///     Version of the resource, should be
		///     <code>major.minor.build</code>
		/// </summary>
		public string Version { get; set; }

		/// <summary>
		///     Dependencies for this resource, can be null
		/// </summary>
		public string[]? Dependencies { get; set; }

		/// <summary>
		///     File where this resource originates from
		/// </summary>
		internal FileInfo File { get; set; }

		public ResourceMetadata(Table table, FileInfo file)
		{
			Name            = (string) table["name"];
			GUID            = (string) table["guid"];
			Author          = (string) table["author"];
			Version         = (string) table["version"];
			Dependencies    = (string[]) table["dependencies"];
			File            = file;
		}

		public ResourceMetadata(string name, string guid, string author, string version, FileInfo file, params string[]? dependencies)
		{
			Name = name;
			GUID = guid;
			Author = author;
			Version = version;
			File = file;
			Dependencies = dependencies;
		}
	}
}
