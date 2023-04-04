using UnityEngine;
using System;

namespace Editor
{
    using General.PartOfBots;
    using Editor.Interface;
    using Editor.Counting;

    public class PartForSpawn : MonoBehaviour
    {

        private bool _spawnEnd;
        private bool _partIsntSpawn;
        private bool _spawnPermission;

        private GameObject _partOnScene;
        private PartOfBot _spawnPart;
        private PartCountValue _partCountValue;
        private SpawnPart _generalValuesForSpawnPart;

        public PartCountValue PartCountValue { get => _partCountValue; }

        private void Start()
        {
            try
            {
                _generalValuesForSpawnPart = GameObject.FindGameObjectWithTag("ValuesForSpawn").GetComponent<SpawnPart>();
            }
            catch (System.Exception)
            {
                throw new Exception("Can't find object with tags 'ValuesForSpawn' or component 'SpawnPart' ");
            }

            GameObject[] PrefabBoxes;
            GameObject content = _generalValuesForSpawnPart.Content;
            try
            {
                PrefabBoxes = content.GetComponent<AddContent>().PrefabBoxes;
            }
            catch
            {
                Debug.Log($"Lost class Scroling on {content.name}.");
                return;
            }

            for (int i = 0; i < PrefabBoxes.Length; i++)
            {
                if (PrefabBoxes[i] == gameObject)
                {
                    _spawnPart = content.GetComponent<AddContent>().PartOfBotsAll[i];
                    _partCountValue = _spawnPart.Prefab.GetComponent<PartCountValue>();
                    break;
                }
            }

            _spawnEnd = false;
            _partIsntSpawn = true;
            _spawnPermission = false;
        }
        private void Update()
        {
            try
            {
                _spawnPermission = gameObject.GetComponent<EventTriggerForSpawn>().IsHold;
                if (!_spawnPermission)
                    _partIsntSpawn = true;

                if (!_spawnPermission && _spawnEnd)
                    _spawnEnd = false;
            }
            catch { _spawnPermission = false; }

            if (_spawnPermission && PartCountingSystem.AddIsPosible(_partCountValue))
            {
                if (_partIsntSpawn)
                    _partOnScene = _generalValuesForSpawnPart.SpawnPrefab(_spawnPart, ref _partIsntSpawn, ref _spawnEnd);

                //Vector2 mousePosition = DragAndDrop.MousePositionOnDragArea(_generalValuesForSpawnPart.LimitPoint1.position,
                //    _generalValuesForSpawnPart.LimitPoint2.position);

                //Vector2 objectOnScenesPosition = _partOnScene.transform.position;
                //_partOnScene.transform.position = new Vector2(Mathf.Lerp(objectOnScenesPosition.x, mousePosition.x,
                //    _generalValuesForSpawnPart.MovementSharpness * Time.deltaTime), Mathf.Lerp(objectOnScenesPosition.y, mousePosition.y,
                //    _generalValuesForSpawnPart.MovementSharpness * Time.deltaTime));
            }
        }
    }
}