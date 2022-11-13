using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General.Saves
{
    public struct ConnectedBody2D
    {
        public float XAnchor { get; private set; }
        public float YAnchor { get; private set; }
        public int ID { get; set; }
        public ConnectedBody2D(GameObject gameObject)
        {
            XAnchor = gameObject.GetComponent<FixedJoint2D>().anchor.x;
            YAnchor = gameObject.GetComponent<FixedJoint2D>().anchor.y;

            ID = 0;
        }

    }
}