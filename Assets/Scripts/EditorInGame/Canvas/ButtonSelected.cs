using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Editor
{
    public class ButtonSelected : Button
    {
        public static GameObject SelectedButton;

        public override void OnPointerDown(PointerEventData eventData) { }
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            Select();
        }
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            SelectedButton = gameObject;

            gameObject.AddComponent<EventTriggerForSpawn>();
        }
        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);

            Destroy(gameObject.GetComponent<EventTriggerForSpawn>());
        }
        protected override void Start()
        {
            SelectedButton = null;
        }
    }
}