using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using General;

namespace CombatMechanics
{
    public class SpawnPlayer : SpawnPrefab
    {
        [SerializeField]
        private GameObject LeftButton;
        [SerializeField]
        private GameObject RightButton;

        private void Start()
        {
            LeftButton.SetActive(true);
            RightButton.SetActive(true);
        }
    }
}