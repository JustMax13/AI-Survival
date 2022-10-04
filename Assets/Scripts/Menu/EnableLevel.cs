using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Menu.ButtonEditor;

namespace Menu
{
    public class EnableLevel : Button
    {
        private bool _pressLvlButton;
        private const float _turnOffTime = 0.1f;
        private float _currentOffTime;
        private Button _playButton;

        public Button PlayButton
        {
            get => _playButton;
            set { _playButton = value; }
        }
        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            _currentOffTime = _turnOffTime;
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
            _currentOffTime = 0;
        }

        private void FixedUpdate()
        {
            if (_currentOffTime < 0) _playButton.interactable = _pressLvlButton;
            else _currentOffTime -= Time.deltaTime;
        }
    }
}
