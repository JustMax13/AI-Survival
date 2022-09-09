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
        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            _isHold = true;
        }
        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            _isHold = false;
        }
        private void Start()
        {
            _isHold = false;
        }
    }
}