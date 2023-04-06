using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menu.Interfaces
{
    public class InterfaceValue : MonoBehaviour
    {
        [SerializeField] private GameObject[] _activeIntesfaceWithThat;

        public GameObject[] ActiveIntesfaceWithThat { get => _activeIntesfaceWithThat; private set => _activeIntesfaceWithThat = value; }
    }
}