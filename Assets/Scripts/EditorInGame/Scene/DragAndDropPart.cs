using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using General;

namespace Editor
{
    public class DragAndDropPart : MonoBehaviour
    {
        [SerializeField] private GameObject _limitPoint1;
        [SerializeField] private GameObject _limitPoint2;
        [SerializeField] private float _backlogCursor;
        private float _clickTime;
        private float _curentClickTime;
        private bool _isSelected;
        private bool _cursorOnObject;
        
        public GameObject LimitPoint1
        {
            get => _limitPoint1;
            set
            {
                _limitPoint1 = value;
            }
        }
        public GameObject LimitPoint2
        {
            get => _limitPoint2;
            set
            {
                _limitPoint2 = value;
            }
        }
        public float BacklogCursor 
        {
            get => _backlogCursor;
            set
            {
                _backlogCursor = value;
            }
        }


        // сделать сначало выделение ( посмотреть как в других играх сделано )
        private void OnMouseDown()
        {
            if(_isSelected) ActionManager.CameraMoveAndZoom = false;
            else _curentClickTime = 0;
        }

        private void OnMouseDrag()
        {
            if(_isSelected)
            {
                Vector2 touth = DragAndDrop.MousePositionOnDragArea(_limitPoint1, _limitPoint2);
                transform.position = new Vector2(Mathf.Lerp(transform.position.x, touth.x, _backlogCursor * Time.deltaTime),
                    Mathf.Lerp(transform.position.y, touth.y, _backlogCursor * Time.deltaTime));
            }
        }
        private void OnMouseUp() 
        {
            if (ActionManager.CameraMoveAndZoom == false) 
                ActionManager.CameraMoveAndZoom = true;
            else if(_curentClickTime <= _clickTime) _isSelected = true;
        }
        private void OnMouseEnter() => _cursorOnObject = true;
        private void OnMouseExit() => _cursorOnObject = false;

        private void Start()
        {
            if (_backlogCursor == 0)
                _backlogCursor = 22;
            _clickTime = 0.5f;
            _curentClickTime = 0;
            _isSelected = false;
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !_cursorOnObject) _isSelected = false;
        }
        private void FixedUpdate()
        {
            if(_curentClickTime <= _clickTime) _curentClickTime += Time.deltaTime;
        }
    }
}