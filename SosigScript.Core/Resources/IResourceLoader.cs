using System.Collections.Generic;

namespace SosigScript.Resources
{
	/// <summary>
	///     Interface for a resource loader
	/// </summary>
	/// <typeparam name="T">Type of the resource to load</typeparam>
	public interface IResourceLoader<T>
	{
		/// <summary>
		///     Loaded resources and their metadata
		/// </summary>
		public Dictionary<ResourceMetadata, T> LoadedResources { get; }

		/// <summary>
		///     Gets a resource
		/// </summary>
		/// <param name="id">GUID of the resource to get</param>
		public T this[string id] { get; }

		/// <summary>
		///     Loads a resource
		/// </summary>
		/// <param name="resource">Resource to load</param>
		public void LoadResource(T resource);
	}
}
