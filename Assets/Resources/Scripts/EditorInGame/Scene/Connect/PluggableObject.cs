using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Editor
{
    public class PluggableObject : MonoBehaviour
    {
        [SerializeField] private TypeOfPart _partType = TypeOfPart.AnotherPart;
        [SerializeField] private ConnectPoint[] _connectPointsOnPart = null;

        private bool _removeAllDone;
        private const float _timeWasMouseDown = 0.02f;
        private float _currentTimeWasMouseDown;

        private DragAndDropPart _dragAndDropPartComponent;

        public TypeOfPart PartType { get => _partType; }
        public bool WasMouseDown { get; set; }
        public bool IsDrag { get; set; }
        public ConnectPoint[] ConnectPointsOnPart { get => _connectPointsOnPart; set { _connectPointsOnPart = value; } }

        private void OnMouseDown()
        {
            if (_dragAndDropPartComponent.IsSelected)
            {
                IsDrag = true;
                WasMouseDown = true;
            }
        }
        private void OnMouseUp()
        {
            if (_dragAndDropPartComponent.IsSelected)
            {
                IsDrag = false;
                _currentTimeWasMouseDown = _timeWasMouseDown;
            }
        }

        private void Start()
        {
            _removeAllDone = false;
            WasMouseDown = false;
            IsDrag = false;

            _currentTimeWasMouseDown = 0;

            try { _dragAndDropPartComponent = gameObject.GetComponent<DragAndDropPart>(); }
            catch { throw new System.Exception($"{gameObject.name} lost DragAndDropPart script."); }

            if (_connectPointsOnPart == null)
                throw new Exception($"У деталі не може не бути точок підключення! Деталь: {gameObject}");
        }
        private void FixedUpdate()
        {
            if (_currentTimeWasMouseDown > 0)
                _currentTimeWasMouseDown -= Time.deltaTime;
            else if (WasMouseDown && !IsDrag)
                WasMouseDown = false;

            if (IsDrag && !_removeAllDone)
            {
                FullDisconnect(this);

                _removeAllDone = true;
            }
            else if (!IsDrag)
                _removeAllDone = false;
        }
        public static void FullDisconnect(PluggableObject pluggableObject)
        {
            RemoveСonnectionFromThisParts(pluggableObject.gameObject);
            ClearConnectPoints(pluggableObject.ConnectPointsOnPart);
            RemoveСonnectionFromOtherParts(pluggableObject.ConnectPointsOnPart);
        }
        private static void RemoveСonnectionFromThisParts(GameObject gameObject)
        {
            FixedJoint2D[] fixedJoints2D;
            fixedJoints2D = gameObject.GetComponents<FixedJoint2D>();


            if (fixedJoints2D.Length != 0)
            {
                foreach (var item in fixedJoints2D)
                    Destroy(item);
            }
        }
        private static void RemoveСonnectionFromOtherParts(ConnectPoint[] connectPoints)
        {
            foreach (var connectPoint in connectPoints)
            {
                Collider2D[] allCollider = Physics2D.OverlapPointAll(connectPoint.transform.position);

                List<ConnectPoint> connectPointsList = connectPoint.FindConnectedPoints(allCollider);

                connectPointsList.Remove(connectPoint);

                Rigidbody2D connectObjectRB = connectPoint.PluggableObj.GetComponent<Rigidbody2D>();

                foreach (var point in connectPointsList)
                {
                    foreach (var fixedJoint2D in point.FixedJointOnPoint.ToList())
                    {
                        if (fixedJoint2D.connectedBody == connectObjectRB)
                        {
                            point.FixedJointOnPoint.Remove(fixedJoint2D);
                            Destroy(fixedJoint2D);
                        }
                    }
                }
            }
        }
        private static void ClearConnectPoints(ConnectPoint[] connectPoints)
        {
            foreach (var point in connectPoints)
                point.FixedJointOnPoint.Clear();
        }
        
        public enum TypeOfPart
        {
            AnotherPart,
            Wheel,
            BaseBlock,
        }
    }
}