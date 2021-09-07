using System.Collections.Generic;
using System.Reflection;
using SosigScript.Resources;

namespace SosigScript.Libraries
{
	public class SosigScriptLibrary
	{
		public ResourceMetadata LibraryInfo { get; }
		public IDictionary<string, object> LoadedObjects => _loadedObjects;
		public Assembly Assembly { get; }

		internal LibraryLoader? LibraryLoader { get; set; }

		private Dictionary<string, object> _loadedObjects;

		public SosigScriptLibrary(ResourceMetadata libraryInfo)
		{
			LibraryInfo = libraryInfo;
			_loadedObjects = new Dictionary<string, object>();
			Assembly = Assembly.GetCallingAssembly();

			LibraryLoader.AddLibrary(this);
		}

		public void RegisterObject(string name, object obj) => _loadedObjects.Add(name, obj);
	}
}
