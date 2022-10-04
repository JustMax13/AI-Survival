using System;
using UnityEngine;
using UnityEngine.UI;

namespace CombatMechanics
{
    public class ButtonConnection : MonoBehaviour
    {
        public static event Action<Button> ConnectedShotButton;
        public static void CallConnectedShotButton(Button shotButton)
        {
            if(shotButton) ConnectedShotButton?.Invoke(shotButton);
        }
    }
}