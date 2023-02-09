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

            foreach (Transform child in SpawnObject.transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("Enemy");
                if (child.gameObject.tag == "CPU")
                {
                    child.GetComponent<AiBrain>().FoundAllPoints(allPoints);
                }
                foreach (Transform childInChild in child.transform)
                    childInChild.gameObject.layer = LayerMask.NameToLayer("Enemy");
            }

            GameOverEvent.PlayerWon += OnPlayerWon;
        }
        private void OnDestroy() => GameOverEvent.PlayerWon -= OnPlayerWon;
    }
}