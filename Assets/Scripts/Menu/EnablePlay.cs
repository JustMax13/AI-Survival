using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Menu.ButtonEditor;


namespace Menu
{
    public class EnablePlay : Button
    {
        private bool _pressLvlButton;
        private Button _playButton;

        public Button PlayButton
        {
            get => _playButton;
            set { _playButton = value; }
        }
        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            _pressLvlButton = false;
        }
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            _pressLvlButton = true;
        }

        protected override void Start()
        {
            _pressLvlButton = false;
        }
        
        private void FixedUpdate()
        {
           _playButton.interactable = _pressLvlButton;
        }
    }
    
        
    

}
