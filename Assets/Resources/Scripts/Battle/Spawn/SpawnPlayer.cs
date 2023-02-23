using UnityEngine;
using General;

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

            SetLayerNameForAllChild(SpawnObject.transform, "Player");

            LeftButton.SetActive(true);
            RightButton.SetActive(true);

            GameOverEvent.EnemyWon += OnEnemyWon;
        }
        private void OnDestroy() => GameOverEvent.EnemyWon -= OnEnemyWon;
    }
}