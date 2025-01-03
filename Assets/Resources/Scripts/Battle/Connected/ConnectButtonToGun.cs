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
        private void OnConnectedShotButton(Button shot, RotateButtonParent rotate)
        {
            if (!_gunConnected && gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                int index = -1;

                var connectShotButton = shot.transform.parent.GetComponent<ConnectShotButton>();
                {
                    int counter = 0;
                    foreach (var item in connectShotButton.ShotAndRotatePair.Values)
                    {
                        if (item == rotate)
                        {
                            index = counter;
                            break;
                        }
                        counter++;
                    }    
                }
                
                //for (int i = 0; i < connectShotButton.ShotAndRotatePair.Count; i++)
                //{
                //    if (connectShotButton.ShotAndRotatePair.Values == shot)
                //    {
                //        index = i;
                //        break;
                //    }
                //}

                if (index != -1)
                {
                    shot.gameObject.SetActive(true);
                    shot.onClick.AddListener(gameObject.GetComponent<ShotGun>().CheckReloadAndShot);
                    rotate.gameObject.SetActive(true);
                    rotate.Up.onClick.AddListener(gameObject.GetComponent<GunRotation>().RotateUp);
                    rotate.Down.onClick.AddListener(gameObject.GetComponent<GunRotation>().RotateDown);

                    _gunConnected = true;
                    connectShotButton.BusyShotAndRotatePair[index] = true;
                }
            }
        }
        private void OnDestroy()
        {
            ButtonConnection.ConnectedShotButton -= OnConnectedShotButton;
        }
    }
}