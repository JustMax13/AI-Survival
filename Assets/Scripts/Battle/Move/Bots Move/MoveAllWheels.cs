using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    using Move;
    public class MoveAllWheels : MonoBehaviour
    {
        [SerializeField]
        private string TagWhichWheels;
        private bool inMoveLeft { get; set; }
        private bool inMoveRight { get; set; }
        public void MoveLeftDown()
        {
            GameObject[] Wheels = GameObject.FindGameObjectsWithTag(TagWhichWheels);
            foreach (var item in Wheels)
                item.GetComponent<BotsWheel>()?.MoveLeft();
            inMoveLeft = true;
        }
        public void MoveLeftUp() => inMoveLeft = false;
        public void MoveRightDown()
        {
            GameObject[] Wheels = GameObject.FindGameObjectsWithTag(TagWhichWheels);
            foreach (var item in Wheels)
                item.GetComponent<BotsWheel>()?.MoveRight();
            inMoveRight = true;
        }
        public void MoveRightUp() => inMoveRight = false;
        private void FixedUpdate()
        {
            if (inMoveLeft)
            {
                MoveLeftDown();
            }
            else if (inMoveRight)
            {
                MoveRightDown();
            }
        }
        private void Start()
        {
            inMoveLeft = false;
            inMoveRight = false;
        }
    }
}