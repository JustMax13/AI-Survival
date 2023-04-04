using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using General.PartOfBots;
using Editor.Counting;
using System.Threading.Tasks;

namespace Editor.Interface
{
    public class AddContent : MonoBehaviour
    {
        [SerializeField] private uint space;

        [SerializeField] private GameObject boxForPrefab;
        [SerializeField] private PartOfBot[] partOfBot;

        private int panelCount;

        private GameObject[] _prefabBoxes;
        private TypeOfPart[] _typeOfPrefab;

        public GameObject[] PrefabBoxes { get => _prefabBoxes; }
        public PartOfBot[] PartOfBotsAll { get => partOfBot; }
        
        private void Start()
        {
            partOfBot ??= new PartOfBot[0];
            panelCount = partOfBot.Length;
            _prefabBoxes = new GameObject[panelCount];
            _typeOfPrefab = new TypeOfPart[panelCount];

            for (int i = 0; i < panelCount; i++)
            {
                _prefabBoxes[i] = Instantiate(boxForPrefab, transform, false);
                //_prefabBoxes[i].GetComponent<PartForSpawn>().PartCountValue is null
                AddTypeOfPrefab(_prefabBoxes[i].GetComponent<PartForSpawn>(), i);
                //_typeOfPrefab[i] = _prefabBoxes[i].GetComponent<PartForSpawn>().PartCountValue.TypeOfPart; // нужно сделать асинхронным

                try
                {
                    _prefabBoxes[i].transform.GetChild(0).GetComponent<Image>().preserveAspect = true;
                    _prefabBoxes[i].transform.GetChild(0).GetComponent<Image>().sprite = partOfBot[i].Icon;
                }
                catch { throw new System.Exception("Does not exist child or Icon on child"); }

                if (i == 0) 
                    continue;
                _prefabBoxes[i].transform.localPosition = new Vector2(_prefabBoxes[i - 1].transform.localPosition.x
                    + boxForPrefab.transform.GetComponent<RectTransform>().sizeDelta.x + space, _prefabBoxes[i].transform.localPosition.y);
            }

            {
                float newContentSizeX = panelCount * (boxForPrefab.transform.GetComponent<RectTransform>().sizeDelta.x + space) + space;
                transform.GetComponent<RectTransform>().offsetMax = new Vector2(newContentSizeX, transform.GetComponent<RectTransform>().offsetMax.y);
            }

            PartCountingSystem.CountPartUpdate += OnCounterUpdate;
        }
        
        private void OnCounterUpdate()
        {
            for (int i = 0; i < panelCount; i++)
            {
                PartCountingSystem.CurrentNumberOfType.TryGetValue(_typeOfPrefab[i], out float currentCount);
                PartCountingSystem.MaxNumberOfType.TryGetValue(_typeOfPrefab[i], out int maxCount);
               
                if (currentCount >= maxCount)
                {
                    _prefabBoxes[i].GetComponent<Button>().interactable = false;
                    _prefabBoxes[i].GetComponent<PartForSpawn>().enabled = false;
                }
                else
                {
                    _prefabBoxes[i].GetComponent<Button>().interactable = true; // после перезахода в редактор, тут null
                    _prefabBoxes[i].GetComponent<PartForSpawn>().enabled = true;
                }
            }
        }
        private async void AddTypeOfPrefab(PartForSpawn partForSpawn, int indexInTypeOfPrefab)
        {
            await Task.Run(() =>
            {
                while(true)
                {
                    if (partForSpawn.PartCountValue)
                    {
                        _typeOfPrefab[indexInTypeOfPrefab] = partForSpawn.PartCountValue.TypeOfPart;
                        break;
                    }  
                }
            });
        }
    }
}