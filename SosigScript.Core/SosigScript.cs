using System.IO;
using MoonSharp.Interpreter;

namespace SosigScript
{
	public struct Metadata
	{
		public string Name			{ get; set; }
		public string Description	{ get; set; }
		public string Version		{ get; set; }
		public string Author		{ get; set; }

	}

	public class SosigScript
	{
		public Metadata Metadata		{ get; }

		public string FileName			{ get; }
		public string Path				{ get; }

		public ScriptOptions Options => _script.Options;

		private readonly Script _script;

		public SosigScript(FileSystemInfo file) : this(file.FullName) {}

		public SosigScript(string path)
		{
			if (!File.Exists(path))
				throw new FileNotFoundException($"Could not find file {path}");
			var file = new FileInfo(path);
			FileName = file.Name;
			Path = file.FullName;

			_script = new Script();
			_script.LoadFile(Path, null, FileName);

			Table metadata = _script.Globals.Get("ScriptInfo").Table;

			Metadata = new Metadata
			{
				Name = metadata["Name"].ToString(),
				Author = metadata["Author"].ToString(),
				Description = metadata["Description"].ToString(),
				Version = metadata["Version"].ToString()
			};
		}

		public DynValue Execute(string funcName) => _script.Call(funcName);
		public DynValue Execute(string funcName, params object[] args) => _script.Call(funcName, args);
	}
}
