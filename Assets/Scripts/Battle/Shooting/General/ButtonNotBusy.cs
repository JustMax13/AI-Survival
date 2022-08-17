using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    public class ButtonNotBusy : MonoBehaviour
    {
        public bool ButtonNotBusyNow { get; set; }
        private void Awake()
        {
            ButtonNotBusyNow = true;
        }
        private void Start()
        {
            if (ButtonNotBusyNow)
                gameObject.SetActive(false);
        }
    }
}