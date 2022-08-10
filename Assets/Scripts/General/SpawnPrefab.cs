using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class SpawnPrefab : MonoBehaviour
    {
        [SerializeField]
        private GameObject _prefabSpawn;
        public GameObject PrefabSpawn
        {
            get => _prefabSpawn;
            set
            {
                _prefabSpawn = value;
            }
        }
        [SerializeField]
        private GameObject _spawnPoint;
        public GameObject SpawnPoint
        {
            get => _spawnPoint;
            set
            {
                _spawnPoint = value;
            }
        }
        protected GameObject spawnObject { get; set; }
        private void Awake()
        {
            spawnObject = Instantiate(PrefabSpawn, SpawnPoint.transform.position, PrefabSpawn.transform.rotation);
        }
    }
}