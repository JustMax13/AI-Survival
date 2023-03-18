using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General.Saves
{
    public struct ConnectedBody2D
    {
        public float XAnchor { get; set; }
        public float YAnchor { get; set; }
        public int ID { get; set; }
        public ConnectedBody2D(GameObject fixedPart, int jointNumber)
        {
            FixedJoint2D[] joints = fixedPart.GetComponents<FixedJoint2D>();

            XAnchor = joints[jointNumber].anchor.x;
            YAnchor = joints[jointNumber].anchor.y;

            ID = 0;
        }

    }
}