using General.PartOfBots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General.Pathes;
using Editor;

namespace General.Saves
{
    public class PartData
    {
        private static int _nextFreeID = 0;
        public int ID { get; set; }
        public string PathToPrefab { get; set; }

        public SimplePosition PartPosition { get; set; }
        public SimpleRotation PartRotation { get; set; }

        public ConnectedBody2D[] ConnectedBodys2D { get; set; }

        //public BoltData[] boltsData { get; set; }
        public PartData(){}
        public PartData(GameObject part)
        {
            ID = _nextFreeID++;
            PathToPrefab = part.GetComponent<PartPath>().Path;

            PartPosition = new SimplePosition(part);
            PartRotation = new SimpleRotation(part);

            ConnectedBodys2D = new ConnectedBody2D[part.GetComponents<FixedJoint2D>().Length];

            //var AllBolts = part.GetComponentsInChildren<ConnectPoint>();
            //boltsData = new BoltData[AllBolts.Length];

            //for (int i = 0; i < boltsData.Length; i++)
            //    boltsData[i] = new BoltData(AllBolts[i].gameObject);
        }
    }
}