using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General.Saves
{
    public struct PartPosition
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Z { get; private set; }
        public PartPosition(GameObject gameObject)
        {
            X = gameObject.transform.position.x;
            Y = gameObject.transform.position.y;
            Z = gameObject.transform.position.z;
        }
    }
}