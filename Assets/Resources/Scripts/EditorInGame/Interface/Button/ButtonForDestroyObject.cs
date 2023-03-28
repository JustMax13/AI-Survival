using CombatMechanics.AI;
using Editor.Moves;
using General.PartOfBots;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Editor.Interface
{
    public class ButtonForDestroyObject : Button
    {
        private GameObject _selectedPart;
        private PartOfBot _partOfBot;
        public GameObject SelectedPart { get => _selectedPart; set { _selectedPart = value; } }
        public PartOfBot PartOfBot { get => _partOfBot; set { _partOfBot = value; } }
        public static Action<GameObject> BeforeRemovingPart;
        protected override void Start()
        {
            _selectedPart = null;
        }
        private void Update()
        {
            try
            {
                if (!_selectedPart.GetComponent<DragAndDropPart>().IsSelected)
                {
                    _selectedPart = null;
                    gameObject.GetComponent<Button>().interactable = false;
                }
                else
                    gameObject.GetComponent<Button>().interactable = true;
            }
            catch { gameObject.GetComponent<Button>().interactable = false; }
        }

        private void DestroySelectPart()
        {
            PluggableObject pluggableObject;

            try { pluggableObject = _selectedPart.GetComponent<PluggableObject>(); }
            catch { throw new System.Exception($"На {_selectedPart} немає скрипта 'PluggableObject'!"); }

            PluggableObject.FullDisconnect(pluggableObject);
            BeforeRemovingPart?.Invoke(pluggableObject.gameObject);
            Destroy(_selectedPart);
            interactable = false;
        }
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            EventManager.CheckConditionsAndStartEvent(EventManager.ActionType.PressInteractionInterface);

            if (interactable)
                DestroySelectPart();
        }
        //public override void OnPointerUp(PointerEventData eventData)
        //{
        //    base.OnPointerUp(eventData);

        //    ActionManager.ActionButtonDown = false;
        //}
    }
}