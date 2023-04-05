using Editor.Counting;
using General.PartOfBots;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Editor.Interface
{
    public class FillContent : MonoBehaviour
    {
        [SerializeField] private uint _space;

        [SerializeField] private GameObject _boxForPrefab;
        [SerializeField] private PartOfBot[] _partOfBot;

        private static int panelCount;
        private static PartOfBot[] _partOfBotStatic;

        private static GameObject[] _prefabBoxes;
        private static TypeOfPart[] _typeOfPrefab;

        public static GameObject[] PrefabBoxes { get => _prefabBoxes; }
        public static PartOfBot[] PartOfBotsAll { get => _partOfBotStatic; }
        
        private void Start()
        {
            _partOfBot ??= new PartOfBot[0];
            _partOfBotStatic = _partOfBot;
            panelCount = _partOfBot.Length;
            _prefabBoxes = new GameObject[panelCount];
            _typeOfPrefab = new TypeOfPart[panelCount];

            for (int i = 0; i < panelCount; i++)
            {
                _prefabBoxes[i] = Instantiate(_boxForPrefab, transform, false);
                AddTypeOfPrefab(_prefabBoxes[i].GetComponent<PartForSpawn>(), i);

                try
                {
                    _prefabBoxes[i].transform.GetChild(0).GetComponent<Image>().preserveAspect = true;
                    _prefabBoxes[i].transform.GetChild(0).GetComponent<Image>().sprite = _partOfBot[i].Icon;
                }
                catch { throw new System.Exception("Does not exist child or Icon on child"); }

                if (i == 0) 
                    continue;
                _prefabBoxes[i].transform.localPosition = new Vector2(_prefabBoxes[i - 1].transform.localPosition.x
                    + _boxForPrefab.transform.GetComponent<RectTransform>().sizeDelta.x + _space, _prefabBoxes[i].transform.localPosition.y);
            }

            {
                float newContentSizeX = panelCount * (_boxForPrefab.transform.GetComponent<RectTransform>().sizeDelta.x + _space) + _space;
                transform.GetComponent<RectTransform>().offsetMax = new Vector2(newContentSizeX, transform.GetComponent<RectTransform>().offsetMax.y);
            }

            PartCountingSystem.CountPartUpdate += OnCounterUpdate;
        }
        
        private static void OnCounterUpdate()
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
                    _prefabBoxes[i].GetComponent<Button>().interactable = true;
                    _prefabBoxes[i].GetComponent<PartForSpawn>().enabled = true;
                }
            }
        }
        private static async void AddTypeOfPrefab(PartForSpawn partForSpawn, int indexInTypeOfPrefab)
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