using System.IO;
using MoonSharp.Interpreter;

namespace SosigScript
{
	public class SosigScript
	{
		public string Name				{ get; }
		public string Path				{ get; }

		public ScriptOptions Options => _script.Options;

		private Script _script;

		public SosigScript(FileSystemInfo file) : this(file.FullName) {}

		public SosigScript(string path)
		{
			if (!File.Exists(path))
				throw new FileNotFoundException($"Could not find file {path}");
			var file = new FileInfo(path);
			Name = file.Name;
			Path = file.FullName;

			_script = new Script();
			_script.LoadFile(Path, null, Name);
		}

		public DynValue Execute(string funcName) => _script.Call(funcName);
		public DynValue Execute(string funcName, params object[] args) => _script.Call(funcName, args);
	}
}
