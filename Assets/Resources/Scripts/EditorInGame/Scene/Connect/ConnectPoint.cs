using System;
using System.Collections.Generic;
using UnityEngine;

namespace Editor
{
    public class ConnectPoint : MonoBehaviour
    {
        [SerializeField] private PluggableObject _pluggableObj;

        private Collider2D _findedCollider;
        private List<Joint2D> _jointOnPoint;
        private PartCounter _partCounter;

        public List<Joint2D> JointOnPoint { get => _jointOnPoint; set { _jointOnPoint = value; } }
        public PluggableObject PluggableObj { get => _pluggableObj; }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision)
            {
                if (collision == _findedCollider)
                    return;

                if (!collision.GetComponent<PartCounter>())
                    _findedCollider = collision;
            }
        }

        private void Start()
        {
            _jointOnPoint = new List<Joint2D>();

            if (_pluggableObj == null)
                throw new Exception($"PluggableObject is null. Pls add PluggableObject on {gameObject.name}");

            _pluggableObj.DragStop += DragIsEnd;
            _pluggableObj.DragStop += EventManager.OffCurrentAction;
        }
        private void Update()
        {
            if (_pluggableObj.WasMouseDown && _findedCollider != null)
                OnTriggerStay2D(_findedCollider);
        }

        private void DragIsEnd()
        {
            if (!_findedCollider)
                return;

            var findedPoint = _findedCollider.GetComponent<ConnectPoint>();
            if (!findedPoint)
            {
                Collider2D[] collider2D = Physics2D.OverlapPointAll(transform.position);

                List<ConnectPoint> connectPoint = FindConnectedPoints(collider2D);

                connectPoint.Remove(this);

                if (connectPoint.Count == 0)
                    return;

                CheckMoveAndConnect(connectPoint[0]);
            }
            else
                CheckMoveAndConnect(findedPoint);

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
        private void CheckMoveAndConnect(in ConnectPoint connectPoint)
        {
            _partCounter = PartCounter.FindCounterObject(connectPoint.transform.position);

            if (!(CheckConnectionCompatibility(connectPoint.PluggableObj) && !PointAlreadyConnected(connectPoint, this)) && !_partCounter)
                return;

            if (!_partCounter)
            {
                var gameObject = new GameObject("PartCounter");

                var collider = gameObject.AddComponent<CircleCollider2D>();
                _partCounter = gameObject.AddComponent<PartCounter>();

                collider.radius = 0.01f;

                Transform parentOfConnectPoint = connectPoint.transform.parent;
                connectPoint.transform.parent = null;

                gameObject.transform.position = connectPoint.transform.position;

                connectPoint.transform.parent = parentOfConnectPoint;
                gameObject.transform.parent = _pluggableObj.transform.parent;
            }
            else if (_partCounter.CurrentCount >= _partCounter.MaxCount)
                return;


            MoveToCoint(connectPoint.transform);

            List<ConnectPoint> connectedPoints = FindConnectedPoints(connectPoint.transform.position);

            var baseBlocks = new List<ConnectPoint>();

            foreach (var point in connectedPoints)
                if (point.PluggableObj.PartType == PluggableObject.TypeOfPart.BaseBlock)
                    baseBlocks.Add(point);

            if (_pluggableObj.PartType == PluggableObject.TypeOfPart.BaseBlock)
            {
                switch (baseBlocks.Count)
                {
                    case 0:
                        {
                            if (connectedPoints.Count > 1)
                            {
                                foreach (var point in connectedPoints)
                                    MoveToCoint(connectPoint.transform, point.transform);

                                MultipleConnect(this, connectedPoints, _partCounter);
                            }
                            else
                            {
                                Connect(this, connectPoint);

                                if (_partCounter.MaxCount == 0)
                                {
                                    _partCounter.AddFirstPart(this);
                                    _partCounter.AddPart(connectPoint);
                                }
                                else
                                    _partCounter.AddPart(connectPoint);
                            }
                            break;
                        }
                    case 1:
                        {
                            Connect(baseBlocks[0], this);

                            if (_partCounter.MaxCount == 0)
                            {
                                _partCounter.AddFirstPart(baseBlocks[0]);
                                _partCounter.AddPart(this);
                            }
                            else
                                _partCounter.AddPart(this);
                            break;
                        }
                    default: // коли знайдено більше одного БК
                        {
                            Connect(_partCounter.GetFirstBaseBlock(), this);
                            _partCounter.AddPart(this);

                            break;
                        }
                }
            }
            else
            {
                switch (baseBlocks.Count)
                {
                    case 1:
                        {
                            Connect(baseBlocks[0], this);

                            if (_partCounter.MaxCount == 0)
                            {
                                _partCounter.AddFirstPart(baseBlocks[0]);
                                _partCounter.AddPart(this);
                            }
                            else
                                _partCounter.AddPart(this);
                            break;
                        }
                    default: // коли знайдено більше одного БК
                        {
                            Connect(_partCounter.GetFirstBaseBlock(), this);
                            _partCounter.AddPart(this);

                            break;
                        }
                }
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
        private bool PointAlreadyConnected(ConnectPoint connectPoint1, ConnectPoint connectPoint2)
        {
            var pluggableObject1RB = connectPoint1.PluggableObj.GetComponent<Rigidbody2D>();

            foreach (var item in connectPoint2.JointOnPoint)
                if (item.connectedBody == pluggableObject1RB)
                    return true;

            var pluggableObject2RB = connectPoint2.PluggableObj.GetComponent<Rigidbody2D>();

            foreach (var item in connectPoint1.JointOnPoint)
                if (item.connectedBody == pluggableObject2RB)
                    return true;

            return false;
        }
        private List<ConnectPoint> FindConnectedPoints(in Vector2 point)
        {
            Collider2D[] collidersInPoint = Physics2D.OverlapPointAll(point);

            var connectedPoints = new List<ConnectPoint>();

            foreach (var item in collidersInPoint)
            {
                var connectPoint = item.GetComponent<ConnectPoint>();
                if (connectPoint && connectPoint != this)
                    connectedPoints.Add(connectPoint);
            }

            return connectedPoints;
        }
        private void GetConnectPoint(in Collider2D connectPointCollider, out ConnectPoint connectPoint)
        {
            try
            { connectPoint = connectPointCollider.GetComponent<ConnectPoint>(); }
            catch
            { throw new Exception($"On the object {connectPointCollider.GetComponentInParent<Transform>().gameObject} not found script \"ConnectPoint\" "); }
        }
        private void TryGetConnectPoint(in Collider2D connectPointCollider, out ConnectPoint connectPoint)
        {
            try
            { connectPoint = connectPointCollider.GetComponent<ConnectPoint>(); }
            catch
            { connectPoint = null; }
        }
        private void MoveToCoint(in Transform moveTo, Transform movable = null)
        {
            if (!movable)
                movable = transform;

            Vector3 distance = moveTo.position - movable.position;
            movable.parent.position += distance;
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
        private void Connect(in ConnectPoint connectTo, ConnectPoint toPlug = null)
        {
            if (!toPlug)
                toPlug = this;


            switch (toPlug.PluggableObj.PartType)
            {
                case PluggableObject.TypeOfPart.AnotherPart:
                    {
                        if (connectTo.PluggableObj != toPlug._pluggableObj)
                        {
                            FixedJoint2D jointOnObject = toPlug._pluggableObj.gameObject.AddComponent<FixedJoint2D>();

                            jointOnObject.connectedBody = connectTo.PluggableObj.GetComponent<Rigidbody2D>();
                            jointOnObject.anchor = toPlug.transform.localPosition;
                            
                            toPlug.JointOnPoint.Add(jointOnObject);
                        }

                        break;
                    };
                case PluggableObject.TypeOfPart.BaseBlock:
                    {
                        if (connectTo.PluggableObj != toPlug._pluggableObj)
                        {
                            FixedJoint2D jointOnObject = toPlug._pluggableObj.gameObject.AddComponent<FixedJoint2D>();

                            jointOnObject.connectedBody = connectTo.PluggableObj.GetComponent<Rigidbody2D>();
                            jointOnObject.anchor = toPlug.transform.localPosition;

                            toPlug.JointOnPoint.Add(jointOnObject);
                        }

                        break;
                    };
                case PluggableObject.TypeOfPart.Wheel:
                    {
                        if (connectTo.PluggableObj != toPlug._pluggableObj)
                        {
                            WheelJoint2D wheelJointOnObject = toPlug._pluggableObj.gameObject.AddComponent<WheelJoint2D>();

                            wheelJointOnObject.connectedBody = connectTo.PluggableObj.GetComponent<Rigidbody2D>();

                            wheelJointOnObject.connectedAnchor += (Vector2)connectTo.transform.localPosition;;

                            toPlug.JointOnPoint.Add(wheelJointOnObject);
                        }

                        break;
                    };
            }

            
        }
        private void MultipleConnect(in Collider2D connectTo, in List<Collider2D> toPlug, PartCounter partCounter)
        {
            if (toPlug == null)
                throw new Exception("Не було передано масиву для підключення!");

            var connectToPoint = connectTo.GetComponent<ConnectPoint>();

            var toPlugPoints = new List<ConnectPoint>();

            foreach (var item in toPlug)
                toPlugPoints.Add(item.GetComponent<ConnectPoint>());

            MultipleConnect(connectToPoint, toPlugPoints, partCounter);
        }
        private void MultipleConnect(in ConnectPoint connectTo, in List<ConnectPoint> toPlug, PartCounter partCounter)
        {
            if (toPlug == null)
                throw new Exception("Не було передано масиву для підключення!");

            foreach (var point in toPlug)
                if (PointAlreadyConnected(connectTo, point))
                    toPlug.Remove(point);

            foreach (var toPlugPoint in toPlug)
            {
                if (partCounter.CurrentCount >= partCounter.MaxCount)
                    break;

                Connect(connectTo, toPlugPoint);

                if (partCounter.MaxCount == 0)
                {
                    partCounter.AddFirstPart(connectTo);
                    partCounter.AddPart(toPlugPoint);
                }
                else
                    partCounter.AddPart(toPlugPoint);
            }
        }

        public void TryReconnectThisPoint()
        {
            if (_partCounter)
            {
                ConnectPoint baseBlock = _partCounter.GetFirstBaseBlock();

                if (!baseBlock)
                    return;
                if (baseBlock != this)
                    Connect(baseBlock, this);
            }
        }
    }
}