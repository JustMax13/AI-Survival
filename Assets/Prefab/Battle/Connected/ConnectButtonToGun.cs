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
            int index = -1;
            GameObject shotButtonParent = shotButton.transform.parent.gameObject;

            for (int i = 0; i < shotButtonParent.GetComponent<ConnectedShotButton>().ShotButtons.Length; i++)
            {
                if (shotButtonParent.GetComponent<ConnectedShotButton>().ShotButtons[i] == shotButton)
                {
                    index = i;
                    break;
                }
            }

            if (!_gunConnected && index != -1)
            {
                shotButton.gameObject.SetActive(true);
                shotButton.onClick.AddListener(gameObject.GetComponent<ShotGun>().CheckReloadAndShot);
                _gunConnected = true;
                shotButtonParent.GetComponent<ConnectedShotButton>().BusyShotButton[index] = true;
            }
        }
        private void OnDestroy()
        {
            ButtonConnection.ConnectedShotButton -= OnConnectedShotButton;
        }
    }
}