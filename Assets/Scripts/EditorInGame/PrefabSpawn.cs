using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Editor
{
    public class PrefabSpawn : MonoBehaviour
    {
        private GameObject _content;
        private GameObject _spawnObject;
        private GameObject _objectOnScenes;
        private bool _spawnPermission;
        private bool _ObjectIsntSpawn;

        private void SpawnPrefab()
        {
            _objectOnScenes = Instantiate(_spawnObject, Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.rotation);
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
                
                _objectOnScenes.transform.position = mousePosition;
            } 
        }
    }
}