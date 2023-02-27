using Editor.Moves;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor
{
    public class ActionManager : MonoBehaviour
    {
        private static bool _cameraMoveAndZoom;
        private static bool _actionButtonDown;

        public static bool CameraMoveAndZoom 
        { 
            get => _cameraMoveAndZoom; 
            set => _cameraMoveAndZoom = value;
        }
        public static bool ActionButtonDown 
        {
            get => _actionButtonDown;
            set => _actionButtonDown = value;
        }
        private void Start()
        {
            _cameraMoveAndZoom = true;
            _actionButtonDown = false;
        }
        public static void AnyoneFalse()
        {
            _cameraMoveAndZoom = false;
            _actionButtonDown = false;
        }
        public static void SomeOnePartIsSelected(bool isSelected) => _cameraMoveAndZoom = !isSelected;
    }
}