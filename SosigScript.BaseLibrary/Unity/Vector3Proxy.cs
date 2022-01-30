using UnityEngine;

namespace SosigScript.BaseLibrary.Unity
{
    public class Vector3Proxy
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3 ToUnity => new Vector3(X, Y, Z);
        public Vector3Proxy(Vector3 vec)
        {
            X = vec.x;
            Y = vec.y;
            Z = vec.z;
        }
    }
}
