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
            if (!_gunConnected && gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                int index = -1;
                GameObject shotButtonParent = shotButton.transform.parent.gameObject;

                var connectedShotButton = shotButtonParent.GetComponent<ConnectedShotButton>();
                for (int i = 0; i < connectedShotButton.ShotButtons.Length; i++)
                {
                    if (connectedShotButton.ShotButtons[i] == shotButton)
                    {
                        index = i;
                        break;
                    }
                }

                if (index != -1) // тут дописать условие: если это пушка игрока
                {
                    shotButton.gameObject.SetActive(true);
                    shotButton.onClick.AddListener(gameObject.GetComponent<ShotGun>().CheckReloadAndShot);
                    _gunConnected = true;
                    shotButtonParent.GetComponent<ConnectedShotButton>().BusyShotButton[index] = true;
                }
            }
        }
        private void OnDestroy()
        {
            ButtonConnection.ConnectedShotButton -= OnConnectedShotButton;
        }
    }
}