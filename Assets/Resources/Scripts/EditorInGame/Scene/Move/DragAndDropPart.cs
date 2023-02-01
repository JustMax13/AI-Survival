using General;
using General.PartOfBots;
using UnityEngine;

namespace Editor
{
    using Editor.Interface;
    public class DragAndDropPart : MonoBehaviour
    {
        private bool _isSelected;
        private bool _cursorOnObject;
        private float _clickTime;
        private float _curentClickTime;
        private float _movementSpeed;

        private GameObject _limitPoint1;
        private GameObject _limitPoint2;

        private GameObject _destroyButton;
        private GameObject[] _rotationButton;
        private PartOfBot _partOfBot;

        public bool IsSelected { get => _isSelected; }
        public PartOfBot PartOfBot
        {
            get => _partOfBot;
            set { _partOfBot = value; }
        }


        private void OnMouseDown()
        {
            if (_isSelected) ActionManager.CameraMoveAndZoom = false;
            else _curentClickTime = 0;
        }

        private void OnMouseDrag()
        {
            if (_isSelected)
            {
                Vector2 touth = DragAndDrop.MousePositionOnDragArea(_limitPoint1, _limitPoint2);
                transform.position = new Vector2(Mathf.Lerp(transform.position.x, touth.x, _movementSpeed * Time.deltaTime),
                    Mathf.Lerp(transform.position.y, touth.y, _movementSpeed * Time.deltaTime));
            }
        }
        private void OnMouseUp()
        {
            if (ActionManager.CameraMoveAndZoom == false)
                ActionManager.CameraMoveAndZoom = true;
            else if (_curentClickTime <= _clickTime) _isSelected = true;
        }
        private void OnMouseEnter() => _cursorOnObject = true;
        private void OnMouseExit() => _cursorOnObject = false;

        private void Start()
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

            _clickTime = 0.5f;
            _curentClickTime = 0;
            _isSelected = true;

            _movementSpeed = DragAndDropValue.MovementSpeed;

            _limitPoint1 = DragAndDropValue.LimitPoint1;
            _limitPoint2 = DragAndDropValue.LimitPoint2;

            _destroyButton = GameObject.FindGameObjectWithTag("DestroyButton");
            _rotationButton = GameObject.FindGameObjectsWithTag("RotationButton");
        }
        private void Update()
        {
            try
            {
                if (_isSelected)
                {
                    if (_destroyButton)
                    {
                        _destroyButton.GetComponent<ButtonForDestroyObject>().SelectedPart = gameObject;
                        _destroyButton.GetComponent<ButtonForDestroyObject>().PartOfBot = _partOfBot;
                    }
                    else Debug.Log("Destroy button isn't found!");

                    if (_rotationButton != null)
                    {
                        foreach (var item in _rotationButton)
                        {
                            item.GetComponent<RotatePartButton>().SelectedPart = gameObject;
                        }
                    }
                    else Debug.Log("Lost RotationButton");
                }
            }
            catch
            {
                Debug.Log("DestroyButton lost component: ButtonForDestroyObject");
            }

            if (Input.GetMouseButtonDown(0) && !_cursorOnObject && !ActionManager.ActionButtonDown)
                _isSelected = false;
        }
        private void FixedUpdate()
        {
            if (_curentClickTime <= _clickTime) _curentClickTime += Time.deltaTime;
        }
    }
}