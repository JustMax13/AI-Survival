using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Editor
{
    public class PrefabSpawn : MonoBehaviour
    {
        //[SerializeField] private RectTransform _noSpawnZone;
        //[SerializeField] private GameObject _spawnObject;
        private bool _spawnPermission;

        private void SpawnPrefab()
        {
            // дописать код спавна 
        }
        private void Start()
        {
            _spawnPermission = false;
        }
        private void Update()
        {
            try
            {
                _spawnPermission = gameObject.GetComponent<EventTriggerForSpawn>().IsHold;
            }
            catch
            {
                _spawnPermission = false;
                Debug.Log($"Class EventTriggerForSpawn dont found on {gameObject.name}");
            }

            if (_spawnPermission) SpawnPrefab();
        }
    }
}