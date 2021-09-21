using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BepInEx;
using MoonSharp.Interpreter;
using SosigScript.Resources;

namespace SosigScript.Libraries
{
	/// <summary>
	///     Library providing additional objects and types to a SosigScript
	/// </summary>
	public class SosigScriptLibrary
	{
		private static readonly Type[] PRIMITIVE_TYPES =
		{
			typeof(char),
			typeof(string),

			typeof(short),
			typeof(int),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),

			typeof(bool),

			typeof(object)
		};

		/// <summary>
		///     Creates a new SosigScriptLibrary
		/// </summary>
		/// <param name="libraryInfo">Metadata about the Library</param>
		public SosigScriptLibrary(ResourceMetadata libraryInfo)
		{
			LibraryInfo = libraryInfo;
			LoadedObjects = new Dictionary<string, object>();
			LoadedTypes = new Dictionary<string, Type>();
			Assembly = Assembly.GetCallingAssembly();

			LibraryLoader.AddLibrary(this);
		}

		/// <summary>
		///     Info about the current library
		/// </summary>
		public ResourceMetadata LibraryInfo { get; }

		/// <summary>
		///     Loaded Objects into SosigScript from this library
		/// </summary>
		public Dictionary<string, object> LoadedObjects { get; }

		/// <summary>
		///     Types loaded into SosigScript from this library
		/// </summary>
		public Dictionary<string, Type> LoadedTypes { get; }

		/// <summary>
		///     Assembly this library originates from
		/// </summary>
		public Assembly Assembly { get; }

		/// <summary>
		///     Loader for this library
		/// </summary>
		internal LibraryLoader? LibraryLoader { get; set; }

		/// <summary>
		///     Registers an object into this library, and sends it to each SosigScript
		/// </summary>
		/// <param name="name">Name of the object</param>
		/// <param name="obj">Value of the object</param>
		/// <param name="registerType">If the type T is not already registered it will be automatically registered</param>
		/// <typeparam name="T">Type of the object</typeparam>
		/// <exception cref="Exception">The type is not registered</exception>
		public void RegisterObject<T>(string name, T obj, bool registerType = true)
		{
			if (obj == null) throw new ArgumentNullException(nameof(obj));
			Type type = typeof(T);

			var typeExists = false;

			//Check if the type is a primitive
			if (PRIMITIVE_TYPES.Any(primType => primType == type))
			{
				LoadedObjects.Add(name, obj);

				//use the non generic option
				Plugin.ScriptLoader!.AddObject(name, obj);

				return;
			}

			if (LoadedTypes.Values.Any(ttype => ttype == type)) typeExists = true;

			if (!typeExists)
			{
				if (registerType)
					RegisterType<T>();
				else
					throw new Exception($"Type {nameof(T)} is not registered with SosigScript");
			}

			LoadedObjects.Add(name, obj);

			Plugin.ScriptLoader!.AddObject<T>(name, obj);
		}

		/// <summary>
		///     Registers a type for use with a SosigScript object
		/// </summary>
		/// <param name="name">Name of the type</param>
		/// <typeparam name="T">Type of the type</typeparam>
		public void RegisterType<T>(string name = "") => RegisterType(typeof(T), name);

		/// <summary>
		///     Registers a type for use with a SosigScript object
		/// </summary>
		/// <param name="type">Type of the type</param>
		/// <param name="name">Name of the type</param>
		public void RegisterType(Type type, string name = "")
		{
			UserData.RegisterType(type, InteropAccessMode.Default,
								  name.IsNullOrWhiteSpace() || string.IsNullOrEmpty(name) ? null : name);
			LoadedTypes.Add(name.IsNullOrWhiteSpace() || string.IsNullOrEmpty(name) ? type.Name : name, type);
		}
	}
}
