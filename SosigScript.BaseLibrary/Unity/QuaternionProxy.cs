using MoonSharp.Interpreter;
using UnityEngine;

namespace SosigScript.BaseLibrary.Unity
{
    public class QuaternionProxy
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }

        [MoonSharpHidden]
        public Quaternion ToUnity => new Quaternion(X, Y, Z, W);

        [MoonSharpHidden]
        public QuaternionProxy(Quaternion quat)
        {
            X = quat.x;
            Y = quat.y;
            Z = quat.z;
            W = quat.w;
        }
    }
}
