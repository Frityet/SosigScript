using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoonSharp.Interpreter;
using SosigScript.Common;
using SosigScript.Resources;

namespace SosigScript
{
	/// <summary>
	///     Loads SosigScripts
	/// </summary>
	public class ScriptLoader : IResourceLoader<SosigScript>
	{
		/// <summary>
		///     Creates a new ScriptLoader
		/// </summary>
		/// <param name="searchDir">Directory to search scripts in</param>
		/// <param name="ext">Extension of the files to select</param>
		/// <exception cref="DirectoryNotFoundException">Search directory does not exist</exception>
		public ScriptLoader(string searchDir, string ext)
		{
			if (!Directory.Exists(searchDir))
				throw new DirectoryNotFoundException($"Directory {searchDir} does not exist");

			var raw = new List<FileInfo>();

			foreach (DirectoryInfo dir in Directory.GetDirectories(searchDir).ToDirectories())
				raw.AddRange(dir.GetFiles().Where(file => file.Extension == ext));


			List<SosigScript> scripts = raw.Select(file => new SosigScript(file)).ToList();

			LoadedResources = new Dictionary<ResourceMetadata, SosigScript>(scripts.Count);

			foreach (SosigScript script in scripts) LoadedResources.Add(script.ScriptMetadata, script);

			Plugin.OnAwake += () => Execute("Awake");
			Plugin.OnStart += () => Execute("Start");
			Plugin.OnUpdate += () => Execute("Update");
			Plugin.OnFixedUpdate += () => Execute("FixedUpdate");
		}

		/// <summary>
		///     Collection of all of the loaded SosigScripts and their metadata
		/// </summary>
		public Dictionary<ResourceMetadata, SosigScript> LoadedResources { get; }

		/// <summary>
		///     Loads a SosigScript into the ScriptLoader
		/// </summary>
		/// <exception cref="Exceptions.ResourceAlreadyLoadedException">
		///     Script already loaded into the current instance of Script
		///     Loader
		/// </exception>
		/// <param name="script">Script to load</param>
		public void LoadResource(SosigScript script)
		{
			if (LoadedResources.Keys.GUIDExists(script.ScriptMetadata.GUID))
				throw new Exceptions.ResourceAlreadyLoadedException(
					$"SosigScript {script.ScriptMetadata.GUID} is already loaded");
			LoadedResources.Add(script.ScriptMetadata, script);
		}

		/// <summary>
		///     Gets a SosigScript
		/// </summary>
		/// <param name="index">GUID of the script to find</param>
		/// <exception cref="KeyNotFoundException">Script GUID not found</exception>
		public SosigScript this[string index]
		{
			get
			{
				foreach (SosigScript script in LoadedResources.Where(script => script.Key.GUID == index)
															  .Select(pair => pair.Value)) return script;

				throw new KeyNotFoundException($"Script {index} is not loaded");
			}
		}

		/// <summary>
		///     Loads a SosigScript file into the ScriptLoader
		/// </summary>
		/// <param name="path">Path of the script to load</param>
		/// <exception cref="Exceptions.ResourceAlreadyLoadedException">
		///     Script already loaded into the current instance of the
		///     Script
		/// </exception>
		public void LoadResource(string path)
		{
			var script = new SosigScript(path);

			if (LoadedResources.Keys.GUIDExists(script.ScriptMetadata.GUID))
				throw new Exceptions.ResourceAlreadyLoadedException(
					$"SosigScript {script.ScriptMetadata.GUID} is already loaded");

			LoadedResources.Add(script.ScriptMetadata, script);
		}

		/// <summary>
		///     Adds a global object to all loaded scripts
		/// </summary>
		/// <param name="name">Name of the object to add</param>
		/// <param name="obj">Value of the object</param>
		public void AddObject(string name, object obj)
		{
			foreach (SosigScript script in LoadedResources.Values) script.AddGlobal(name, obj);
		}

		/// <summary>
		///     Adds a global object to all loaded scripts
		/// </summary>
		/// <param name="name">Name of the object to add</param>
		/// <param name="obj">Value of the object</param>
		/// <typeparam name="T">Type of the object</typeparam>
		public void AddObject<T>(string name, T obj)
		{
			foreach (SosigScript script in LoadedResources.Values) script.AddGlobal(name, obj);
		}

		/// <summary>
		///     Executes a function in each loaded scripts
		/// </summary>
		/// <param name="functionName">Name of the function to execute</param>
		/// <returns>Collection of all of the return values</returns>
		public IEnumerable<DynValue> Execute(string functionName) =>
			LoadedResources.Select(script => script.Value.Execute(functionName));

		/// <summary>
		///     Executes a function in each of the loaded scripts
		/// </summary>
		/// <param name="functionName">Name of the function to execute</param>
		/// <param name="args">Arguments to pass to the function</param>
		/// <returns>Collection of all of the return values</returns>
		public IEnumerable<DynValue> Execute(string functionName, params object[] args) =>
			LoadedResources.Select(script => script.Value.Execute(functionName, args));
	}
}
