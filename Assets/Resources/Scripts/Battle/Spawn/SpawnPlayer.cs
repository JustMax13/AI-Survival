using UnityEngine;
using General;

namespace CombatMechanics
{
    public class SpawnPlayer : SpawnPrefab
    {
        [SerializeField] private GameObject LeftButton;
        [SerializeField] private GameObject RightButton;
        [SerializeField] private HPBars _HP;
        [SerializeField] private TamerHPCalcute _timer;

        private void OnEnemyWon() => Destroy(SpawnObject);
        override protected void Start()
        {
            base.Start();

            SpawnObject.layer = LayerMask.NameToLayer("Player");
            _HP.Count = SpawnObject.GetComponent<AllHPCount>();
            SetLayerNameForAllChild(SpawnObject.transform, "Player");
            _timer.PlayerHPCount = SpawnObject.GetComponent<AllHPCount>();

            LeftButton.SetActive(true);
            RightButton.SetActive(true);

            GameOverEvent.EnemyWon += OnEnemyWon;
        }
        private void OnDestroy() => GameOverEvent.EnemyWon -= OnEnemyWon;
    }
}