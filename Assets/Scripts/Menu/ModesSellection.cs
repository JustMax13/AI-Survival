using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menu 
{
    public class ModesSellection : MonoBehaviour
    {
        [SerializeField] private GameObject _modesSellectionMenu;
        public void OpenModesSellection()
        {
            _modesSellectionMenu.SetActive(true);
        }




    }
}
