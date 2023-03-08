using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Editor.Interface
{
    public class ClickOnPrefabInventory : MonoBehaviour, IPointerDownHandler
    {
        //private static bool _onPointerDownStart;
        public static event Action PressOnInventory;
        private void Update()
        {
            //if (_onPointerDownStart)
            //{
            //    if (Input.touchCount == 0)
            //    {
            //        _onPointerDownStart = false;
            //        ActionManager.CameraMoveAndZoom = true;
            //    }
            //}
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            //_onPointerDownStart = true;
            PressOnInventory?.Invoke();
            EventManager.CheckConditionsAndStartEvent(EventManager.ActionType.PressInteractionInterface);
        }
    }
}