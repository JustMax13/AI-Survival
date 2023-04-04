using UnityEngine;
using General;

namespace CombatMechanics
{
    public class SpawnPlayer : SpawnPrefab
    {
        [SerializeField] private GameObject _leftButton;
        [SerializeField] private GameObject _rightButton;
        [SerializeField] private HPBars _HP;
        [SerializeField] private TamerHPCalcute _timer;
        [SerializeField] private CameraMove _cameraMove;

        override protected void Start()
        {
            base.Start();

            SpawnObject.layer = LayerMask.NameToLayer("Player");
            _HP.Count = SpawnObject.GetComponent<AllHPCount>();
            SetLayerNameForAllChild(SpawnObject.transform, "Player");
            _timer.PlayerHPCount = SpawnObject.GetComponent<AllHPCount>();
            _cameraMove.SetPlayer(SpawnObject);

            _leftButton.SetActive(true);
            _rightButton.SetActive(true);

            GameOverEvent.EnemyWon += OnEnemyWon;
        }

        private void OnEnemyWon() => Destroy(SpawnObject);
        private void OnDestroy() => GameOverEvent.EnemyWon -= OnEnemyWon;
    }
}