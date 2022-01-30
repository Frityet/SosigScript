using MoonSharp.Interpreter;
using UnityEngine;

namespace SosigScript.BaseLibrary.Unity
{
    public class GameObjectProxy : ObjectProxy
    {
        public TransformProxy Transform { get; }

        public T GetComponent<T>() where T : Component => Target.GetComponent<T>();
        public T AddComponent<T>() where T : Component => Target.AddComponent<T>();

        [MoonSharpHidden]
        public new GameObject Target { get; private set; }

        [MoonSharpHidden]
        public GameObjectProxy(GameObject go) : base(go)
        {
            Target = go;
            Transform = new TransformProxy(go.transform);
        }
    }
}
