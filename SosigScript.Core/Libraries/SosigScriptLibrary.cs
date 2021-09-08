using System;
using System.Collections.Generic;
using System.Reflection;
using BepInEx;
using MoonSharp.Interpreter;
using SosigScript.Resources;

namespace SosigScript.Libraries
{
	public class SosigScriptLibrary
	{
		public ResourceMetadata LibraryInfo { get; }
		public IDictionary<string, object> LoadedObjects => _loadedObjects;
		public IDictionary<string, Type> LoadedTypes => _loadedTypes;
		public Assembly Assembly { get; }

		internal LibraryLoader? LibraryLoader { get; set; }

		private Dictionary<string, Type> _loadedTypes;
		private Dictionary<string, object> _loadedObjects;

		public SosigScriptLibrary(ResourceMetadata libraryInfo)
		{
			LibraryInfo = libraryInfo;
			_loadedObjects = new Dictionary<string, object>();
			_loadedTypes = new Dictionary<string, Type>();
			Assembly = Assembly.GetCallingAssembly();

			LibraryLoader.AddLibrary(this);
		}

		public void RegisterObject(string name, object obj)
		{
			_loadedObjects.Add(name, obj);

			Plugin.ScriptLoader!.AddObject(name, obj);
		}

		public void RegisterType(Type type, string name = "")
		{
			UserData.RegisterType(type, InteropAccessMode.Default, name.IsNullOrWhiteSpace() || String.IsNullOrEmpty(name) ? null : name);
			_loadedTypes.Add(name.IsNullOrWhiteSpace() || String.IsNullOrEmpty(name) ? type.Name : name, type);
		}
	}
}
