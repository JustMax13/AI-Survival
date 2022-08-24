using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Editor;

public class Scroling : MonoBehaviour
{
    [SerializeField] private GameObject boxForPrefab;
    [SerializeField] private GameObject Image;
    [SerializeField] private PartOfBots[] partOfBots;

    private int panelCount;

    private void Start()
    {
        partOfBots ??= new PartOfBots[0];
        panelCount = partOfBots.Length;
        
        {
            GameObject thisBox;

            for (int i = 0; i < panelCount; i++)
            {
                thisBox = Instantiate(boxForPrefab, transform, false);
                
                try
                {
                    //thisBox.transform.GetChild(0).GetComponent<RectTransform>().rect.size = partOfBots[i].prefab.transform.GetComponent<RectTransform>().rect.size;
                    // сделать так, чтобы картинки не были квадратными
                    thisBox.transform.GetChild(0).GetComponent<Image>().sprite = partOfBots[i].prefab.GetComponent<SpriteRenderer>().sprite;
                }
                catch
                {
                    Debug.Log("Does not exist child or Image on child");
                }
            }
        }
    }

}
