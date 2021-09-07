using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SosigScript.Resources;

namespace SosigScript.Libraries
{
	public class LibraryLoader : IResourceLoader<SosigScriptLibrary>
	{
		public IDictionary<ResourceMetadata, SosigScriptLibrary> LoadedResources => _loadedResources;
		public int LoadedResourceCount { get; }

		private Dictionary<ResourceMetadata, SosigScriptLibrary> _loadedResources;
		private List<Assembly> _loadedAssemblies;

		public LibraryLoader()
		{
			_loadedAssemblies = new List<Assembly>();
			_loadedResources = new Dictionary<ResourceMetadata, SosigScriptLibrary>();
		}

		internal static void AddLibrary(SosigScriptLibrary library)
		{
			Plugin.LibraryLoader._loadedAssemblies.Add(library.Assembly);
			Plugin.LibraryLoader._loadedResources.Add(library.LibraryInfo, library);

			library.LibraryLoader = Plugin.LibraryLoader;
		}

		public void LoadResource(SosigScriptLibrary resource)
		{
			_loadedAssemblies.Add(resource.Assembly);
			_loadedResources.Add(resource.LibraryInfo, resource);
		}

		public SosigScriptLibrary this[string id] => _loadedResources.Values.Where(lib => lib.LibraryInfo.GUID == id).FirstOrDefault();
	}
}
