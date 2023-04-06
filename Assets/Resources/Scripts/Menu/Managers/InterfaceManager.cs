using Menu.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menu.Managers
{
    public class InterfaceManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _allInterfaces;

        public GameObject[] AllInterfaces { get => _allInterfaces; private set => _allInterfaces = value; }

        public void SetActiveInterface(InterfaceValue interfaceValue)
        {
            foreach (var item in _allInterfaces)
                item.SetActive(false);

            interfaceValue.gameObject.SetActive(true);
            foreach (var item in interfaceValue.ActiveIntesfaceWithThat)
                item.SetActive(true);
        }
    }
}