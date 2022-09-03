using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor
{
    [CreateAssetMenu(fileName = "New part", menuName = "PartOfBots/New part", order = 13)]
    public class PartOfBots : ScriptableObject
    {
        public GameObject Prefab;
        public int OpeningLevel;
    }
}
