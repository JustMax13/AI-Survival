using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    public class CPUHP : HPofPart
    {
        private void OnDestroy()
        {
            if (gameObject.layer == LayerMask.NameToLayer("Player"))
                GameOverEvent.OnEnemyWon();
            else if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
                GameOverEvent.OnPlayerWon();
        }
    }
}