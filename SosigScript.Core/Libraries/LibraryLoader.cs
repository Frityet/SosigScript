using System;
using System.Collections.Generic;
using System.Reflection;

namespace SosigScript.Core.Libraries
{
	public class LibraryLoader : IResourceLoader<SosigScriptLibraryAttribute>
	{
		public IDictionary<ResourceMetadata, SosigScriptLibraryAttribute> LoadedResources { get; }
		public int LoadedResourceCount { get; }

		public void LoadResource(SosigScriptLibraryAttribute resource)
		{

		}

		public void LoadResource(string path)
		{

		}

		public SosigScriptLibraryAttribute this[string id]
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
