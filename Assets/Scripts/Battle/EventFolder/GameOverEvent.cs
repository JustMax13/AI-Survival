using General;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    public class GameOverEvent : MonoBehaviour
    {
        private static bool _gameOver;

        public static event Action PlayerWon;
        public static event Action EnemyWon;

        public static void OnPlayerWon()
        {
            if(!_gameOver)
            {
                PlayerWon?.Invoke();
                _gameOver = true;
            }
        }
        public static void OnEnemyWon()
        {
            if (!_gameOver)
            {
                EnemyWon?.Invoke();
                _gameOver = true;
            }
        }
        private static void OnLoadedSomeScene() => _gameOver = false;
        private void Start()
        {
            _gameOver = false;
            LoadingScene.LoadedSomeScene += OnLoadedSomeScene;
        }
        private void OnDestroy()
        {
            LoadingScene.LoadedSomeScene -= OnLoadedSomeScene;
        }
    }
}