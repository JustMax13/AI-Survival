using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Editor.Interface
{
    public class RotatePartButton : Button
    {
        private bool _leftRotate;

        private const float _timeToEndClick = 0.2f;
        private float _currentTimeToEndClick;

        private bool _buttonDown;

        private bool _forceRotatingStart;
        private float _minSpeed;
        private float _maxSpeed;
        private float _currentSpeed;
        private float _speedStep;

        private float _acceleration;
        private bool _useForce;
        private float _angle;
        private float _forceAngle;
        private float _maxForceAngle;

        private GameObject _selectedPart;
        public GameObject SelectedPart
        {
            get => _selectedPart;
            set { _selectedPart = value; }
        }

        public void RotatingRight() => _leftRotate = false;
        public void RotatingLeft() => _leftRotate = true;
        public void OneTurnLeft()
        {
            if (_selectedPart)
            {
                if (_useForce) _selectedPart.transform.Rotate(Vector3.forward, _forceAngle);
                else _selectedPart.transform.Rotate(Vector3.forward, _angle);
                _selectedPart.GetComponent<PluggableObject>().IsDrag = true;
            }
        }
        public void OneTurnRight()
        {
            if (_selectedPart)
            {
                if (_useForce) _selectedPart.transform.Rotate(Vector3.forward, -_forceAngle);
                else _selectedPart.transform.Rotate(Vector3.forward, -_angle);
                _selectedPart.GetComponent<PluggableObject>().IsDrag = true;
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            OnPointerClick(eventData);

            if (_selectedPart)
            {
                if (_selectedPart.GetComponent<DragAndDropPart>().IsSelected)
                {
                    _currentTimeToEndClick = _timeToEndClick;
                    _buttonDown = true;

                    if (_leftRotate) OneTurnLeft();
                    else OneTurnRight();

                    ActionManager.CameraMoveAndZoom = false;
                    ActionManager.ActionButtonDown = true;
                }
            }
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            _buttonDown = false;
            _useForce = false;

            ActionManager.ActionButtonDown = false;
            ActionManager.CameraMoveAndZoom = true;
        }
        protected override void Start()
        {
            base.Start();

            _currentTimeToEndClick = 0;

            _buttonDown = false;

            _forceRotatingStart = false;
            _minSpeed = 0;
            _maxSpeed = 1f;
            _currentSpeed = 0;
            _speedStep = _maxSpeed / 50;

            _acceleration = 2f;
            _angle = 1;
            _forceAngle = _angle * _acceleration;
            _maxForceAngle = _angle * _acceleration;

            _selectedPart = null;
        }
        private void FixedUpdate()
        {
            if (_selectedPart != null)
            {
                if (!_selectedPart.GetComponent<DragAndDropPart>().IsSelected)
                    gameObject.GetComponent<Button>().interactable = false;
                else gameObject.GetComponent<Button>().interactable = true;
            }
            else gameObject.GetComponent<Button>().interactable = false;

            if (_selectedPart)
            {
                if (_selectedPart.GetComponent<DragAndDropPart>().IsSelected)
                {
                    if (_currentTimeToEndClick > 0)
                    {
                        _currentTimeToEndClick -= Time.deltaTime;
                        _forceRotatingStart = true;
                        _forceAngle = _angle * _acceleration;
                    }
                    else if (_buttonDown)
                    {
                        if (_forceRotatingStart)
                        {
                            _currentSpeed = _minSpeed;
                            _forceRotatingStart = false;
                        }
                        _useForce = true;

                        if (_currentSpeed < _maxSpeed)
                        {
                            if (_forceAngle < _maxForceAngle)
                            {
                                _forceAngle += _forceAngle * _currentSpeed;
                                _currentSpeed += _speedStep;
                            }
                            else _forceAngle = _maxForceAngle;
                        } else _currentSpeed = _maxSpeed;   
                    }

                    if (_useForce)
                    {
                        if (_leftRotate) OneTurnLeft();
                        else OneTurnRight();
                    }
                }
            }
        }
    }
}