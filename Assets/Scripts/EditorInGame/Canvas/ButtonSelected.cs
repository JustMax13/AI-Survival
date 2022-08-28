using Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Editor
{
    public class ButtonSelected : Button
    {
        // | DONE |1) сделать так, чтобы деталь выбиралась только на клик 
        // 2) Когда деталь выбрана на ней появится Event Trigger 
        // 3) Кода выбраная деталь перестает быть выделенной - снимаем Event Trigger
        public override void OnPointerDown(PointerEventData eventData) { }
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            Select();
        }
    }
}