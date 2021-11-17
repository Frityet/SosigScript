namespace SosigScript
{
    public class LibraryLoader : ResourceLoader<Library>
    {
        public LibraryLoader() : base() {}

        public void LoadLibrary(Library lib)
        {
            LoadedResources.Add(lib);
        }
    }
}
