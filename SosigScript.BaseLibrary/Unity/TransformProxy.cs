using MoonSharp.Interpreter;
using UnityEngine;

namespace SosigScript.BaseLibrary.Unity
{
    public class TransformProxy : ObjectProxy
    {
        public Vector3Proxy Position { get; }

        [MoonSharpHidden]
        public new Transform Target { get; private set; }

        [MoonSharpHidden]
        public TransformProxy(Transform t) : base(t)
        {
            Target = t;
            Position = new Vector3Proxy(t.position);
        }
    }
}
