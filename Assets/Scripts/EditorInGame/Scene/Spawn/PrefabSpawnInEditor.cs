using General;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Editor
{
    using Editor.Interface;
    public class PrefabSpawnInEditor : MonoBehaviour
    {
        [SerializeField] private float _movementSharpness; // в общий скрипт; static
        [SerializeField] private GameObject _limitPoint1; // в общий скрипт; static
        [SerializeField] private GameObject _limitPoint2; // в общий скрипт; static

        private bool _spawnEnd; // в общий скрипт
        private bool _spawnPermission; // в общий скрипт
        private bool _ObjectIsntSpawn; // в общий скрипт
        private GameObject _content; // в общий скрипт; static, SerializeField
        private GameObject _objectOnScenes;
        private PartOfBot _spawnObject;

        private void SpawnPrefab() //вынести в общий скрипт
        {
            Vector2 mousePosition = DragAndDrop.MousePositionOnDragArea(_limitPoint1, _limitPoint2);

            _objectOnScenes = Instantiate(_spawnObject.Prefab, mousePosition, transform.rotation);
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
            _objectOnScenes.GetComponent<DragAndDropPart>().PartOfBot = _spawnObject;

            _ObjectIsntSpawn = false;
            _spawnEnd = true;
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

            var PrefabBoxes = new GameObject[0];
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
                    _spawnObject = _content.GetComponent<Scroling>().PartOfBotsAll[i];
                    break;
                }
            }
            // всю высше описаную логику закинуть в общий скрипт, но не в Start, а в отдельный метод,
            // а тут его уже просто вызвать 

            _spawnEnd = false; // в общий скрипт
            _ObjectIsntSpawn = true; // в общий скрипт
            _spawnPermission = false; // в общий скрипт
        }
        private void Update()
        {
            try
            {
                _spawnPermission = gameObject.GetComponent<EventTriggerForSpawn>().IsHold;
                if (!_spawnPermission)
                {
                    _ObjectIsntSpawn = true;
                }

                if (!_spawnPermission && _spawnEnd)
                {
                    _spawnEnd = false;
                    _spawnObject.CurrentCountOfPart++;
                }
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