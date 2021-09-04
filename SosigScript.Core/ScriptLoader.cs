using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoonSharp.Interpreter;
using SosigScript.Common;

namespace SosigScript
{
	public class ScriptLoader
	{
		public IEnumerable<SosigScript> LoadedScripts => _loadedScripts;
		public int LoadedScriptCount => _loadedScripts.Count;

		private List<SosigScript> _loadedScripts;

		public ScriptLoader(string searchDir, string ext)
		{
			if (!Directory.Exists(searchDir))
			{
				throw new DirectoryNotFoundException($"Directory {searchDir} does not exist");
			}

			var raw = new List<FileInfo>();

			foreach (DirectoryInfo dir in Directory.GetDirectories(searchDir).ToDirectories())
			{
				IEnumerable<FileInfo> files = dir.GetFiles().Where(file => file.Name.EndsWith($".{ext}"));
				raw.AddRange(files);
			}

			_loadedScripts = raw.Select(file => new SosigScript(file)).ToList();
		}

		public void AddScript(SosigScript script) => _loadedScripts.Add(script);
		public void AddScript(string path) => _loadedScripts.Add(new SosigScript(path));

		public SosigScript this[string index]
		{
			get
			{
				foreach (SosigScript script in _loadedScripts.Where(script => script.Name == index))
				{
					return script;
				}

				throw new KeyNotFoundException($"Script {index} is not loaded");
			}
		}

		public IEnumerable<DynValue> Execute(string functionName) => _loadedScripts.Select(script => script.Execute(functionName));
		public IEnumerable<DynValue> Execute(string functionName, params object[] args) => _loadedScripts.Select(script => script.Execute(functionName, args));

	}
}
