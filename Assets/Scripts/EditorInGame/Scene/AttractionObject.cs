using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor
{
    public class AttractionObject : MonoBehaviour
    {
        public bool WasMouseDown { get; set; }
        public bool IsDrag { get; set; }
        private void Start()
        {
            WasMouseDown = true;
            IsDrag = true;
        }
        private void OnMouseDown() => WasMouseDown = true;
        private void OnMouseDrag() => IsDrag = true;
        private void OnMouseUp() => IsDrag = false;
    }
}