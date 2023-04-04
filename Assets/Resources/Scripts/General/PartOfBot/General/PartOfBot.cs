using UnityEngine;

namespace General.PartOfBots
{
    [CreateAssetMenu(fileName = "Part", menuName = "PartOfBots/New part", order = 1)]
    public class PartOfBot : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private GameObject _prefab;
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
    }
}
