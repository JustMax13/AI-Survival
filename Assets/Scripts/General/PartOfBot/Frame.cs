using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    [CreateAssetMenu(fileName = "Frame", menuName = "PartOfBots/New part/Frame", order = 1)]
    public class Frame : PartOfBots
    {
        [SerializeField] private GameObject _prefab;

        [SerializeField] private float _partTakesUpSpace;

        private static float _currentCountOfPart = 0;
        private static float _maxCountOfPart = 20;
        public override GameObject Prefab
        {
            get => _prefab;
            protected set { _prefab = value; }
        }

        public float PartTakesUpSpace
        {
            get => _partTakesUpSpace;
            set { _partTakesUpSpace = value; }
        }
        public override float CurrentCountOfPart
        {
            get => _currentCountOfPart;
            set { _currentCountOfPart = value; }
        }
        public override float MaxCountOfPart
        {
            get => _maxCountOfPart;
            protected set 
            { 
                if(_maxCountOfPart < value) _maxCountOfPart = value;
            }
        }
    }
}