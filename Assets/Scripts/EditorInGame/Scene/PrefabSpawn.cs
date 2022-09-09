using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using General;
using UnityEditor.SceneManagement;

namespace Editor
{
    public class PrefabSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject _limitPoint1;
        [SerializeField] private GameObject _limitPoint2;
        [SerializeField] private float _movementSharpness;

        private GameObject _content;
        private GameObject _spawnObject;
        private GameObject _objectOnScenes;
        private bool _spawnPermission;
        private bool _ObjectIsntSpawn;

        private void SpawnPrefab()
        {
            Vector2 mousePosition = DragAndDrop.MousePositionOnDragArea(_limitPoint1, _limitPoint2);

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

            _objectOnScenes.AddComponent<DragAndDropPart>();
            _objectOnScenes.GetComponent<DragAndDropPart>().BacklogCursor = 22;
            _objectOnScenes.GetComponent<DragAndDropPart>().LimitPoint1 = _limitPoint1;
            _objectOnScenes.GetComponent<DragAndDropPart>().LimitPoint2 = _limitPoint2;

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

                Vector2 mousePosition = DragAndDrop.MousePositionOnDragArea(_limitPoint1, _limitPoint2);

                Vector2 objectOnScenesPosition = _objectOnScenes.transform.position;
                _objectOnScenes.transform.position = new Vector2(Mathf.Lerp(objectOnScenesPosition.x, mousePosition.x,
                    _movementSharpness * Time.deltaTime), Mathf.Lerp(objectOnScenesPosition.y, mousePosition.y,
                    _movementSharpness * Time.deltaTime));
            } 
        }
    }
}