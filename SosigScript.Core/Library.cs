using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;

namespace SosigScript
{
    public class Library : Resource
    {
        internal Dictionary<string, object> RegisteredObjects { get; private set; }

        public Library()
        {
            RegisteredObjects = new Dictionary<string, object>();
            LibraryLoader.RegisterLibrary(this);
        }

        public void RegisterObject(string name, object obj)
        {
            var type = obj.GetType();
            if (!UserData.IsTypeRegistered(type))
                UserData.RegisterType(type);

            RegisteredObjects.Add(name, obj);
        }
    }
}
