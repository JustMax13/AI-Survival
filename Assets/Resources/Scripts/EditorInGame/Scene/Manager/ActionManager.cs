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
        //private static bool _contentMove;

        public static bool CameraMoveAndZoom
        {
            get => _cameraMoveAndZoom;
            set
            {
                _cameraMoveAndZoom = value;
            }
        }
        public static bool ActionButtonDown 
        {
            get => _actionButtonDown;
            set { _actionButtonDown = value; }
        }
        //public static bool ContentMove
        //{
        //    get => _contentMove;
        //    set 
        //    {
        //        _contentMove = value;
        //    }
        //}
        private void Start()
        {
            _cameraMoveAndZoom = true;
            _actionButtonDown = false;
            //_contentMove = false;

            DragAndDropPart.PartStartDrag += SomeOnePartStartDrag;
            DragAndDropPart.PartEndDrag += SomeOnePartEndDrag;
        }
        public static void AnyoneFalse()
        {
            _cameraMoveAndZoom = false;
            _actionButtonDown = false;
            //_contentMove = false;
        }
        public static void SomeOnePartStartDrag()
        {
            Debug.Log("SomeOnePartStartDrag");
            _cameraMoveAndZoom = false;
        }
        public static void SomeOnePartEndDrag()
        {
            Debug.Log("SomeOnePartEndrag");
            _cameraMoveAndZoom = true;
        }
    }
}