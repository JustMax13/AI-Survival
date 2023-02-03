using General;
using UnityEngine;

namespace CombatMechanics
{
    public class SpawnEnemy : SpawnPrefab
    {
        private void OnPlayerWon() => Destroy(SpawnObject);

        override protected void Start()
        {
            base.Start();

            SpawnObject.layer = LayerMask.NameToLayer("Enemy");

            foreach (Transform child in SpawnObject.transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("Enemy");

                foreach (Transform childInChild in child.transform)
                    childInChild.gameObject.layer = LayerMask.NameToLayer("Enemy");
            }

            GameOverEvent.PlayerWon += OnPlayerWon;
        }
        private void OnDestroy() => GameOverEvent.PlayerWon -= OnPlayerWon;
    }
}