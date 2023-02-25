using General;
using General.PartOfBots;
using UnityEngine;

namespace Editor.Moves
{
    using Editor.Interface;
    using System.Threading.Tasks;

    public class DragAndDropPart : MonoBehaviour
    {
        private bool _isSelected;
        //private float _clickTime;
        //private float _curentClickTime;
        private float _movementSpeed;

        private GameObject _limitPoint1;
        private GameObject _limitPoint2;

        private GameObject _destroyButton;
        private GameObject[] _rotationButton;
        private PartOfBot _partOfBot;

        public bool IsSelected { get => _isSelected; set => _isSelected = value; }
        public PartOfBot PartOfBot
        {
            get => _partOfBot;
            set { _partOfBot = value; }
        }

        private void OnMouseDown()
        {
            if (_isSelected) ActionManager.CameraMoveAndZoom = false;
            //else _curentClickTime = 0;
        }

        //private void OnMouseDrag()
        //{
        //    if (_isSelected)
        //    {
        //        Vector2 touth = DragAndDrop.MousePositionOnDragArea(_limitPoint1, _limitPoint2);
        //        transform.position = new Vector2(Mathf.Lerp(transform.position.x, touth.x, _movementSpeed * Time.deltaTime),
        //            Mathf.Lerp(transform.position.y, touth.y, _movementSpeed * Time.deltaTime));
        //    }
        //}
        private void OnMouseUp()
        {
            if (ActionManager.CameraMoveAndZoom == false)
                ActionManager.CameraMoveAndZoom = true;
            //else if (_curentClickTime <= _clickTime) _isSelected = true;
        }

        private void Start()
        {

            gameObject.TryGetComponent<Rigidbody2D>(out var rigidbody2D);

            if(rigidbody2D)
               rigidbody2D.bodyType = RigidbodyType2D.Kinematic;

            SetRigidbodyTypeForAllChild(gameObject.transform, RigidbodyType2D.Kinematic);

            //_clickTime = 0.5f;
            //_curentClickTime = 0;
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
                    else throw new System.Exception("Destroy button isn't found!");

                    if (_rotationButton != null)
                    {
                        foreach (var item in _rotationButton)
                        {
                            item.GetComponent<RotatePartButton>().SelectedPart = gameObject;
                        }
                    }
                    else throw new System.Exception("Lost RotationButton");
                }
            }
            catch { throw new System.Exception("DestroyButton lost component: ButtonForDestroyObject"); }

            if (Input.GetMouseButtonDown(0) && !CursorOverThisPart(Input.mousePosition) && !ActionManager.ActionButtonDown)
                _isSelected = false;
        }
        //private void FixedUpdate()
        //{
        //    if (_curentClickTime <= _clickTime) _curentClickTime += Time.deltaTime;
        //}
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
        async public void MoveFollowingTheCursor()
        {
            await Task.Run(() => 
            {
                while(!ClickedPart.MouseButtonUp)
                {
                    Vector2 touth = DragAndDrop.MousePositionOnDragArea(_limitPoint1, _limitPoint2);
                    transform.position = new Vector2(Mathf.Lerp(transform.position.x, touth.x, _movementSpeed * Time.deltaTime),
                        Mathf.Lerp(transform.position.y, touth.y, _movementSpeed * Time.deltaTime));
                }
            });
            
        }
    }
}