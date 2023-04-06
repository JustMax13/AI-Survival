using CombatMechanics.HP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    public class RefToHP : MonoBehaviour, IHaveHP
    {
        [SerializeField] private HPOnObject _mainPartHP;
        public float HP { get => _mainPartHP.HP; set { _mainPartHP.HP = value; } }
    }
}