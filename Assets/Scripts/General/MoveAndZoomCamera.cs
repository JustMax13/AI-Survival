using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class MoveAndZoomCamera : MonoBehaviour
    {
        [SerializeField] private GameObject limitPoint1;
        [SerializeField] private GameObject limitPoint2;
        private Vector3 touchPosition;
        private Vector3 targetPosition;

        private float minX;
        private float maxX;
        private float minY;
        private float maxY;
        [SerializeField] private float sensitivity;

        [SerializeField] private float _minCameraSize;
        [SerializeField] private float _maxCameraSize;
        public float MinCameraSize { get; set; }
        public float MaxCameraSize { get; set; }
        [SerializeField] private float zoomSensivity;
        private bool zoomEnd;

        private bool executionCondition;


        private void Move()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 direction = touchPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector3 CameraPosition = Camera.main.transform.position;
                targetPosition = new Vector3(Mathf.Clamp(CameraPosition.x + direction.x, minX,
                    maxX), Mathf.Clamp(CameraPosition.y + direction.y, minY, maxY),
                    CameraPosition.z + direction.z);
            }
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPosition.x,
                sensitivity * Time.deltaTime), Mathf.Lerp(transform.position.y, targetPosition.y,
                sensitivity * Time.deltaTime), transform.position.z);
        }
        private void Zoom()
        {
            zoomEnd = false;
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroLastPosition = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOneLastPosition = touchOne.position - touchOne.deltaPosition;

            float distanceTouch = (touchZeroLastPosition - touchOneLastPosition).magnitude;
            float currentDistanceTouch = (touchZero.position - touchOne.position).magnitude;

            float difference = currentDistanceTouch - distanceTouch;

            float EndPosition = Mathf.Clamp(Camera.main.orthographicSize - difference, _minCameraSize, _maxCameraSize);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, EndPosition, zoomSensivity * 0.01f);

            if (Input.touchCount == 0)
                zoomEnd = true;
        }
        private void Start()
        {
            executionCondition = Editor.ActionManager.CameraMoveAndZoom;
            zoomEnd = true;
            Vector3 point1 = limitPoint1.transform.position;
            Vector3 point2 = limitPoint2.transform.position;
            if (point1.x > point2.x) { maxX = point1.x; minX = point2.x; }
            else { maxX = point2.x; minX = point1.x; }

            if (point1.y > point2.y) { maxY = point1.y; minY = point2.y; }
            else { maxY = point2.y; minY = point1.y; }
        }
        private void Update()
        {
            if (Input.touchCount == 0)
                zoomEnd = true;

            if (Input.GetMouseButtonDown(0))
                touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.touchCount == 2 && executionCondition)
                Zoom();
            else if (Input.touchCount == 1 && zoomEnd && executionCondition)
                Move();
            executionCondition = Editor.ActionManager.CameraMoveAndZoom;
        }
    }
}