using System;
using System.Collections.Generic;
using HarmonyLib;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;

namespace SosigScript
{
    public class Library : Resource
    {
        internal Dictionary<string, object> RegisteredObjects { get; }
        internal Dictionary<string, Type> RegisteredTypes { get; }
        internal Dictionary<string, KeyValuePair<Type, Type>> RegisteredProxyTypes { get; }

        /// <summary>
        /// Creates a new SosigScript library and registers it
        /// </summary>
        public Library()
        {
            RegisteredObjects = new Dictionary<string, object>();
            RegisteredTypes = new Dictionary<string, Type>();
            RegisteredProxyTypes = new Dictionary<string, KeyValuePair<Type, Type>>();
            LibraryLoader.RegisterLibrary(this);
        }


        /// <summary>
        /// Creates a new SosigScript library and registers it
        /// </summary>
        /// <param name="name">Name of the library</param>
        /// <param name="guid">GUID of the library, must be unique</param>
        /// <param name="author">Creator of the library, if multiple people label a group</param>
        /// <param name="version">Version, 0.0.0</param>
        public Library(string name, string guid, string author, string version = "1.0.0")
        {
            RegisteredObjects = new Dictionary<string, object>();
            RegisteredTypes = new Dictionary<string, Type>();

            Metadata = new ResourceMetadata()
            {
                Name = name,
                GUID = guid,
                Author = author,
                Version = version
            };

            LibraryLoader.RegisterLibrary(this);
        }

        public void RegisterObject(string name, object obj)
        {
            Type type = obj.GetType();
            if (!UserData.IsTypeRegistered(type))
                RegisterType(type.Name, type);

            RegisteredObjects.Add(name, obj);
        }

        public void RegisterType<T>()
        {
            RegisterType(nameof(T), typeof(T));
        }

        public void RegisterType(string name, Type t)
        {
            UserData.RegisterType(t);
            RegisteredTypes[name] = t;
        }

        public void RegisterProxyType<TTarget, TProxy>(Func<TTarget, TProxy> wrapDelegate)  where TProxy : class
                                                                                            where TTarget : class
        {
            Type proxy = typeof(TProxy),
                 target = typeof(TTarget);

            RegisteredProxyTypes[$"{proxy.Name}:{target.Name}"] = new KeyValuePair<Type, Type>(proxy, target);
            UserData.RegisterProxyType(wrapDelegate, InteropAccessMode.Default, proxy.Name);
        }


        public void AddDependency(string dependency)
        {
            var _ = Metadata.Dependencies == null
                ? Metadata.Dependencies = new string[] { dependency }
                : Metadata.Dependencies.AddItem(dependency);
        }
    }
}
