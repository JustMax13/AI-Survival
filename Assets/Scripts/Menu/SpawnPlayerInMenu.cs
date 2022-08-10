using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

namespace Menu
{
    public class SpawnPlayerInMenu : SpawnPrefab
    {
        private void Start()
        {
            spawnObject.transform.localScale *= 1.5f;
        }
    }
}