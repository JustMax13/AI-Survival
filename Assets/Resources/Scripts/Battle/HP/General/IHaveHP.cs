using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    namespace HP
    {
        public interface IHaveHP
        {
            float HP { get; set; }
            public void TakeDamage(IHaveHP partOfBot, float damage) => partOfBot.HP -= damage;
        }
    }
}