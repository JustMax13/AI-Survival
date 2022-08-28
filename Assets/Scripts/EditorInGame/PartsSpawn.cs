using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor
{
    public class PartsSpawn : MonoBehaviour
    {
        private GameObject[] prefabBoxes;
        private PartOfBots[] partOfBots;
        private GameObject Content;
        private void Start()
        {
            Content = GameObject.FindGameObjectWithTag("Content");
            prefabBoxes = Content.GetComponent<Scroling>().PrefabBoxes;
            partOfBots = Content.GetComponent<Scroling>().PartOfBotsAll;
        }
    }
}