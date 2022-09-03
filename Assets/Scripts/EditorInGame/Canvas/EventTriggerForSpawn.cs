using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Editor
{
    public class EventTriggerForSpawn : EventTrigger
    {
        private bool _isHold;
        public bool IsHold { get => _isHold; }
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            _isHold = true;
            
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            _isHold = false;
        }
        private void Start()
        {
            _isHold = false;
        }
    }
}