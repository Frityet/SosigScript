using MoonSharp.Interpreter;
using UnityEngine;

namespace SosigScript.BaseLibrary.Unity
{
    public class ComponentProxy
    {
        [MoonSharpHidden]
        public Component Target { get; private set; }

        [MoonSharpHidden]
        public ComponentProxy(Component t)
        {
            Target = t;
        }
    }
}
