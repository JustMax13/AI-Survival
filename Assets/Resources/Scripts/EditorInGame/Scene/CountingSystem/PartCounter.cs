using System;
using System.Collections.Generic;
using UnityEngine;

namespace Editor
{
    public class PartCounter : MonoBehaviour
    {
        private int _maxCount = 0;
        private int _currentCount = -1;

        private List<ConnectPoint> _connectedParts = null;

        public int MaxCount { get => _maxCount; set { _maxCount = value; } }
        public int CurrentCount { get => _currentCount; set { _currentCount = value; } }

        private void Update()
        {
            //Debug.Log($"Имя счетчика - {gameObject.name}\nДлина списка: {_connectedParts.Count},_currentCount: {_currentCount}");
            //foreach (var item in _connectedParts)
            //    Debug.Log(item);
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

        private bool TheDictionaryHasConnectionBlocks()
        {
            foreach (var part in _connectedParts)
                if (part.PluggableObj.PartType == PluggableObject.TypeOfPart.BaseBlock)
                    return true;

            return false;
        }
        public ConnectPoint GetFirstBaseBlock()
        {
            foreach (var point in _connectedParts)
            {
                if (point.PluggableObj.PartType == PluggableObject.TypeOfPart.BaseBlock)
                    return point;
            }

            Destroy(gameObject);
            return null;
        }
        public void AddFirstPart(ConnectPoint connectPoint)
        {
            PluggableObject pluggableObject = connectPoint.PluggableObj;

            if (_connectedParts != null)
                throw new Exception($"Деталь {pluggableObject} не може бути додана першою, оскільки вже була додана перша деталь!");
            if (pluggableObject.PartType != PluggableObject.TypeOfPart.BaseBlock)
                throw new Exception($"Деталь {pluggableObject} не є базовим блоком і не може бути додана першою!");

            try { _maxCount = pluggableObject.GetComponent<PartCountValue>().MaxPossibleConnectionToPoint; }
            catch { throw new Exception($"Деталь {pluggableObject} не містить PartCountValue!"); }

            _connectedParts = new List<ConnectPoint>();

            AddPart(connectPoint);
        }
        public void AddPart(ConnectPoint connectPoint)
        {
            _connectedParts.Add(connectPoint);
            _currentCount++;
        }
        public void RemovePart(ConnectPoint connectPoint)
        {
            foreach (var part in _connectedParts)
            {
                if (connectPoint == part)
                {
                    _connectedParts.Remove(part);

                    if (!TheDictionaryHasConnectionBlocks())
                        Destroy(gameObject);

                    _currentCount--;

                    return;
                }
            }

            throw new Exception($"Не знайдено {connectPoint.PluggableObj} у списку підключений деталей!");
        }

    }
}