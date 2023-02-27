using General.Pathes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Editor.Moves
{
    public class ClickedPart : MonoBehaviour
    {
        private static bool _mouseButtonUp;
        private bool _onGoingCheck;

        private float _maxTimeForClick;
        private float _currentPressTime;

        private Vector2 _mouseDownPosition;
        private DragAndDropPart _lastFindPart;

        private event Action _wasClick;

        public static bool MouseButtonUp { get => _mouseButtonUp; }

        private void Start()
        {
            _mouseButtonUp = false;
            _onGoingCheck = false;

            _maxTimeForClick = 0.4f;
            _currentPressTime = 0;

            _lastFindPart = null;

            _wasClick += FindAndClickOnPart;
        }
        private void LateUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _mouseButtonUp = false;
                _mouseDownPosition = Input.mousePosition;

                if(_lastFindPart)
                {
                    DragAndDropPart.UpdateIsSelect(_lastFindPart);

                    if (!_lastFindPart.IsSelected)
                    {
                        _lastFindPart = null;
                        CheckOnClickAsync();
                    }  
                }
                else 
                    CheckOnClickAsync();
            }
            else if (Input.GetMouseButtonUp(0))
                _mouseButtonUp = true;

            if (_onGoingCheck)
                _currentPressTime += Time.deltaTime;
        }
        private async void CheckOnClickAsync()
        {
            _onGoingCheck = true;

            await Task.Run(() => { while (!_mouseButtonUp) { } });

            _onGoingCheck = false;
            if (_currentPressTime <= _maxTimeForClick)
                _wasClick?.Invoke();

            _currentPressTime = 0;
        }
        private void FindAndClickOnPart()
        {
            GameObject part = FindPartWhereMouseDown(_mouseDownPosition);

            if (!part)
                return;

            _lastFindPart = part.GetComponent<DragAndDropPart>();

            if (!_lastFindPart.IsSelected)
                _lastFindPart.IsSelected = true;      
        }
        private GameObject FindPartWhereMouseDown(Vector2 mousePosition)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Collider2D[] colliders = Physics2D.OverlapPointAll(mousePosition);

            var onlyPartSpriteRenderers = new List<SpriteRenderer>();
            foreach (var item in colliders)
            {
                if (item.TryGetComponent<PartPath>(out var partPath))
                    onlyPartSpriteRenderers.Add(partPath.GetComponent<SpriteRenderer>());
            }

            if (onlyPartSpriteRenderers.Count == 0)
                return null;

            EnumPartSortingLayer upperSortingLayer = SpriteManagement.FindUpperSortingLayerID(onlyPartSpriteRenderers);

            var spriteRenderersWithUpperSortingLayer = new List<SpriteRenderer>();
            spriteRenderersWithUpperSortingLayer = SpriteManagement.FindSpriteRendererWithOneSortingLayer(onlyPartSpriteRenderers, upperSortingLayer);
            SpriteRenderer upperSpriteRenderer = SpriteManagement.FindUpperSpriteRenderer(spriteRenderersWithUpperSortingLayer, upperSortingLayer);

            return upperSpriteRenderer.gameObject;
        }
    }
}