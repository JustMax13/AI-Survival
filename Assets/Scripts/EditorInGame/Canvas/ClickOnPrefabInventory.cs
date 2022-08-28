using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Editor
{
    public class ClickOnPrefabInventory : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            ActionManager.CameraMoveAndZoom = false;
            ActionManager.ContentMove = true;
        }

        public void EndTouch()
        {
            ActionManager.ContentMove = false;
            ActionManager.CameraMoveAndZoom = true;
        }
        private void Update()
        {
            if (Input.touchCount == 0 && ActionManager.ContentMove)
                EndTouch();
        }
    }
}