using UnityEngine;

namespace SosigScript.BaseLibrary.Unity
{
    public class Vector2Proxy
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2 ToUnity => new Vector2(X, Y);

        public Vector2Proxy(Vector2 vec)
        {
            X = vec.x;
            Y = vec.y;
        }
    }
}
