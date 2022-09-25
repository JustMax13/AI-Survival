using General;
using UnityEngine;
using UnityEngine.UI;

namespace Editor
{
    public class PrefabSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject _limitPoint1;
        [SerializeField] private GameObject _limitPoint2;
        [SerializeField] private float _movementSharpness;

        private bool _spawnEnd;
        private bool _spawnPermission;
        private bool _ObjectIsntSpawn;
        private GameObject _content;
        private PartOfBots _spawnObject;
        private GameObject _objectOnScenes;
        // поскольку скрипт висит на конкретной кнопке и он отвечает за 1н обьект,
        // тут нужн обьект типа PartOfBots в который присвоить обьект который спавнит эта кнопка

        private void SpawnPrefab()
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

            _spawnEnd = false;
            _ObjectIsntSpawn = true;
            _spawnPermission = false;
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

                if(!_spawnPermission && _spawnEnd)
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