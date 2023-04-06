using Menu.Managers;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu.Buttons
{
    public class LevelButton : Button
    {
        public static event Action<LevelButton> ButtonOnSelect;
        public static event Action ButtonOnDeselect;

        public override void OnSelect(BaseEventData eventData)
        {
            ButtonOnSelect?.Invoke(this);
            base.OnSelect(eventData);
        }
        public override void OnDeselect(BaseEventData eventData)
        {
            DeselectEvent();
            base.OnDeselect(eventData);
        }
        private IEnumerator DeselectEvent() 
        { 
            yield return new WaitForEndOfFrame();
            ButtonOnDeselect?.Invoke();
        }
    }
}
