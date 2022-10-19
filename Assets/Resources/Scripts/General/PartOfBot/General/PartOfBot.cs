using UnityEngine;

namespace General
{
    public abstract class PartOfBot : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        public Sprite Icon
        {
            get => _icon;
            protected set { _icon = value; }
        }
        public abstract GameObject Prefab { get; protected set; }
        public abstract float MaxCountOfPart { get; protected set; }
        public abstract float CurrentCountOfPart { get; set; }
    }
}
