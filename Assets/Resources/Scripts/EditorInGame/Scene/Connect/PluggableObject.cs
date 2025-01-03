using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Editor.Moves;
using General.PartOfBots;

namespace Editor
{
    public class PluggableObject : MonoBehaviour
    {
        [SerializeField] private TypeOfPart _partType;
        [SerializeField] private ConnectPoint[] _connectPointsOnPart = null;

        private const float _timeWasMouseDown = 0.02f;
        private float _currentTimeWasMouseDown;

        private DragAndDropPart _dragAndDropPartComponent;

        public event Action<PluggableObject> DragStart;
        public event Action DragStop;

        public TypeOfPart PartType { get => _partType; }
        public bool WasMouseDown { get; set; }
        public bool IsDrag { get; set; }
        public bool IsSelected
        {
            get => _dragAndDropPartComponent.IsSelected;
            set => _dragAndDropPartComponent.IsSelected = value;
        }
        public ConnectPoint[] ConnectPointsOnPart 
        {
            get => _connectPointsOnPart; 
            set => _connectPointsOnPart = value; 
        }
        // Вместо OnMouseDown и OnMouseUp нужно использовать что-то другое, так 
        // как не всегда вызывается события, что и сбиравет счетчик с счета

        //private void OnMouseDown()
        //{
        //    if (IsSelected)
        //    {
        //        IsDrag = true;
        //        WasMouseDown = true;

        //        DragStart?.Invoke(this);
        //    }
        //}
        //private void OnMouseUp()
        //{
        //    if (IsSelected)
        //    {
        //        IsDrag = false;
        //        _currentTimeWasMouseDown = _timeWasMouseDown;

        //        DragStop?.Invoke();
        //    }
        //}

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

        public static void FullDisconnect(PluggableObject pluggableObject) // не всегда заходит в метод ( событие )
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
            AnchoredJoint2D[] anchoredJoints2D = gameObject.GetComponents<AnchoredJoint2D>();

            if (anchoredJoints2D.Length != 0)
            {
                foreach (var item in anchoredJoints2D)
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
                    foreach (var anchoredJoint2D in point.JointOnPoint.ToList())
                    {
                        if (anchoredJoint2D.connectedBody == connectObjectRB)
                        {
                            point.JointOnPoint.Remove(anchoredJoint2D);
                            Destroy(anchoredJoint2D);

                            point.TryReconnectThisPoint();
                        }
                    }
                }
            }
        }
        private static void DecrementCounters(PluggableObject pluggableObject) // не всегда заходит в метод
        {
            ConnectPoint[] connectPoints = pluggableObject.ConnectPointsOnPart;

            foreach (var point in connectPoints)
            {
                Collider2D[] colliders2D = Physics2D.OverlapPointAll(point.transform.position);

                foreach (var item in colliders2D) // не всегда заходит в цыкл
                {
                    var partCounter = item.GetComponent<PartCounter>();
                    if (partCounter)
                    {
                        partCounter.RemovePoint(point); // не всегда вызывается
                        break;
                    }
                }
            }
        }

        public void MouseDown()
        {
            //Debug.Log($"Mouse Down - {gameObject}");
            IsDrag = true;
            WasMouseDown = true;

            DragStart?.Invoke(this);
        }

        public void MouseUp()
        {
            //Debug.Log($"Mouse Up - {gameObject}");
            IsDrag = false;
            _currentTimeWasMouseDown = _timeWasMouseDown;

            DragStop?.Invoke();
        }
    }
}