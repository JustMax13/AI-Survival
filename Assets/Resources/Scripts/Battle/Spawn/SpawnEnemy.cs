using General;
using UnityEngine;
using CombatMechanics.AI;

namespace CombatMechanics
{
    public class SpawnEnemy : SpawnPrefab
    {
        [SerializeField] private Vector2[] allPoints;
        private void OnPlayerWon() => Destroy(SpawnObject);

        override protected void Start()
        {
            base.Start();

            SpawnObject.layer = LayerMask.NameToLayer("Enemy");
            SpawnObject.GetComponent<AiBrain>().FoundAllPoints(allPoints);

            SetLayerNameForAllChild(SpawnObject.transform, "Enemy");

            GameOverEvent.PlayerWon += OnPlayerWon;
        }
        private void OnDestroy() => GameOverEvent.PlayerWon -= OnPlayerWon;
    }
}