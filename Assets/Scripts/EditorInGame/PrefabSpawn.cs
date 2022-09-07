using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Editor
{
    public class PrefabSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject _limitPoint1;
        [SerializeField] private GameObject _limitPoint2;
        [SerializeField] private float _movementSharpness;

        private float _minX;
        private float _maxX;
        private float _minY;
        private float _maxY;
        private GameObject _content;
        private GameObject _spawnObject;
        private GameObject _objectOnScenes;
        private bool _spawnPermission;
        private bool _ObjectIsntSpawn;

        private void SpawnPrefab()
        {
            Vector2 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            mousePosition = new Vector2(Mathf.Clamp(mousePosition.x, _minX, _maxX),
                Math.Clamp(mousePosition.y, _minY, _maxY));

            _objectOnScenes = Instantiate(_spawnObject, mousePosition, transform.rotation);
            try
            {
                _objectOnScenes.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            }
            catch
            {
                Debug.Log($"{_objectOnScenes.name} haven't Rigidbody2D. Object was destroy.");
                Destroy(_objectOnScenes);
            }
           
           _ObjectIsntSpawn = false;
        }
        private void Start()
        {
            try
            {
                _content = GameObject.Find("Content");
            }
            catch
            {
                Debug.Log($"Can't found object with name 'Content'");
                return;
            }
            
            GameObject[] PrefabBoxes = new GameObject[0];
            try
            {
                PrefabBoxes = _content.GetComponent<Scroling>().PrefabBoxes;
            }
            catch
            {
                Debug.Log($"Lost class Scroling on {_content.name}.");
                return;
            }
            
            for (int i = 0; i < PrefabBoxes.Length; i++)
            {
                if (PrefabBoxes[i] == gameObject)
                {
                    _spawnObject = _content.GetComponent<Scroling>().PartOfBotsAll[i].Prefab;
                    break;
                }
            }

            _ObjectIsntSpawn = true;
            _spawnPermission = false;

            if(_limitPoint1.transform.position.x > _limitPoint2.transform.position.x) 
            { 
                _minX = _limitPoint2.transform.position.x;
                _maxX = _limitPoint1.transform.position.x;
            } else
            {
                _minX = _limitPoint1.transform.position.x;
                _maxX = _limitPoint2.transform.position.x;
            }
            if (_limitPoint1.transform.position.y > _limitPoint2.transform.position.y)
            {
                _minY = _limitPoint2.transform.position.y;
                _maxY = _limitPoint1.transform.position.y;
            } else
            {
                _minY = _limitPoint1.transform.position.y;
                _maxY = _limitPoint2.transform.position.y;
            }
        }
        private void Update()
        {
            try
            {
                _spawnPermission = gameObject.GetComponent<EventTriggerForSpawn>().IsHold;
                if (!_spawnPermission) _ObjectIsntSpawn = true;
            }
            catch
            {
                _spawnPermission = false;
            }

            if (_spawnPermission)
            {
                if (_ObjectIsntSpawn) SpawnPrefab();
                
                Vector2 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

                mousePosition = new Vector2(Mathf.Clamp(mousePosition.x, _minX, _maxX),
                    Math.Clamp(mousePosition.y, _minY, _maxY));

                Vector2 objectOnScenesPosition = _objectOnScenes.transform.position;
                _objectOnScenes.transform.position = new Vector2(Mathf.Lerp(objectOnScenesPosition.x, mousePosition.x,
                    _movementSharpness * Time.deltaTime), Mathf.Lerp(objectOnScenesPosition.y, mousePosition.y,
                    _movementSharpness * Time.deltaTime));
            } 
        }
    }
}