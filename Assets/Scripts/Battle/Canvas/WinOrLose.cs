using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    public class WinOrLose : MonoBehaviour
    {
        [SerializeField] private GameObject VictoryWindow;
        [SerializeField] private GameObject LoseWindow;

        private void OnPlayerWon()
        {
            VictoryWindow.SetActive(true);
        }
        private void OnEnemyWon()
        {
            LoseWindow.SetActive(true);
        }
        private void Start()
        {
            GameOverEvent.PlayerWon += OnPlayerWon;
            GameOverEvent.EnemyWon += OnEnemyWon;
        }
        private void OnDestroy()
        {
            GameOverEvent.PlayerWon -= OnPlayerWon;
            GameOverEvent.EnemyWon -= OnEnemyWon;
        }
    }
}