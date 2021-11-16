using System.IO;
using System.Linq;
using MoonSharp.Interpreter;
using SosigScript.Resources;

namespace SosigScript
{
	/// <summary>
	///     Represents an instance of a SosigScript, and metadata along with it
	/// </summary>
	public class SosigScript
	{
		private readonly FileInfo _file;

		private readonly Script _script;

		/// <summary>
		///     Creates a new instance of a SosigScript
		/// </summary>
		/// <param name="file">File to load</param>
		public SosigScript(FileSystemInfo file) : this(file.FullName)
		{
		}

		/// <summary>
		///     Creates a new instance of a SosigScript
		/// </summary>
		/// <param name="path">Path to the script</param>
		/// <exception cref="FileNotFoundException">File specified in path returned null</exception>
		public SosigScript(string path)
		{
			if (!File.Exists(path))
				throw new FileNotFoundException($"Could not find file {path}");
			var file = new FileInfo(path);

			_script = new Script();
			_script.LoadFile(file.FullName, null, file.Name);
			_file = file;
		}

		/// <summary>
		///     Options of the current script
		/// </summary>
		public ScriptOptions Options => _script.Options;

		/// <summary>
		///     Metadata for the Script
		/// </summary>
		/// <exception cref="ScriptRuntimeException">Metadata could not be retrieved (Script global not defined)</exception>
		public ResourceMetadata ScriptMetadata
		{
			get
			{
				if (!IsDefined("ScriptInfo"))
					throw new ScriptRuntimeException(
						$"Script metadata not found in file {_file.Name}. Did you forget to make the ScriptInfo table?");

				Table metadata = _script.Globals.Get("ScriptInfo").Table;

				return new ResourceMetadata
				{
					Name = metadata.Get("Name").String,
					GUID = metadata.Get("GUID").String,
					Author = metadata.Get("Author").String,
					Description = metadata.Get("Description").String,
					Version = metadata.Get("Version").String,
					Dependencies = metadata.Get("Dependencies").Table.Values.AsObjects<string>().ToArray(),
					File = _file
				};
			}
		}

		/// <summary>
		///     Adds a global to the current script
		/// </summary>
		/// <param name="name">Name of the variable</param>
		/// <param name="obj">Value to assign</param>
		/// <typeparam name="T">Type of the value (Must be registered)</typeparam>
		public void AddGlobal<T>(string name, T obj) =>
			_script.Globals[name] = DynValue.FromObject(_script, UserData.Create(obj));

		/// <summary>
		///     Adds a global to the current script
		/// </summary>
		/// <param name="name">Name of the variable</param>
		/// <param name="obj">Value to assign</param>
		public void AddGlobal(string name, object obj) => _script.Globals[name] = DynValue.FromObject(_script, obj);

		/// <summary>
		///     Executes a function in the script
		/// </summary>
		/// <param name="funcName">Name of the function</param>
		/// <returns>Return value of the function</returns>
		public DynValue Execute(string funcName) => _script.Call(funcName);

		/// <summary>
		///     Executes a function in the script
		/// </summary>
		/// <param name="funcName">Name of the function</param>
		/// <param name="args">Arguments to pass to the function</param>
		/// <returns>Return value of the function</returns>
		public DynValue Execute(string funcName, params object[] args) => _script.Call(funcName, args);

		/// <summary>
		///     Checks if a global is defined in the script
		/// </summary>
		/// <param name="global">Global to check</param>
		/// <returns>Result of the check</returns>
		public bool IsDefined(string global) => _script.Globals.Keys.Any(key => key.String == global);
	}
}
