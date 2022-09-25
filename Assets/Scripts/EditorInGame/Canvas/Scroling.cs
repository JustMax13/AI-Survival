using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using General;

namespace Editor
{
    public class Scroling : MonoBehaviour
    {
        [SerializeField] private uint space;

        [SerializeField] private GameObject boxForPrefab;
        [SerializeField] private PartOfBots[] partOfBots;

        private int panelCount;

        private GameObject[] _prefabBoxes;

        public GameObject[] PrefabBoxes { get => _prefabBoxes; }
        public PartOfBots[] PartOfBotsAll { get => partOfBots; }
        
        private void Start()
        {
            partOfBots ??= new PartOfBots[0];
            panelCount = partOfBots.Length;
            _prefabBoxes = new GameObject[panelCount];

            for (int i = 0; i < panelCount; i++)
            {
                _prefabBoxes[i] = Instantiate(boxForPrefab, transform, false);

                try
                {
                    _prefabBoxes[i].transform.GetChild(0).GetComponent<Image>().preserveAspect = true;
                    _prefabBoxes[i].transform.GetChild(0).GetComponent<Image>().sprite = partOfBots[i].Icon;
                }
                catch
                {
                    Debug.Log("Does not exist child or Icon on child");
                }
                if (i == 0) continue;
                _prefabBoxes[i].transform.localPosition = new Vector2(_prefabBoxes[i - 1].transform.localPosition.x
                    + boxForPrefab.transform.GetComponent<RectTransform>().sizeDelta.x + space, _prefabBoxes[i].transform.localPosition.y);
            }

            {
                float newContentSizeX = panelCount * (boxForPrefab.transform.GetComponent<RectTransform>().sizeDelta.x + space) + space;
                transform.GetComponent<RectTransform>().offsetMax = new Vector2(newContentSizeX, transform.GetComponent<RectTransform>().offsetMax.y);
            }
        }
        private void FixedUpdate()
        {
            for (int i = 0; i < panelCount; i++)
            {
                if (partOfBots[i].CurrentCountOfPart >= partOfBots[i].MaxCountOfPart)
                {
                    _prefabBoxes[i].GetComponent<Button>().interactable = false;
                    _prefabBoxes[i].GetComponent<PrefabSpawn>().enabled = false;
                }
                else 
                {
                    _prefabBoxes[i].GetComponent<Button>().interactable = true;
                    _prefabBoxes[i].GetComponent<PrefabSpawn>().enabled = true;
                }
            }
        }
    }
}