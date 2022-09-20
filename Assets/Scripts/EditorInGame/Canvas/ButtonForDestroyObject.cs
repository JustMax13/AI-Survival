using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Editor
{
    public class ButtonForDestroyObject : Button
    {
        private GameObject _selectedPart;
        public GameObject SelectedPart
        {
            get => _selectedPart;
            set
            {
                _selectedPart = value;
            }
        }
        private void DestroySelectPart()
        {
            Destroy(_selectedPart);
            gameObject.GetComponent<Button>().interactable = false;
        }
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            ActionManager.ActionButtonDown = true;
            DestroySelectPart();
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            ActionManager.ActionButtonDown = true;
        }
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

                } else
                {
                    gameObject.GetComponent<Button>().interactable = true;
                }
                
            }
            catch
            {
                gameObject.GetComponent<Button>().interactable = false;
            }
        }
    }
}