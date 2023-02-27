using General.PartOfBots;
using UnityEngine;

namespace Editor.Moves
{
    using Editor.Interface;
    using General;
    using System;

    public class DragAndDropPart : MonoBehaviour
    {
        private bool _isSelected;
        private float _movementSpeed;

        private Transform _limitPoint1;
        private Transform _limitPoint2;

        private ButtonForDestroyObject _destroyButton;
        private RotatePartButton[] _rotationButton;
        private PartOfBot _partOfBot;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if(_isSelected != value)
                {
                    _isSelected = value;
                    if (value)
                        WhenIsSelectSetTrue();

                    Debug.Log(gameObject + $"ActionManager.SomeOnePartIsSelected({value});");
                    ActionManager.SomeOnePartIsSelected(value);
                }
            }

        }
        public PartOfBot PartOfBot
        {
            get => _partOfBot;
            set { _partOfBot = value; }
        }

        private void Start()
        {
            gameObject.TryGetComponent<Rigidbody2D>(out var rigidbody2D);

            if (rigidbody2D)
                rigidbody2D.bodyType = RigidbodyType2D.Kinematic;

            SetRigidbodyTypeForAllChild(gameObject.transform, RigidbodyType2D.Kinematic);

            _movementSpeed = DragAndDropValue.MovementSpeed;

            _limitPoint1 = DragAndDropValue.LimitPoint1;
            _limitPoint2 = DragAndDropValue.LimitPoint2;

            _destroyButton = GameObject.FindGameObjectWithTag("DestroyButton").GetComponent<ButtonForDestroyObject>();

            GameObject[] rotationButtonObject = GameObject.FindGameObjectsWithTag("RotationButton");
            _rotationButton = new RotatePartButton[rotationButtonObject.Length];

            for (int i = 0; i < rotationButtonObject.Length; i++)
                _rotationButton[i] = rotationButtonObject[i].GetComponent<RotatePartButton>();

            IsSelected = true;

            ClickOnPrefabInventory.PressOnInventory += SetIsSelectedFalse;
        }

        private void SetIsSelectedFalse() => _isSelected = false;

        private void Update()
        {
            //Debug.Log(!ActionManager.ActionButtonDown);
            if (!ClickedPart.MouseButtonUp && IsSelected && !ActionManager.ActionButtonDown)
            {
                Vector2 touth = DragAndDrop.MousePositionOnDragArea(_limitPoint1.position, _limitPoint2.position);
                transform.position = new Vector2(Mathf.Lerp(transform.position.x, touth.x, _movementSpeed * Time.deltaTime),
                    Mathf.Lerp(transform.position.y, touth.y, _movementSpeed * Time.deltaTime));
            }

            UpdateIsSelect(this);
        }
        private bool CursorOverThisPart(Vector2 mousePosition)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            foreach (var item in Physics2D.OverlapPointAll(mousePosition))
            {
                if (transform == item.transform)
                    return true;
            }

            return false;
        }
        private static bool CursorOverThisPart(Vector2 mousePosition, DragAndDropPart dragAndDropPart)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            foreach (var item in Physics2D.OverlapPointAll(mousePosition))
            {
                if (dragAndDropPart.transform == item.transform)
                    return true;
            }

            return false;
        }
        private void WhenIsSelectSetTrue()
        {
            try
            {
                if (_destroyButton)
                {
                    _destroyButton.SelectedPart = gameObject;
                    _destroyButton.PartOfBot = _partOfBot;
                }
                else throw new System.Exception("Destroy button isn't found!");

                if (_rotationButton != null)
                {
                    foreach (var item in _rotationButton)
                        item.SelectedPart = gameObject;
                }
                else throw new System.Exception("Lost RotationButton");
            }
            catch { throw new System.Exception("DestroyButton lost component: ButtonForDestroyObject"); }
        }
        private void SetRigidbodyTypeForAllChild(Transform transform, RigidbodyType2D rigidbodyType2D)
        {
            foreach (Transform child in transform)
            {
                child.TryGetComponent<Rigidbody2D>(out var childRB);

                if (childRB)
                    childRB.bodyType = rigidbodyType2D;

                if (child.childCount > 0)
                    SetRigidbodyTypeForAllChild(child, rigidbodyType2D);
            }
        }
        public static void UpdateIsSelect(DragAndDropPart dragAndDropPart)
        {
            if (dragAndDropPart.IsSelected == true && Input.GetMouseButtonDown(0) && !CursorOverThisPart(Input.mousePosition, dragAndDropPart) && !ActionManager.ActionButtonDown)
                dragAndDropPart.IsSelected = false;
        }
    }
}