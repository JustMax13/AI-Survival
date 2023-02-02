using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using General;
using General.PartOfBots;

namespace Editor
{
    

    public class SpawnPart : MonoBehaviour
    {
        [SerializeField] private float _movementSharpness;

        [SerializeField] private GameObject _limitPoint1;
        [SerializeField] private GameObject _limitPoint2;
        [SerializeField] private GameObject _content;
        [SerializeField] private GameObject _parentsOfParts;

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

            partOnScene.transform.parent = _parentsOfParts.transform;
            partOnScene.AddComponent<DragAndDropPart>();
            partOnScene.GetComponent<DragAndDropPart>().PartOfBot = spawnPart;

            partIsntSpawn = false;
            spawnEnd = true;

            return partOnScene;
        }
    }
}