using System.Collections.Generic;
using System.Linq;

namespace SosigScript
{
    public class LibraryLoader : ResourceLoader<Library>
    {
        internal static void RegisterLibrary(Library lib)
        {
            Instance.LoadedResources.Add(lib);
        }

        public void  LoadAllLibraries()
        {
            foreach (SosigScript script in ScriptLoader.Instance.LoadedResources)
                foreach (KeyValuePair<string, object> entry in LoadedResources.SelectMany(lib => lib.RegisteredObjects))
                    script.AddGlobal(entry.Key, entry.Value);
        }
    }
}
