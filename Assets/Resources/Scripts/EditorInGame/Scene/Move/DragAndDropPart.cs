using General;
using General.PartOfBots;
using UnityEngine;

namespace Editor
{
    using Editor.Interface;
    public class DragAndDropPart : MonoBehaviour
    {
        [SerializeField] private float _backlogCursor;

        [SerializeField] private GameObject _limitPoint1;
        [SerializeField] private GameObject _limitPoint2;

        private bool _isSelected;
        private bool _cursorOnObject;
        private float _clickTime;
        private float _curentClickTime;

        private GameObject _destroyButton;
        private GameObject[] _rotationButton;
        private PartOfBot _partOfBot;

        public bool IsSelected { get => _isSelected; }
        public float BacklogCursor
        {
            get => _backlogCursor;
            set
            {
                _backlogCursor = value;
            }
        }

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
                transform.position = new Vector2(Mathf.Lerp(transform.position.x, touth.x, _backlogCursor * Time.deltaTime),
                    Mathf.Lerp(transform.position.y, touth.y, _backlogCursor * Time.deltaTime));
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
            if (_backlogCursor == 0)
                _backlogCursor = 22;
            _clickTime = 0.5f;
            _curentClickTime = 0;
            _isSelected = true;

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

            if (Input.GetMouseButtonDown(0) && !_cursorOnObject && !ActionManager.ActionButtonDown) _isSelected = false;
        }
        private void FixedUpdate()
        {
            if (_curentClickTime <= _clickTime) _curentClickTime += Time.deltaTime;
        }
    }
}