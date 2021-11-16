using System.IO;

namespace SosigScript.Resources
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
		///     Description of the resource, can be null
		/// </summary>
		public string? Description { get; set; }

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
		///     File where this resource originates fro
		/// </summary>
		internal FileInfo File { get; set; }
	}
}
