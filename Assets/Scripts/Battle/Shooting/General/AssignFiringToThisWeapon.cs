using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CombatMechanics
{
    namespace Weapon
    {
        public class AssignFiringToThisWeapon : MonoBehaviour
        {
            private static GameObject[] ShotButton;

            private void Start()
            {
                ShotButton = GameObject.FindGameObjectsWithTag("ShotButton");
                if (gameObject.GetComponent<ShotGun>() == null)
                {
                    Debug.Log($"{gameObject.name} lost ShotGan class");
                    return;
                }
                foreach (var item in ShotButton)
                {
                    if (item == null)
                    {
                        Debug.Log($"ShotButton = null");
                        return;
                    }
                }

                foreach (var item in ShotButton)
                {
                    if (item.GetComponent<ButtonNotBusy>() == null)
                    {
                        item.SetActive(false);
                        Debug.Log($"{item.name} == null");
                        continue;
                    }
                    if (item.GetComponent<ButtonNotBusy>().ButtonNotBusyNow)
                    {
                        item.GetComponent<Button>().onClick.AddListener(gameObject.GetComponent<GunShot>()
                            .CheckReloadAndShot);
                        item.GetComponent<ButtonNotBusy>().ButtonNotBusyNow = false;
                        break;
                    }
                }
            }
        }
    }
}