using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General.Saves
{
    public struct PartRotation
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Z { get; private set; }
        public float W { get; private set; }
        public PartRotation(GameObject gameObject)
        {
            X = gameObject.transform.rotation.x;
            Y = gameObject.transform.rotation.y;
            Z = gameObject.transform.rotation.z;
            W = gameObject.transform.rotation.w;
        }
    }
}