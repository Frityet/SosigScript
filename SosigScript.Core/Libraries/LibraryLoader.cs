using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SosigScript.Common;
using SosigScript.Resources;

namespace SosigScript.Libraries
{
	public class LibraryLoader : IResourceLoader<SosigScriptLibrary>
	{
		public IDictionary<ResourceMetadata, SosigScriptLibrary> LoadedResources => _loadedResources;
		public int LoadedResourceCount { get; }

		private readonly Dictionary<ResourceMetadata, SosigScriptLibrary> _loadedResources;
		private readonly List<Assembly> _loadedAssemblies;

		public LibraryLoader()
		{
			_loadedAssemblies = new List<Assembly>();
			_loadedResources = new Dictionary<ResourceMetadata, SosigScriptLibrary>();
		}

		internal static void AddLibrary(SosigScriptLibrary library)
		{
			if (Plugin.LibraryLoader._loadedResources.Keys.GUIDExists(library.LibraryInfo.GUID))
			{
				throw new Exceptions.ResourceAlreadyLoadedException($"Library {library.LibraryInfo.GUID} is already loaded");
			}

			Plugin.LibraryLoader._loadedAssemblies.Add(library.Assembly);
			Plugin.LibraryLoader._loadedResources.Add(library.LibraryInfo, library);

			library.LibraryLoader = Plugin.LibraryLoader;
		}

		public void LoadResource(SosigScriptLibrary resource) => AddLibrary(resource);

		public SosigScriptLibrary this[string id]
		{
			get
			{
				foreach (SosigScriptLibrary library in _loadedResources.Values.Where(library => library.LibraryInfo.GUID == id))
				{
					return library;
				}

				throw new KeyNotFoundException($"Could not find library {id}");
			}
		}
	}
}
