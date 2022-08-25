using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Editor
{
    public class Scroling : MonoBehaviour
    {
        [SerializeField] private GameObject boxForPrefab;
        [SerializeField] private PartOfBots[] partOfBots;
        [SerializeField] private uint space;
        private GameObject[] prefabBoxes;
        private int panelCount;

        public PartOfBots[] PartOfBotsAll { get => partOfBots; }
        private void Start()
        {
            partOfBots ??= new PartOfBots[0];
            panelCount = partOfBots.Length;
            prefabBoxes = new GameObject[panelCount];

            for (int i = 0; i < panelCount; i++)
                {
                    prefabBoxes[i] = Instantiate(boxForPrefab, transform, false);

                    try
                    {
                        prefabBoxes[i].transform.GetChild(0).GetComponent<Image>().preserveAspect = true;
                        prefabBoxes[i].transform.GetChild(0).GetComponent<Image>().sprite = partOfBots[i]
                            .prefab.GetComponent<SpriteRenderer>().sprite;
                    }
                    catch
                    {
                        Debug.Log("Does not exist child or Image on child");
                    }
                    if (i == 0) continue;
                    prefabBoxes[i].transform.localPosition = new Vector2(prefabBoxes[i - 1].transform.localPosition.x
                        + boxForPrefab.transform.GetComponent<RectTransform>().sizeDelta.x + space, prefabBoxes[i].transform.localPosition.y);
                }

                {
                    float newContentSizeX = panelCount * (boxForPrefab.transform.GetComponent<RectTransform>().sizeDelta.x + space) + space;
                    transform.GetComponent<RectTransform>().offsetMax = new Vector2(newContentSizeX, transform.GetComponent<RectTransform>().offsetMax.y);
                }
        }

    }
}