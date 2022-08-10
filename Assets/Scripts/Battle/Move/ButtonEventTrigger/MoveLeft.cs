using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CombatMechanics
{
    public class MoveLeft : EventTrigger
    {
        private GameObject[] Player;
        public override void OnPointerDown(PointerEventData data)
        {
            for (int i = 0; i < Player.Length; i++)
                Player[i]?.GetComponent<MoveAllWheels>().MoveLeftDown();
        }

        public override void OnPointerUp(PointerEventData data)
        {
            for (int i = 0; i < Player.Length; i++)
                Player[i]?.GetComponent<MoveAllWheels>().MoveLeftUp();
        }

        private void Awake()
        {
            Player = GameObject.FindGameObjectsWithTag("Player");
        }
    }
}