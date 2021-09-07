using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoonSharp.Interpreter;
using SosigScript.Common;
using SosigScript.Resources;

namespace SosigScript
{
	public class ScriptLoader : IResourceLoader<SosigScript>
	{
		public IDictionary<ResourceMetadata, SosigScript> LoadedResources => _loadedResources;
		public int LoadedResourceCount => _loadedResources.Count;

		private Dictionary<ResourceMetadata, SosigScript> _loadedResources;

		public ScriptLoader(string searchDir, string ext)
		{
			if (!Directory.Exists(searchDir))
			{
				throw new DirectoryNotFoundException($"Directory {searchDir} does not exist");
			}

			var raw = new List<FileInfo>();

			foreach (DirectoryInfo dir in Directory.GetDirectories(searchDir).ToDirectories())
			{
				raw.AddRange(dir.GetFiles().Where(file => file.Extension == ext));
			}

			_loadedResources = new Dictionary<ResourceMetadata, SosigScript>();

			List<SosigScript> scripts = raw.Select(file => new SosigScript(file)).ToList();

			foreach (SosigScript script in scripts)
			{
				_loadedResources.Add(script.ScriptMetadata, script);
			}

			Plugin.OnAwake += () => Execute("Awake");
			Plugin.OnStart += () => Execute("Start");
			Plugin.OnUpdate += () => Execute("Update");
			Plugin.OnFixedUpdate += () => Execute("FixedUpdate");
		}

		public void LoadResource(SosigScript script) => _loadedResources.Add(script.ScriptMetadata, script);

		public void LoadResource(string path)
		{
			var script = new SosigScript(path);
			_loadedResources.Add(script.ScriptMetadata, script);
		}

		public SosigScript this[string index]
		{
			get
			{
				foreach (SosigScript script in _loadedResources.Where(script => script.Key.File.Name == index).Select(pair => pair.Value))
				{
					return script;
				}

				throw new KeyNotFoundException($"Script {index} is not loaded");
			}
		}

		public IEnumerable<DynValue> Execute(string functionName) => _loadedResources.Select(script => script.Value.Execute(functionName));
		public IEnumerable<DynValue> Execute(string functionName, params object[] args) => _loadedResources.Select(script => script.Value.Execute(functionName, args));

	}
}
