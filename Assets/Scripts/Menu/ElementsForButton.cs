using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Menu
{
    namespace ButtonEditor
    {
        public class ElementsForButton : MonoBehaviour
        {

            [SerializeField] private Button _playButton;
            [SerializeField] private GameObject _lvlButton;
            private void Start()
            {
                _lvlButton.GetComponent<EnablePlay>().PlayButton = _playButton;
            }
        }
        

    }

}
