using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor
{
    public class ActionManager : MonoBehaviour
    {
        private static bool _cameraMoveAndZoom;
        private static bool _contentMove;

        public static bool CameraMoveAndZoom
        {
            get => _cameraMoveAndZoom;
            set
            {
                _cameraMoveAndZoom = value;
            }
        }
        public static bool ContentMove
        {
            get => _contentMove;
            set 
            {
                _contentMove = value;
            }
        }
        public static void AnyoneFalse()
        {
            _cameraMoveAndZoom = false;
            _contentMove = false;
        }
        private void Start()
        {
            _cameraMoveAndZoom = true;
            _contentMove = false;
        }
    }
}