using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class MoveAndZoomCamera : MonoBehaviour
    {
        [SerializeField]
        private GameObject _limitPoint1;
        [SerializeField]
        private GameObject _limitPoint2;
        private Vector3 touchPosition;
        private Vector3 targetPosition;
        
        private float _minX;
        private float _maxX;
        private float _minY;
        private float _maxY;
        [SerializeField]
        private float _sensitivity;

        [SerializeField]
        private float _minCameraSize;
        [SerializeField]
        private float _maxCameraSize;
        public float MinCameraSize { get; set; }
        public float MaxCameraSize { get; set; }
        [SerializeField]
        private float _zoomSensivity;
        private bool _zoomEnd;

        
        private void Move()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 direction = touchPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector3 CameraPosition = Camera.main.transform.position;
                targetPosition = new Vector3(Mathf.Clamp(CameraPosition.x + direction.x, _minX,
                    _maxX), Mathf.Clamp(CameraPosition.y + direction.y, _minY, _maxY),
                    CameraPosition.z + direction.z);
            }
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPosition.x,
                _sensitivity * Time.deltaTime), Mathf.Lerp(transform.position.y, targetPosition.y,
                _sensitivity * Time.deltaTime), transform.position.z);

        }
        private void Zoom()
        {
            _zoomEnd = false;
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroLastPosition = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOneLastPosition = touchOne.position - touchOne.deltaPosition;

            float distanceTouch = (touchZeroLastPosition - touchOneLastPosition).magnitude;
            float currentDistanceTouch = (touchZero.position - touchOne.position).magnitude;

            float difference = currentDistanceTouch - distanceTouch;

            float EndPosition = Mathf.Clamp(Camera.main.orthographicSize - difference, _minCameraSize, _maxCameraSize);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, EndPosition, _zoomSensivity);

            if(Input.touchCount == 0)
                _zoomEnd = true;
        }
        private void Start()
        {
            _zoomEnd = true;
            Vector3 point1 = _limitPoint1.transform.position;
            Vector3 point2 = _limitPoint2.transform.position;
            if (point1.x > point2.x){ _maxX = point1.x; _minX = point2.x; } 
            else { _maxX = point2.x; _minX = point1.x; }

            if (point1.y > point2.y) { _maxY = point1.y; _minY = point2.y; }
            else { _maxY = point2.y; _minY = point1.y; }
        }
        private void Update()
        {
            if (Input.touchCount == 0)
                _zoomEnd = true;

            if (Input.GetMouseButtonDown(0))
                touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.touchCount == 2)
                Zoom();
            else if (Input.touchCount == 1 && _zoomEnd)
                Move();
        }
    }
}