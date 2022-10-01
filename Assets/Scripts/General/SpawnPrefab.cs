using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

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
    }
}