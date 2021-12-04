using System.Collections.Generic;
namespace SosigScript
{
    public class LibraryLoader : ResourceLoader<Library>
    {
        public LibraryLoader() : base() {}

        internal static void RegisterLibrary(Library lib)
        {
            Instance.LoadedResources.Add(lib);
        }

        public void LoadAllLibraries()
        {
            foreach (SosigScript script in ScriptLoader.Instance.LoadedResources)
                foreach (Library lib in LoadedResources)
                    foreach (KeyValuePair<string, object> entry in lib.RegisteredObjects)
                        script.AddGlobal(entry.Key, entry.Value);
        }
    }
}
