using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Editor.Interface
{
    public class ClickOnPrefabInventory : MonoBehaviour, IPointerDownHandler
    {
        public static event Action PressOnInventory;
        public void OnPointerDown(PointerEventData eventData)
        {
            PressOnInventory?.Invoke();
            EventManager.CheckConditionsAndStartEvent(EventManager.ActionType.PressInteractionInterface);
        }
    }
}