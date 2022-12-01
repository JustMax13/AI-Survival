using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    using General.PartOfBots;
    [CreateAssetMenu(fileName = "ClassicFrame", menuName = "PartOfBots/New part/Frame/ClassicFrame", order = 1)]
    public class ClassicFrame : PartOfBot
    {
        [SerializeField] private float _partTakesUpSpace;

        [SerializeField] private GameObject _prefab;

        private static float _currentCountOfPart = 0;
        private static float _maxCountOfPart = 20;

        public float PartTakesUpSpace
        {
            get => _partTakesUpSpace;
            set { _partTakesUpSpace = value; }
        }

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
                if(_maxCountOfPart < value) _maxCountOfPart = value;
            }
        }

        public override string Path => "Prefab/PartOfBot/Frame/ClassicFrame";
        
        // сделать так, чтобы можно было создавать из одного вида деталий разные модификации
        // но чтобы это можно было не через скрипт сделать, а через создание обьекта определенного
        // типа. К примеру тип Frame
    }
}