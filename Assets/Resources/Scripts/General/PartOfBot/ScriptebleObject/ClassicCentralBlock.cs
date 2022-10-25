using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    using General.PartOfBots;
    [CreateAssetMenu(fileName = "ClassicCentralBlock", menuName = "PartOfBots/New part/CentralBlock/ClassicCentralBlock", order = 0)]
    public class ClassicCentralBlock : PartOfBot
    {
        [SerializeField] private GameObject _prefab;
        
        private static float _currentCountOfPart = 0;
        private static float _maxCountOfPart = 1;
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
            protected set
            {
                _maxCountOfPart = value;
            } 
        }

        public override string Path => "Prefab/PartOfBot/CebtralBlock/ClassicCebtralBlock";
    }
}

