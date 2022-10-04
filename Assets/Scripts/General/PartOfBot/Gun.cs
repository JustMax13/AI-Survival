using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    [CreateAssetMenu(fileName = "Gun", menuName = "PartOfBots/New part/Gun", order = 2)]
    public class Gun : PartOfBot
    {
        [SerializeField] private GameObject _prefab;

        private static float _currentCountOfPart = 0;
        private static float _maxCountOfPart = 3;
        public override GameObject Prefab
        {
            get => _prefab;
            protected set { _prefab = value; }
        }

        public override float CurrentCountOfPart
        {
            get => _currentCountOfPart;
            set { _currentCountOfPart = value; }
        }
        public override float MaxCountOfPart
        {
            get => _maxCountOfPart;
            protected set { _maxCountOfPart = value; }
        }
    }
}