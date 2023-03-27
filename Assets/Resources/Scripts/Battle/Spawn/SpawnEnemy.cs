using General;
using UnityEngine;
using CombatMechanics.AI;

namespace CombatMechanics
{
    public class SpawnEnemy : SpawnPrefab
    {
        [SerializeField] private Vector2[] _allPoints;
        [SerializeField] private HPBars _HP;
        [SerializeField] private TamerHPCalcute _timer;

        override protected void Start()
        {
            base.Start();

            SpawnObject.layer = LayerMask.NameToLayer("Enemy");
            SpawnObject.GetComponent<AiBrain>().FoundAllPoints(_allPoints);
            _HP.Count = SpawnObject.GetComponent<AllHPCount>();
            SetLayerNameForAllChild(SpawnObject.transform, "Enemy");
            _timer.EnemyHPCount = SpawnObject.GetComponent<AllHPCount>();
            GameOverEvent.PlayerWon += OnPlayerWon;
        }

        private void OnPlayerWon() => Destroy(SpawnObject);
        private void OnDestroy() => GameOverEvent.PlayerWon -= OnPlayerWon;
    }
}