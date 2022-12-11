using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor
{
    public class AttractionObject : MonoBehaviour
    {
        public bool WasMouseDown { get; set; }
        private const float _timeWasMouseDown = 0.02f;
        private float _currentTimeWasMouseDown;
        private bool _firstTimeMouseUp;
        public bool IsDrag { get; set; }
        private void Start()
        {
            WasMouseDown = false;
            IsDrag = false;
            _firstTimeMouseUp = false;
            _currentTimeWasMouseDown = 0;
        }
        private void OnMouseDown() 
        {
            try
            {
                if (gameObject.GetComponent<DragAndDropPart>().IsSelected)
                {
                    WasMouseDown = true;
                    IsDrag = true;
                }
            }
            catch
            {
                Debug.Log($"{gameObject.name} lost DragAndDropPart script.");
            }
        }
        private void OnMouseUp()
        {
            try
            {
                if (gameObject.GetComponent<DragAndDropPart>().IsSelected)
                {
                    IsDrag = false;
                    _currentTimeWasMouseDown = _timeWasMouseDown;
                }
            }
            catch
            {
                Debug.Log($"{gameObject.name} lost DragAndDropPart script.");
            }
        } 
        private void Update()
        {
            if (_firstTimeMouseUp && Input.GetMouseButtonUp(0))
            {
                IsDrag = false;
                _currentTimeWasMouseDown = _timeWasMouseDown;
                _firstTimeMouseUp = false;
            }

            if (gameObject.GetComponent<FixedJoint2D>())
            {
                foreach (var item in gameObject.GetComponents<FixedJoint2D>())
                {
                    if (item.connectedBody.gameObject.GetComponent<AttractionObject>().IsDrag) 
                        Destroy(item);
                }
            }
        }
        private void FixedUpdate()
        {
            if (_currentTimeWasMouseDown > 0)
                _currentTimeWasMouseDown -= Time.deltaTime;
            else if (WasMouseDown && !IsDrag)
                WasMouseDown = false;
        }
    }
}