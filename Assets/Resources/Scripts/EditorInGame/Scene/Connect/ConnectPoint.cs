using System;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;

namespace Editor
{
    public class ConnectPoint : MonoBehaviour
    {
        [SerializeField] private PluggableObject _pluggableObj;

        private Collider2D _findedCollider;
        private List<FixedJoint2D> _fixedJointOnPoint;

        public List<FixedJoint2D> FixedJointOnPoint { get => _fixedJointOnPoint; set { _fixedJointOnPoint = value; } }
        public PluggableObject PluggableObj { get => _pluggableObj; }

        private void OnTriggerStay2D(Collider2D collision)
        {
            _findedCollider = collision;

            if (!_findedCollider.GetComponent<ConnectPoint>())
                return;

            if (_pluggableObj.WasMouseDown && !_pluggableObj.IsDrag)
                CheckMoveAndConnect(_findedCollider);
        }

        private void Start()
        {
            _fixedJointOnPoint = new List<FixedJoint2D>();

            if (_pluggableObj == null)
                throw new Exception($"AttractionObject is null. Pls add AttractionObject on {gameObject.name}");
        }
        private void Update()
        {
            if (_pluggableObj.WasMouseDown && _findedCollider != null)
                OnTriggerStay2D(_findedCollider);
        }

        public List<ConnectPoint> FindConnectedPoints(in Collider2D[] colliders)
        {
            var connectPoints = new List<ConnectPoint>();

            ConnectPoint tepm;
            foreach (var item in colliders)
            {
                tepm = item.GetComponent<ConnectPoint>();

                if (tepm)
                    connectPoints.Add(tepm);
            }

            return connectPoints;
        }
        private void CheckMoveAndConnect(in Collider2D connectPoint)
        {
            GetAttractionObjectFromCollisionParent(connectPoint, out PluggableObject pluggableObject);

            if (CheckConnectionCompatibility(pluggableObject))
            {
                MoveToConnectedObject(connectPoint);

                if (_pluggableObj.PartType == PluggableObject.TypeOfPart.BaseBlock)
                {
                    List<Collider2D> connectedPoints = CheckAnotherConnectedPointsInPoint(connectPoint);

                    if (connectedPoints.Count > 1)
                    {
                        Collider2D collider2DThisPoint = GetComponent<Collider2D>();

                        foreach (var item in connectedPoints)
                            MoveToConnectedObject(collider2DThisPoint, item);

                        MultipleConnect(collider2DThisPoint, connectedPoints);
                    }
                    else Connect(this, connectPoint.GetComponent<ConnectPoint>());
                }
                else Connect(connectPoint.GetComponent<ConnectPoint>());
            }
        }
        private bool CheckConnectionCompatibility(PluggableObject pluggableObject)
        {
            if (_pluggableObj.PartType == PluggableObject.TypeOfPart.BaseBlock
                || pluggableObject.PartType == PluggableObject.TypeOfPart.BaseBlock)
                return true;
            else
                return false;
        }
        private bool ThisPartAlreadyConnected(PluggableObject pluggableObject, ConnectPoint connectPoint)
        {
            var pluggableObjectRB = pluggableObject.GetComponent<Rigidbody2D>();

            foreach (var item in connectPoint.FixedJointOnPoint)
                if (item.connectedBody == pluggableObjectRB)
                    return true;

            return false;
        }
        private List<Collider2D> CheckAnotherConnectedPointsInPoint(in Collider2D connectPoint)
        {
            Vector2 connectPointPosition = connectPoint.transform.position;
            Collider2D[] collidersInPoint = Physics2D.OverlapPointAll(connectPointPosition);

            var allConnectedPointsCollider = new List<Collider2D>();
            Collider2D thisCollider = gameObject.GetComponent<Collider2D>();

            foreach (var item in collidersInPoint)
            {
                if (item.GetComponent<ConnectPoint>() && item != thisCollider)
                    allConnectedPointsCollider.Add(item);
            }

            return allConnectedPointsCollider;
        }
        private void GetAttractionObjectFromCollisionParent(in Collider2D connectPoint, out PluggableObject pluggableObject)
        {
            try
            { pluggableObject = connectPoint.GetComponentInParent<PluggableObject>(); }
            catch
            { throw new Exception($"On the object {connectPoint.GetComponentInParent<Transform>().gameObject} not found script \"AttractionObject\" "); }
        }

        private void MoveToConnectedObject(in Collider2D moveTo, Collider2D movable = null)
        {
            if (!movable)
                movable = gameObject.GetComponent<Collider2D>();

            Transform movableParentTransform = movable.transform.parent;

            GameObject magnet = new GameObject();

            magnet.transform.SetParent(movableParentTransform);
            magnet.transform.localPosition = movable.transform.localPosition;
            magnet.transform.parent = null;

            Transform pastParent = movableParentTransform.parent;

            movableParentTransform.SetParent(magnet.transform);
            magnet.transform.position = moveTo.transform.position;

            movableParentTransform.parent = pastParent;

            Destroy(magnet);
        }

        private void Connect(in Collider2D connectTo, Collider2D toPlug = null)
        {
            ConnectPoint toPlugPoint;

            if (!toPlug)
                toPlugPoint = this;
            else
                toPlugPoint = toPlug.GetComponent<ConnectPoint>();

            var connectToPoint = connectTo.GetComponent<ConnectPoint>();

            Connect(connectToPoint, toPlugPoint);
        }
        private void Connect(in ConnectPoint connectTo, ConnectPoint toPlugPoint = null)
        {
            if (!toPlugPoint)
                toPlugPoint = this;

            if (ThisPartAlreadyConnected(connectTo.PluggableObj, toPlugPoint))
                return;

            if (connectTo.PluggableObj != toPlugPoint._pluggableObj)
            {
                FixedJoint2D jointOnObject = toPlugPoint._pluggableObj.gameObject.AddComponent<FixedJoint2D>();

                jointOnObject.anchor = toPlugPoint.transform.localPosition;
                jointOnObject.connectedBody = connectTo.PluggableObj.GetComponent<Rigidbody2D>();

                toPlugPoint.FixedJointOnPoint.Add(jointOnObject);
            }
        }
        private void MultipleConnect(in Collider2D connectTo, in List<Collider2D> toPlug)
        {
            if (toPlug == null)
                throw new Exception("Не було передано масиву для підключення!");

            var connectToPoint = connectTo.GetComponent<ConnectPoint>();

            var toPlugPoints = new List<ConnectPoint>();

            foreach (var item in toPlug)
                toPlugPoints.Add(item.GetComponent<ConnectPoint>());

            MultipleConnect(connectToPoint, toPlugPoints);
        }
        private void MultipleConnect(in ConnectPoint connectTo, in List<ConnectPoint> toPlugPoints)
        {
            if (toPlugPoints == null)
                throw new Exception("Не було передано масиву для підключення!");

            foreach (var point in toPlugPoints)
                if (ThisPartAlreadyConnected(connectTo.PluggableObj, point))
                    return;

            foreach (var toPlug in toPlugPoints)
            {
                if (connectTo.PluggableObj != toPlug._pluggableObj)
                {
                    FixedJoint2D jointOnObject = toPlug._pluggableObj.gameObject.AddComponent<FixedJoint2D>();

                    jointOnObject.anchor = toPlug.transform.localPosition;
                    jointOnObject.connectedBody = connectTo.PluggableObj.GetComponent<Rigidbody2D>();

                    toPlug.FixedJointOnPoint.Add(jointOnObject);
                }
            }
        }
        //private void TryReconnect()
        //{
        //    // 1) Найти точки для подключения в точке
        //    // Логика если да:
        //    // 2) Если они есть, то проверяем, являются ли те точки точками базового блока.
        //    // 3) Если являются, проверяем, является наша часть базовым блоком
        //    // 4) Если да, проверям, не является ли найденая точка, точкой, расположеной на этом базовом блоке
        //    // 5) Если являются..

        //    Collider2D[] allCollider = Physics2D.OverlapPointAll(this.transform.position);

        //    List<ConnectPoint> connectPoints = FindConnectedPoints(allCollider);
        //    // недописано
        //    if (connectPoints.Count > 1) // більше 1го, бо враховується ця точка, на якій працює метод
        //    {
        //        foreach (var point in connectPoints)
        //        {
        //            if (point.PluggableObj.PartType == PluggableObject.TypeOfPart.BaseBlock)
        //            {
        //                if (_pluggableObj.PartType == PluggableObject.TypeOfPart.BaseBlock)
        //                {
        //                    bool pointOnThisPluggableObject = false;

        //                    foreach (var pointOnThisPluggableObj in _pluggableObj.ConnectPointsOnPart)
        //                        if (pointOnThisPluggableObj == point)
        //                        {
        //                            pointOnThisPluggableObject = true;
        //                            break;
        //                        }

        //                    if (pointOnThisPluggableObject)
        //                        break;
        //                    else
        //                    {

        //                    }
        //                }
        //                else
        //                {
        //                    MoveToConnectedObject(point.GetComponent<Collider2D>());

        //                }
        //            }
        //            else continue;
        //        }
        //    }
        //    else return;
        //}
    }
}