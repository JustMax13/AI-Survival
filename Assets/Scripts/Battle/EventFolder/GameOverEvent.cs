using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    public class GameOverEvent : MonoBehaviour
    {
        private static bool GameOver;
        public static event Action PlayerWon;
        public static event Action EnemyWon;

        public static void OnPlayerWon()
        {
            if(!GameOver)
            {
                PlayerWon?.Invoke();
                GameOver = true;
            }
        }
        public static void OnEnemyWon()
        {
            if (!GameOver)
            {
                EnemyWon?.Invoke();
                GameOver = true;
            }
        }
       
        private void Start()
        {
            GameOver = false;
        }
    }
}