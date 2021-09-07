using System.IO;
using System.Linq;
using MoonSharp.Interpreter;
using SosigScript.Resources;

namespace SosigScript
{
	public class SosigScript
	{
		public ScriptOptions Options => _script.Options;

		private readonly Script _script;
		private readonly FileInfo _file;

		public ResourceMetadata ScriptMetadata
		{
			get
			{
				Table metadata = _script.Globals.Get("ScriptInfo").Table;

				return new ResourceMetadata
				{
					Name		= metadata["Name"].ToString(),
					GUID		= metadata["GUID"].ToString(),
					Author		= metadata["Author"].ToString(),
					Description = metadata["Description"].ToString(),
					Version		= metadata["Version"].ToString(),
					File		= _file
				};
			}
		}

		public SosigScript(FileSystemInfo file) : this(file.FullName) {}

		public SosigScript(string path)
		{
			if (!File.Exists(path))
				throw new FileNotFoundException($"Could not find file {path}");
			var file = new FileInfo(path);

			_script = new Script();
			_script.LoadFile(file.FullName, null, file.Name);
			_file = file;

		}

		public void AddGlobal(string name, object obj) => _script.Globals[name] = obj;


		public DynValue Execute(string funcName) => _script.Call(funcName);
		public DynValue Execute(string funcName, params object[] args) => _script.Call(funcName, args);

		public bool IsDefined(string global) => _script.Globals.Keys.Any(key => key.String == global);


	}
}
