using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

namespace Menu
{
    public class SpawnPlayerInMenu : SpawnPrefab
    {
        override protected void Start()
        {
            base.Start();
            SpawnObject.transform.localScale *= 1.5f;
        }
    }
}