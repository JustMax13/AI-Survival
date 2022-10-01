using General;
using UnityEngine;

namespace CombatMechanics
{
    public class SpawnPlayer : SpawnPrefab
    {
        [SerializeField] private GameObject LeftButton;
        [SerializeField] private GameObject RightButton;

        private void OnEnemyWon() => Destroy(SpawnObject);
        override protected void Start()
        {
            base.Start();

            SpawnObject.layer = LayerMask.NameToLayer("Player");
            foreach (Transform child in SpawnObject.transform)
                child.gameObject.layer = LayerMask.NameToLayer("Player");

            LeftButton.SetActive(true);
            RightButton.SetActive(true);

            GameOverEvent.EnemyWon += OnEnemyWon;
        }
        private void OnDestroy() => GameOverEvent.EnemyWon -= OnEnemyWon;
    }
}