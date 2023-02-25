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

        private event Action _wasClick;

        public static bool MouseButtonUp { get => _mouseButtonUp; }

        private void Start()
        {
            _mouseButtonUp = false;
            _onGoingCheck = false;

            _maxTimeForClick = 0.4f;
            _currentPressTime = 0;

            _wasClick += FindAndClickOnPart;
        }
        private void Update()
        {   
            if (_onGoingCheck)
                _currentPressTime += Time.deltaTime;

            if (Input.GetMouseButtonDown(0))
            {
                _mouseButtonUp = false;
                _mouseDownPosition = Input.mousePosition;

                CheckOnClickAsync();
            }
            else if (Input.GetMouseButtonUp(0))
                _mouseButtonUp = true;
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

            var dragAndDropOfPart = part.GetComponent<DragAndDropPart>();

            if (dragAndDropOfPart.IsSelected)
                dragAndDropOfPart.MoveFollowingTheCursor();
            else
                dragAndDropOfPart.IsSelected = true;
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

            var spriteRenderersWithUpperSortingID = new List<SpriteRenderer>();
            spriteRenderersWithUpperSortingID = SpriteManagement.FindSpriteRendererWithID(onlyPartSpriteRenderers, upperSortingLayer);
            SpriteRenderer upperSpriteRenderer = SpriteManagement.FindUpperSpriteRenderer(spriteRenderersWithUpperSortingID, upperSortingLayer);

            return upperSpriteRenderer.gameObject;
        }
    }
}