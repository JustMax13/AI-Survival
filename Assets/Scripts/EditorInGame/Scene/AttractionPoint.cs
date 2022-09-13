using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor
{
    public class AttractionPoint : MonoBehaviour
    {
        [SerializeField] private AttractionObject _attractionObject;
        private void Start()
        {
            if(_attractionObject == null)
            {
                _attractionObject = new AttractionObject();
                Debug.Log($"AttractionObject is null. Pls add AttractionObject on {gameObject.name}");
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_attractionObject.IsDrag) _attractionObject.WasMouseDown = true;
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (_attractionObject.IsDrag) _attractionObject.WasMouseDown = false;
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            bool itsMagnet;
            if (collision.GetComponent<AttractionPoint>()) itsMagnet = true;
            else itsMagnet = false;

            if (_attractionObject.WasMouseDown && !_attractionObject.IsDrag && itsMagnet) // поменять условия захода ( смотри на доску )
            {
                GameObject magnet = new GameObject();

                magnet.transform.SetParent(_attractionObject.transform);
                magnet.transform.localPosition = gameObject.transform.localPosition;
                magnet.transform.parent = null;

                _attractionObject.gameObject.transform.SetParent(magnet.transform);
                magnet.transform.position = collision.transform.position;
                
                _attractionObject.transform.parent = null;
                Destroy(magnet);
                _attractionObject.WasMouseDown = false;
            }
        }
    }
}