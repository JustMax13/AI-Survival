using UnityEngine;

namespace General
{
    public class MoveAndZoomCamera : MonoBehaviour
    {
        [SerializeField] private GameObject _limitPoint1;
        [SerializeField] private GameObject _limitPoint2;
        private Vector3 touchPosition;

        [SerializeField] private float _sensitivity;

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
                Vector3 targetPosition;
                Vector3 direction = touchPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 CameraPosition = Camera.main.transform.position;

                DragAndDrop.Save2Point(_limitPoint1, _limitPoint2);


                targetPosition = new Vector3(Mathf.Clamp(CameraPosition.x + direction.x, DragAndDrop.MinX,
                    DragAndDrop.MaxX), Mathf.Clamp(CameraPosition.y + direction.y, DragAndDrop.MinY, DragAndDrop.MaxY), CameraPosition.z);
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPosition.x,
               _sensitivity * Time.deltaTime), Mathf.Lerp(transform.position.y, targetPosition.y,
               _sensitivity * Time.deltaTime), CameraPosition.z);
            }
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