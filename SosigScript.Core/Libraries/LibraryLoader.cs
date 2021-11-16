using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SosigScript.Common;
using SosigScript.Resources;

namespace SosigScript.Libraries
{
	/// <summary>
	///     Loads libraries for use with SosigScripts
	///     <remarks>
	///         There should only be only one LibraryLoader active so <see cref="AddLibrary" /> functions correctly,
	///         as it is static
	///     </remarks>
	/// </summary>
	public class LibraryLoader : IResourceLoader<SosigScriptLibrary>
	{
		private readonly List<Assembly> _loadedAssemblies;

		/// <summary>
		///     Creates a new Library Loader
		/// </summary>
		public LibraryLoader()
		{
			_loadedAssemblies = new List<Assembly>();
			LoadedResources = new Dictionary<ResourceMetadata, SosigScriptLibrary>();
		}

		/// <summary>
		///     Collection of all loaded libraries and their metadata
		/// </summary>
		public Dictionary<ResourceMetadata, SosigScriptLibrary> LoadedResources { get; }

		/// <summary>
		///     Adds a library to the registry
		/// </summary>
		/// <param name="resource">Library to add</param>
		public void LoadResource(SosigScriptLibrary resource) => AddLibrary(resource);

		/// <summary>
		///     Gets a SosigScriptLibrary
		/// </summary>
		/// <param name="id">GUID of the library</param>
		/// <exception cref="KeyNotFoundException">Library with the GUID is not loaded or does not exist</exception>
		public SosigScriptLibrary this[string id]
		{
			get
			{
				foreach (SosigScriptLibrary library in LoadedResources.Values.Where(
					library => library.LibraryInfo.GUID == id)) return library;

				throw new KeyNotFoundException($"Could not find library {id}");
			}
		}

		/// <summary>
		///     Adds a library to the registry
		/// </summary>
		/// <param name="library">Library to add</param>
		/// <exception cref="Exceptions.ResourceAlreadyLoadedException">Library already is loaded</exception>
		internal static void AddLibrary(SosigScriptLibrary library)
		{
			if (Plugin.LibraryLoader!.LoadedResources.Keys.GUIDExists(library.LibraryInfo.GUID))
				throw new Exceptions.ResourceAlreadyLoadedException(
					$"Library {library.LibraryInfo.GUID} is already loaded");

			Plugin.LibraryLoader._loadedAssemblies.Add(library.Assembly);
			Plugin.LibraryLoader.LoadedResources.Add(library.LibraryInfo, library);

			library.LibraryLoader = Plugin.LibraryLoader;
		}
	}
}
