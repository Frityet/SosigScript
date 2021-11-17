using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;

namespace SosigScript
{
    public class Library : Resource
    {
        private Dictionary<string, object> _registerObjects;

        public Library()
        {
            _registerObjects = new Dictionary<string, object>();
        }

        public void RegisterObject(object obj, string name)
        {
            var type = obj.GetType();
            if (!UserData.IsTypeRegistered(type))
                UserData.RegisterType(type);

            _registerObjects.Add(name, obj);
        }
    }
}
