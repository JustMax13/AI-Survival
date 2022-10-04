using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CombatMechanics
{
    public class ConnectedShotButton : MonoBehaviour
    {
        [SerializeField] private Button[] _shotButtons;

        private void Start()
        {
            SpawnEnd.SpawnEnded += OnSpawnEnd;
        }
        private void OnSpawnEnd()
        {
            foreach (var item in _shotButtons)
            {
                ButtonConnection.CallConnectedShotButton(item);
            }
        }
    }
}