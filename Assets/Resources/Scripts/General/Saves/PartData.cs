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
        public PartPosition PartPosition { get; private set; }
        public PartRotation PartRotation { get; private set; }
        public ConnectedBody2D[] ConnectedBodys2D { get; set; }
        public PartData(GameObject gameObject)
        {
            ID = _nextFreeID++;
            PathToPrefab = gameObject.GetComponent<IPathOfPart>().Path;

            PartPosition = new PartPosition(gameObject);
            PartRotation = new PartRotation(gameObject);

            ConnectedBodys2D = new ConnectedBody2D[gameObject.GetComponents<FixedJoint2D>().Length];

            // нужно после всех записаных деталей, добавить в ConnectedBodys2D нужные детали
            // возможно сразу добавить в лист с всеми деталями
        }
    }
}