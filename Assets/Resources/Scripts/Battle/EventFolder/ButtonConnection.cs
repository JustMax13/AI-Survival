using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace CombatMechanics
{
    public class ButtonConnection : MonoBehaviour
    {
        public static event Action<Button, RotateButtonParent> ConnectedShotButton;
        public static void CallConnectedShotButton(KeyValuePair<Button, RotateButtonParent> keyValuePair)
            => ConnectedShotButton?.Invoke(keyValuePair.Key, keyValuePair.Value);
    }
}