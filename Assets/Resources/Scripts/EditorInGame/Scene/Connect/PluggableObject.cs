using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Editor.Moves;

namespace Editor
{
    public class PluggableObject : MonoBehaviour
    {
        [SerializeField] private TypeOfPart _partType = TypeOfPart.AnotherPart;
        [SerializeField] private ConnectPoint[] _connectPointsOnPart = null;

        private const float _timeWasMouseDown = 0.02f;
        private float _currentTimeWasMouseDown;

        private DragAndDropPart _dragAndDropPartComponent;

        public event Action<PluggableObject> DragStart;
        public event Action DragStop;

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

                DragStart?.Invoke(this);
            }
        }
        private void OnMouseUp()
        {
            if (_dragAndDropPartComponent.IsSelected)
            {
                IsDrag = false;
                _currentTimeWasMouseDown = _timeWasMouseDown;

                DragStop?.Invoke();
            }
        }

        private void Start()
        {
            WasMouseDown = false;
            IsDrag = false;

            _currentTimeWasMouseDown = 0;

            DragStart += FullDisconnect;

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
        }

        public static void FullDisconnect(PluggableObject pluggableObject)
        {
            DecrementCounters(pluggableObject);

            RemoveСonnectionFromThisParts(pluggableObject.gameObject);
            ClearConnectPoints(pluggableObject.ConnectPointsOnPart);
            RemoveСonnectionFromOtherParts(pluggableObject.ConnectPointsOnPart);
        }
        private static void ClearConnectPoints(ConnectPoint[] connectPoints)
        {
            foreach (var point in connectPoints)
                point.JointOnPoint.Clear();
        }
        private static void RemoveСonnectionFromThisParts(GameObject gameObject)
        {
            FixedJoint2D[] fixedJoints2D = gameObject.GetComponents<FixedJoint2D>();

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
                    foreach (var fixedJoint2D in point.JointOnPoint.ToList())
                    {
                        if (fixedJoint2D.connectedBody == connectObjectRB)
                        {
                            point.JointOnPoint.Remove(fixedJoint2D);
                            Destroy(fixedJoint2D);

                            point.TryReconnectThisPoint();
                        }
                    }
                }
            }
        }
        private static void DecrementCounters(PluggableObject pluggableObject)
        {
            ConnectPoint[] connectPoints = pluggableObject.ConnectPointsOnPart;

            foreach (var point in connectPoints)
            {
                Collider2D[] colliders2D = Physics2D.OverlapPointAll(point.transform.position);

                foreach (var item in colliders2D)
                {
                    var partCounter = item.GetComponent<PartCounter>();
                    if (partCounter)
                    {
                        partCounter.RemovePart(point);
                        break;
                    }
                }
            }
        }

        public enum TypeOfPart
        {
            AnotherPart,
            Wheel,
            BaseBlock,
        }
    }
}