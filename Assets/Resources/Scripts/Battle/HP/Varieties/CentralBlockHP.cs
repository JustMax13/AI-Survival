using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;

namespace CombatMechanics
{
    public class CentralBlockHP : HPofPart
    {
        protected override void FixedUpdate()
        {
            if(HP <= 0)
            {
                if (gameObject.layer == LayerMask.NameToLayer("Player"))
                    GameOverEvent.OnEnemyWon();
                else if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    GameOverEvent.OnPlayerWon();
                Destroy(gameObject);
            }
        }
    }
}