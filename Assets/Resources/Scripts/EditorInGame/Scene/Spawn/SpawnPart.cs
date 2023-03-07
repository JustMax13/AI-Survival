using Editor.Moves;
using General;
using General.PartOfBots;
using System;
using UnityEngine;

namespace Editor
{
    public class SpawnPart : MonoBehaviour
    {
        [SerializeField] private float _movementSharpness;

        [SerializeField] private GameObject _content;
        [SerializeField] private GameObject _parentsOfParts;

        private Transform _limitPoint1;
        private Transform _limitPoint2;

        public static event Action<GameObject> SpawnPartEnd;
        public float MovementSharpness { get => _movementSharpness; }
        public Transform LimitPoint1 { get => _limitPoint1; }
        public Transform LimitPoint2 { get => _limitPoint2; }
        public GameObject Content { get => _content; }

        private void Start()
        {
            _limitPoint1 = DragAndDropValue.LimitPoint1;
            _limitPoint2 = DragAndDropValue.LimitPoint2;
        }
        public GameObject SpawnPrefab(PartOfBot spawnPart, ref bool partIsntSpawn, ref bool spawnEnd)
        {
            Vector2 mousePosition = DragAndDrop.MousePositionOnDragArea(_limitPoint1.position, _limitPoint2.position);

            GameObject partOnScene = Instantiate(spawnPart.Prefab, mousePosition, transform.rotation);
            try
            {
                partOnScene.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

                SetRigidbodyTypeForAllChild(partOnScene.transform, RigidbodyType2D.Kinematic);
            }
            catch
            {
                //Debug.Log($"{partOnScene.name} haven't Rigidbody2D. Object was destroy.");
                //Destroy(partOnScene);
            }

            partOnScene.transform.parent = _parentsOfParts.transform;

            partOnScene.AddComponent<DragAndDropPart>();
            partOnScene.GetComponent<DragAndDropPart>().PartOfBot = spawnPart;

            partIsntSpawn = false;
            spawnEnd = true;

            EventManager.OffCurrentAction();
            EventManager.CheckConditionsAndStartEvent(EventManager.ActionType.PartDrag);

            SpawnPartEnd?.Invoke(partOnScene);

            return partOnScene;
        }
        private void SetRigidbodyTypeForAllChild(Transform transform, RigidbodyType2D rigidbodyType2D)
        {
            foreach (Transform child in transform)
            {
                child.TryGetComponent<Rigidbody2D>(out var childRB);

                if (childRB)
                    childRB.bodyType = rigidbodyType2D;

                if (child.childCount > 0)
                    SetRigidbodyTypeForAllChild(child, rigidbodyType2D);
            }
        }
    }
}