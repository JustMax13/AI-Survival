using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Editor
{
    public class AttractionPoint : MonoBehaviour
    {
        [SerializeField] private AttractionObject _attractionObj;
        public AttractionObject AttractionObj  { get => _attractionObj; }
        private void Start()
        {
            if (_attractionObj == null)
            {
                _attractionObj = new AttractionObject();
                Debug.Log($"AttractionObject is null. Pls add AttractionObject on {gameObject.name}");
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_attractionObj.IsDrag) _attractionObj.WasMouseDown = true;
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (_attractionObj.IsDrag) _attractionObj.WasMouseDown = false;
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            bool itsMagnet;

            if (collision.GetComponent<AttractionPoint>()) itsMagnet = true;
            else itsMagnet = false;

            if (_attractionObj.WasMouseDown && !_attractionObj.IsDrag && itsMagnet)
            {
                GameObject magnet = new GameObject();

                magnet.transform.SetParent(_attractionObj.transform);
                magnet.transform.localPosition = gameObject.transform.localPosition;
                magnet.transform.parent = null;

                _attractionObj.gameObject.transform.SetParent(magnet.transform);
                magnet.transform.position = collision.transform.position;
                
                _attractionObj.transform.parent = null;
                Destroy(magnet);

                Connect(collision);

                _attractionObj.WasMouseDown = false;
            }
        }
        private void Connect(Collider2D collision)
        {
            FixedJoint2D jointOnObject = _attractionObj.gameObject.AddComponent<FixedJoint2D>();
            jointOnObject.anchor = gameObject.transform.localPosition;

            jointOnObject.connectedBody = collision.GetComponent<AttractionPoint>().AttractionObj
                .gameObject.GetComponent<Rigidbody2D>();
            // прописать силу разрыва деталей
        }
        private void Disconnect()
        {
            foreach (var item in _attractionObj.GetComponents<FixedJoint2D>()) Destroy(item);
        }
        private void Update()
        {
            if (_attractionObj.IsDrag) Disconnect();
        }
    }
}