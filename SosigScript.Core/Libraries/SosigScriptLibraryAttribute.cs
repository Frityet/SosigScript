using System;
using System.Reflection;

namespace SosigScript.Core.Libraries
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public class SosigScriptLibraryAttribute : Attribute
	{
		public Assembly Assembly			{ get; }
		public ResourceMetadata LibraryInfo { get; }

		public SosigScriptLibraryAttribute(string name, string author, string version, string description = "")
		{
			Assembly = Assembly.GetExecutingAssembly();

			LibraryInfo = new ResourceMetadata();

			LibraryInfo = new ResourceMetadata
			{
				Name = name,
				Author = author,
				Description = description,
				Version = version,

			};

		}
	}
}
