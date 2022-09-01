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
            gameObject.AddComponent<PrefabSpawn>();
            
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            _isHold = false;
            try
            {
                Destroy(gameObject.GetComponent<PrefabSpawn>());
            }
            catch
            {
                Debug.Log($"PrefabSpawn can't destroy if class PrefabSpawn dont found");
            }
        }
        private void Start()
        {
            _isHold = false;
        }
    }
}