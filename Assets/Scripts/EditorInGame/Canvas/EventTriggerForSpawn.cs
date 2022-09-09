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
        //private bool _itsClick;
        public bool IsHold { get => _isHold; }
        //public bool ItsClick 
        //{ 
        //    get => _itsClick;
        //}
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
        //public override void OnPointerDown(PointerEventData eventData)
        //{
        //    base.OnPointerDown(eventData);
            
        //}
        //public override void OnPointerUp(PointerEventData eventData)
        //{
        //    base.OnPointerUp(eventData);
        //    _isHold = false;
        //}
        //public override void OnPointerClick(PointerEventData eventData)
        //{
        //    base.OnPointerClick(eventData);
        //    _itsClick = true;
        //}
        private void Start()
        {
            _isHold = false;
            //_itsClick = false;
        }
    }
}