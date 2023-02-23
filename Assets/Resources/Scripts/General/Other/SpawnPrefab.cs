using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace General
{
    public class SpawnPrefab : MonoBehaviour
    {
        [SerializeField] private GameObject _prefabForSpawn;
        [SerializeField] private Transform _spawnPoint;
        public GameObject PrefabForSpawn
        {
            get => _prefabForSpawn;
            set
            {
                _prefabForSpawn = value;
            }
        } 
        public Transform SpawnPoint
        {
            get => _spawnPoint;
            set
            {
                _spawnPoint = value;
            }
        }
        protected GameObject SpawnObject { get; set; }

        virtual protected void Start()
        {
            SpawnObject = Instantiate(PrefabForSpawn, SpawnPoint.transform.position, PrefabForSpawn.transform.rotation);
        }

        protected void SetLayerNameForAllChild(Transform transform, string layerName)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer(layerName);

                if (child.childCount > 0)
                    SetLayerNameForAllChild(child, layerName);
            }
        }
    }
}