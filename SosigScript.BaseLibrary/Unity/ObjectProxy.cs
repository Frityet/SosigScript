using MoonSharp.Interpreter;
using UnityEngine;

namespace SosigScript.BaseLibrary.Unity
{
    public class ObjectProxy
    {
        public string Name { get; }

        [MoonSharpHidden]
        public Object Target { get; private set; }

        public ObjectProxy(Object obj)
        {
            Target = obj;
            Name = obj.name;
        }
    }
}
