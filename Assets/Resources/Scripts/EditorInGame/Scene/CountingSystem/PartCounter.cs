using System;
using System.Collections.Generic;
using UnityEngine;
using General.PartOfBots;
using System.Reflection;
using UnityEditor;

namespace Editor
{
    public class PartCounter : MonoBehaviour
    {
        private int _maxCount = 0;
        private int _currentCount = -1;
        private static GameObject _prefabPartCounter = null;

        private List<ConnectPoint> _connectedPoint = null;

        public int MaxCount { get => _maxCount; }
        public int CurrentCount { get => _currentCount; set { _currentCount = value; } }

        //private void Update()
        //{
        //    Debug.Log($"Имя счетчика - {gameObject.name}\nДлина списка: {_connectedPoint.Count},_currentCount: {_currentCount}");
        //    //foreach (var item in _connectedPoint)
        //    //    Debug.Log(item);
        //}

        private bool TheDictionaryHasConnectionBlocks()
        {
            foreach (var part in _connectedPoint)
                if (part.PluggableObj.PartType == TypeOfPart.BaseBlock)
                    return true;

            return false;
        }
        public static PartCounter CreatePartCounterObject(in ConnectPoint OnConnectPoint)
        {
            if(_prefabPartCounter == null)
                _prefabPartCounter = Resources.Load<GameObject>("Prefab/Editor/ObjectWithIcon/PartCounter");

            var partCounter = Instantiate(_prefabPartCounter, OnConnectPoint.transform.position, Quaternion.identity);
            partCounter.transform.parent = OnConnectPoint.PluggableObj.transform.parent;

            return partCounter.GetComponent<PartCounter>();
        }
        public static PartCounter FindCounterObject(Vector2 position)
        {
            Collider2D[] colliders = Physics2D.OverlapPointAll(position);

            foreach (var item in colliders)
            {
                var partCounter = item.GetComponent<PartCounter>();

                if (partCounter)
                    return partCounter;
            }

            return null;
        }
        public bool IsItemOnList(ConnectPoint connectPoint)
        {
            if(_connectedPoint != null)
                foreach (var item in _connectedPoint)
                    if (item == connectPoint)
                        return true;

            return false;
        }
        public bool ListIsEmpty()
        {
            if (_connectedPoint == null)
                return true;
            else
                return false;
        }
        public ConnectPoint GetFirstBaseBlock()
        {
            foreach (var point in _connectedPoint)
            {
                if (point.PluggableObj.PartType == TypeOfPart.BaseBlock)
                    return point;
            }

            Destroy(gameObject);
            return null;
        }
        public void AddFirstPoint(ConnectPoint connectPoint)
        {
            PluggableObject pluggableObject = connectPoint.PluggableObj;

            if (_connectedPoint != null)
                throw new Exception($"Деталь {pluggableObject} не може бути додана першою, оскільки вже була додана перша деталь!");
            if (pluggableObject.PartType != TypeOfPart.BaseBlock)
                throw new Exception($"Деталь {pluggableObject} не є базовим блоком і не може бути додана першою!");

            try { _maxCount = pluggableObject.GetComponent<ConnectToPartValue>().MaxPossibleConnectionToPoint; }
            catch { throw new Exception($"Деталь {pluggableObject} не містить PartCountValue!"); }

            _connectedPoint = new List<ConnectPoint>();

            AddPoint(connectPoint);
        }
        public void AddPoint(ConnectPoint connectPoint)
        {
            _connectedPoint.Add(connectPoint);
            _currentCount++;
        }
        public void AddPointForLoad(ConnectPoint connectPoint)
        {
            if (ListIsEmpty())
                _connectedPoint = new List<ConnectPoint>();
            AddPoint(connectPoint);

            if (_maxCount == 0)
            {
                foreach (var item in connectPoint.PluggableObj.GetComponents<AnchoredJoint2D>())
                    if (item.anchor == (Vector2)connectPoint.transform.localPosition)
                    {
                        if(item.connectedBody)
                        {
                            PluggableObject pluggableObject = item.connectedBody.GetComponent<PluggableObject>();
                            _maxCount = pluggableObject.GetComponent<ConnectToPartValue>().MaxPossibleConnectionToPoint;

                            foreach (var connectedPoint in pluggableObject.ConnectPointsOnPart)
                                if (item.connectedAnchor == (Vector2)connectedPoint.transform.localPosition)
                                {
                                    AddPoint(connectedPoint);
                                    break;
                                }
                                    
                            break;
                        }
                    }
                
            }
        }
        public void RemovePoint(ConnectPoint connectPoint)
        {
            foreach (var part in _connectedPoint)
            {
                if (connectPoint == part)
                {
                    _connectedPoint.Remove(part);

                    if (!TheDictionaryHasConnectionBlocks())
                        Destroy(gameObject);

                    _currentCount--;

                    return;
                }
            }

            Debug.LogWarning($"Не знайдено {connectPoint.PluggableObj} у списку підключений деталей!");
        }
    }
}