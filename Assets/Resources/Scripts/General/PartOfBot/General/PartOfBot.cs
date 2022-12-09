using UnityEngine;
using General.Pathes;

namespace General.PartOfBots
{
    [CreateAssetMenu(fileName = "Part", menuName = "PartOfBots/New part", order = 1)]
    public class PartOfBot : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _maxCountOfPart;
        public Sprite Icon
        {
            get => _icon;
            protected set { _icon = value; }
        }
        public GameObject Prefab 
        { 
            get => _prefab; 
            protected set { _prefab = value; }
        }
        public float MaxCountOfPart 
        { 
            get => _maxCountOfPart;
            protected set { _maxCountOfPart = value; }
        }
        public float CurrentCountOfPart { get; set; }
    }
}
