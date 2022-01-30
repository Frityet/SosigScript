using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SosigScript
{
    public class ResourceLoader<TResource> where TResource : Resource
    {
        public List<TResource> LoadedResources { get; private set; }
        public static ResourceLoader<TResource> Instance => s_instance ??= new ResourceLoader<TResource>();
        private static ResourceLoader<TResource>? s_instance;

        public ResourceLoader()
        {
            LoadedResources = new List<TResource>();
        }
    }
}
