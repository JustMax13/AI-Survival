using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor
{
    using CombatMechanics;
    using General;

    public class SpawnPart : MonoBehaviour
    {
        [SerializeField] private float _movementSharpness;

        [SerializeField] private GameObject _limitPoint1;
        [SerializeField] private GameObject _limitPoint2;
        [SerializeField] private GameObject _content;

        public float MovementSharpness { get => _movementSharpness; }
        public GameObject LimitPoint1 { get => _limitPoint1; }
        public GameObject LimitPoint2 { get => _limitPoint2; }
        public GameObject Content { get => _content; }

        public GameObject SpawnPrefab(PartOfBot spawnPart, ref bool partIsntSpawn, ref bool spawnEnd)
        {
            Vector2 mousePosition = DragAndDrop.MousePositionOnDragArea(_limitPoint1, _limitPoint2);

            GameObject partOnScene = Instantiate(spawnPart.Prefab, mousePosition, transform.rotation);
            try
            {
                partOnScene.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            }
            catch
            {
                Debug.Log($"{partOnScene.name} haven't Rigidbody2D. Object was destroy.");
                Destroy(partOnScene);
            }

            partOnScene.AddComponent<DragAndDropPart>();
            partOnScene.GetComponent<DragAndDropPart>().BacklogCursor = 22;
            partOnScene.GetComponent<DragAndDropPart>().LimitPoint1 = _limitPoint1;
            partOnScene.GetComponent<DragAndDropPart>().LimitPoint2 = _limitPoint2;
            partOnScene.GetComponent<DragAndDropPart>().PartOfBot = spawnPart;

            partIsntSpawn = false;
            spawnEnd = true;

            return partOnScene;
        }
    }
}