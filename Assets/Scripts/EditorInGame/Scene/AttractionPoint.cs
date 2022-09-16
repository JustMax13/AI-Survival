using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using static UnityEditor.Progress;

namespace Editor
{
    public class AttractionPoint : MonoBehaviour
    {
        //private const int MaxCountConnectedObject = 2;
        //private int _currentCountConnectedObject;
        //private AttractionPoint[] _connectedPoints;
        [SerializeField] private AttractionObject _attractionObj;
        private Collider2D _findedCollider;
        //public int CurrentCountConnectedObject 
        //{
        //    get => _currentCountConnectedObject;
        //    set
        //    {
        //        if (value < 0) value = 0;
        //        _currentCountConnectedObject = value;
        //    }
        //}
        public AttractionObject AttractionObj  { get => _attractionObj; }
        private void Start()
        {
            if (_attractionObj == null)
            {
                _attractionObj = new AttractionObject();
                Debug.Log($"AttractionObject is null. Pls add AttractionObject on {gameObject.name}");
            }
            //_currentCountConnectedObject = 0;
            //_connectedPoints = new AttractionPoint[MaxCountConnectedObject];
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            _findedCollider = collision;

            bool itsMagnet;

            if (collision.GetComponent<AttractionPoint>()) itsMagnet = true;
            else itsMagnet = false;

            if (_attractionObj.WasMouseDown && !_attractionObj.IsDrag && itsMagnet)
            {
                MovingObject(collision);
                Connect(collision);
                //_currentCountConnectedObject = collision.GetComponent<AttractionPoint>().CurrentCountConnectedObject; // бо у об'єкта який пересувається немає підключених деталей
                //if (_currentCountConnectedObject < MaxCountConnectedObject)
                //{
                //    _connectedPoints[_currentCountConnectedObject] = collision.GetComponent<AttractionPoint>();
                //    _currentCountConnectedObject++;
                //    collision.GetComponent<AttractionPoint>().CurrentCountConnectedObject++;
                //} 
            }
        }
        private void MovingObject(Collider2D collision)
        {
            GameObject magnet = new GameObject();

            magnet.transform.SetParent(_attractionObj.transform);
            magnet.transform.localPosition = gameObject.transform.localPosition;
            magnet.transform.parent = null;

            _attractionObj.gameObject.transform.SetParent(magnet.transform);
            magnet.transform.position = collision.transform.position;

            _attractionObj.transform.parent = null;
            Destroy(magnet);
        }
        private void Connect(Collider2D collision)
        {
            if (collision.GetComponent<AttractionPoint>().AttractionObj != _attractionObj)
            {
                FixedJoint2D jointOnObject = _attractionObj.gameObject.AddComponent<FixedJoint2D>();
                jointOnObject.anchor = gameObject.transform.localPosition;

                jointOnObject.connectedBody = collision.GetComponent<AttractionPoint>().AttractionObj
                    .gameObject.GetComponent<Rigidbody2D>();
            }
            // прописати силу розриву деталей
        }
        private void Disconnect()
        {
            if (_attractionObj.GetComponent<FixedJoint2D>())
            {
                foreach (var item in _attractionObj.GetComponents<FixedJoint2D>()) Destroy(item);

                //foreach (var item in _connectedPoints) 
                //{
                //    if(item != null)
                //        item.CurrentCountConnectedObject--;
                //}
                //_currentCountConnectedObject = 0;
            }
        }
        private void Update()
        {
            if (_attractionObj.IsDrag) Disconnect();
            else if (_attractionObj.WasMouseDown && _findedCollider != null) 
                OnTriggerStay2D(_findedCollider);
        }
    }
}
// поменять как-то CurrentCountConnectedObject: оно не правильно считает
// переписать все, что касается ограничений, возможно перреписать и то, что с FixedJoint
// Disconected сделать для левого и правого болта
// Сделать сначала схему алгоритма на доске, а потом уже писать код.