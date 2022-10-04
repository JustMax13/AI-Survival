using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CombatMechanics
{
    public class ConnectButtonToGun : MonoBehaviour
    {
        private bool _gunConnected;

        private void Start()
        {
            _gunConnected = false;
            ButtonConnection.ConnectedShotButton += OnConnectedShotButton;
            SpawnEnd.CallSpawnEnded();
        }
        private void OnConnectedShotButton(Button shotButton)
        {
            if(!_gunConnected)
            {
                shotButton.gameObject.SetActive(true);
                shotButton.onClick.AddListener(gameObject.GetComponent<ShotGun>().CheckReloadAndShot);
                _gunConnected = true;
            }
        }
        private void OnDestroy()
        {
            ButtonConnection.ConnectedShotButton -= OnConnectedShotButton;
        }
    }
}