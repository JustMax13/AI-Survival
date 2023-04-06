using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Menu
{
    public class LoadMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _loadScreen;

        private uint _numberOfEventWait;
        private uint _counter;
        private void Awake()
        {
            if (_loadScreen == null)
                throw new System.Exception("LoadScreen == null");

            SpawnPlayerInMenu.PlayerSpawnEnd += AddToCounter;

            _counter = 0;
            _numberOfEventWait = 1;
        }

        private void Start()
        {
            _loadScreen.SetActive(true);
            Coroutine coroutine = StartCoroutine(WaitAllTaskDone());
        }
        private void AddToCounter() => _counter++;
        private IEnumerator WaitAllTaskDone()
        {
            yield return new WaitWhile(() => _counter != _numberOfEventWait);
            _loadScreen.SetActive(false);
        }
    }
}