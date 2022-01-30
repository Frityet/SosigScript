using System;
using BepInEx;
using SosigScript.BaseLibrary.Unity;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SosigScript.BaseLibrary
{
    [BepInPlugin("net.frityet.baselib", "SosigScript.BaseLib", "1.0.0")]
    [BepInDependency("net.frityet.sosigscript")]
    public class BaseLibrary : BaseUnityPlugin
    {
        public static Library Library { get; private set; }
        private void Awake()
        {
            Library = new Library("Baselib", "net.frityet.baselib", "Frityet");
            Library.RegisterType<Vector2Proxy>();
            Library.RegisterType<Vector3Proxy>();
            Library.RegisterType<QuaternionProxy>();
            Library.RegisterProxyType<Object, ObjectProxy>(o => new ObjectProxy(o));
            Library.RegisterProxyType<Component, ComponentProxy>(o => new ComponentProxy(o));
            Library.RegisterProxyType<GameObject, GameObjectProxy>(o => new GameObjectProxy(o));
        }
    }
}
