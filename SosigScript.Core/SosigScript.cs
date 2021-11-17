using System.IO;
using System;
using MoonSharp.Interpreter;

namespace SosigScript
{
    public class SosigScript : Resource
    {
		private Script _executioner;
		public SosigScript(string path)
		{
			var file = new FileInfo(path);
			if (!file.Exists)
				throw new FileNotFoundException($"Could not find script with path of {path}");

			_executioner = new Script();
		    DynValue result = _executioner.LoadFile(path);
			Metadata = new ResourceMetadata((Table)_executioner.Globals["ScriptInfo"], file);
		}

        public DynValue ExecuteFunction(string functionName, params object[] args) => _executioner.Call(functionName, args);
    }
}
