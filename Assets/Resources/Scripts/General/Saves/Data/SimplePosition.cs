using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General.Saves
{
    public struct SimplePosition
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public SimplePosition(GameObject gameObject)
        {
            X = gameObject.transform.position.x;
            Y = gameObject.transform.position.y;
            Z = gameObject.transform.position.z;
        }
    }
}