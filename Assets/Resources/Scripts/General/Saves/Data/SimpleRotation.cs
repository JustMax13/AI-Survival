using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General.Saves
{
    public struct SimpleRotation
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }
        public SimpleRotation(GameObject gameObject)
        {
            X = gameObject.transform.rotation.x;
            Y = gameObject.transform.rotation.y;
            Z = gameObject.transform.rotation.z;
            W = gameObject.transform.rotation.w;
        }
    }
}