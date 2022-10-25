using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General.Saves
{
    public class PartData
    {
        // должен иметь состояние болтов: включен/ выключен конкретный болт
        // массив болтов. каждый болт будет иметь ID
        public float XAnchorInFixedJoint2D { get; set; } 
        public float YAnchorInFixedJoint2D { get; set; }// массив из этих параметров,
        // так как подклюений может быть несколько
        public string PathToPrefab { get; set; }

        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public Rigidbody2D ConnectedRigidbodyInFixedJoint2D { get; set; }

        public PartData(GameObject gameObject)
        {
            // сохранить все свойства с проверкой на null
        }
}
}