using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General.PartOfBots;

namespace General.Saves
{
    public struct ConnectedBody2D
    {
        public float XAnchor { get; set; }
        public float YAnchor { get; set; }
        public float XConnectedAnchor { get; set; }
        public float YConnectedAnchor { get; set; }
        public int ID { get; set; }
        public ConnectedBody2D(GameObject fixedPart, int jointNumber)
        {
            AnchoredJoint2D[] joints = fixedPart.GetComponents<AnchoredJoint2D>();

            XAnchor = joints[jointNumber].anchor.x;
            YAnchor = joints[jointNumber].anchor.y;
            XConnectedAnchor = joints[jointNumber].connectedAnchor.x;
            YConnectedAnchor = joints[jointNumber].connectedAnchor.y;

            ID = 0;
        }

    }
}