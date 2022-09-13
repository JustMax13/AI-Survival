using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor
{
    public class AttractionObject : MonoBehaviour
    {
        public bool WasMouseDown { get; set; }
        private const float _timeWasMouseDown = 0.2f;
        private float _currentTimeWasMouseDown;
        public bool IsDrag { get; set; }
        private void Start()
        {
            WasMouseDown = false;
            IsDrag = false;
            _currentTimeWasMouseDown = 0;
        }
        private void OnMouseDown() 
        {
            WasMouseDown = true;
            IsDrag = true;
        }  // как сделать так, что бы оно потом выключалось?
        private void OnMouseUp()
        {
            IsDrag = false;
            _currentTimeWasMouseDown = _timeWasMouseDown;
        } 
        private void Update()
        {
            if (_currentTimeWasMouseDown > 0)
                _currentTimeWasMouseDown -= Time.deltaTime;
            else if (WasMouseDown && !IsDrag)
                WasMouseDown = false;
        }
    }
}