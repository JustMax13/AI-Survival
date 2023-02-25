using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.Moves
{
    public class DragAndDropValue : MonoBehaviour
    {
        [SerializeField] public float _movementSpeed;

        [SerializeField] public GameObject _limitPoint1;
        [SerializeField] public GameObject _limitPoint2;

        public static float MovementSpeed { get; set; }
        public static GameObject LimitPoint1 { get; set; }
        public static GameObject LimitPoint2 { get; set; }

        private void Awake()
        {
            if (_movementSpeed > 0)
                MovementSpeed = _movementSpeed;
            else
                if (_movementSpeed == 0) throw new Exception("_movementSpeed is empty!");
            else
                throw new Exception("_movementSpeed < 0!");

            if (_limitPoint1)
                LimitPoint1 = _limitPoint1;
            else
                throw new Exception("_limitPoint1 = null!");

            if (_limitPoint2)
                LimitPoint2 = _limitPoint2;
            else
                throw new Exception("_limitPoint2 = null!");
        }
    }
}