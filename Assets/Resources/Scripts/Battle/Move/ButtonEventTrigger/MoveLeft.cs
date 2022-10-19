using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CombatMechanics
{
    public class MoveLeft : EventTrigger
    {
        private GameObject Player;
        public override void OnPointerDown(PointerEventData data) => Player?.GetComponent<MoveAllWheels>().MoveLeftDown();

        public override void OnPointerUp(PointerEventData data) =>Player?.GetComponent<MoveAllWheels>().MoveLeftUp();

        private void Awake() => Player = GameObject.FindGameObjectWithTag("Player");

    }
}