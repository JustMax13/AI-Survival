using General.PartOfBots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General.Saves
{
    public class PartData
    {
        private static int _nextFreeID = 0;
        public int ID { get; private set; }
        public string PathToPrefab { get; private set; }

        public SimplePosition PartPosition { get; private set; }
        public SimpleRotation PartRotation { get; private set; }

        public ConnectedBody2D[] ConnectedBodys2D { get; set; }

        public BoltData[] boltsData { get; private set; }
        
        public PartData(GameObject part)
        {
            ID = _nextFreeID++;
            PathToPrefab = part.GetComponent<IPathOfPart>().Path;

            PartPosition = new SimplePosition(part);
            PartRotation = new SimpleRotation(part);

            ConnectedBodys2D = new ConnectedBody2D[part.GetComponents<FixedJoint2D>().Length];

            var AllBolts = part.GetComponentsInChildren<Transform>();
            boltsData = new BoltData[AllBolts.Length];

            for (int i = 0; i < AllBolts.Length; i++)
                boltsData[i] = new BoltData(AllBolts[i].gameObject);

            // нужно после всех записаных деталей, добавить в ConnectedBodys2D нужные детали
        }
    }
}