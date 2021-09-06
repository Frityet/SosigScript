using System.Collections;
using System.Collections.Generic;

namespace SosigScript.Core
{
	public interface IResourceLoader<T>
	{
		public IDictionary<ResourceMetadata, T> LoadedResources { get; }
		public int LoadedResourceCount { get; }

		public void LoadResource(T resource);
		public void LoadResource(string path);

		public T this[string id] { get; }
	}
}
