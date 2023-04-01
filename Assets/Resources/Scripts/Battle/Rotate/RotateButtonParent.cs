using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateButtonParent : MonoBehaviour
{
    [SerializeField] private Button _up;
    [SerializeField] private Button _down;

    public Button Up { get => _up; }
    public Button Down { get => _down; }
}
