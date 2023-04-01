using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CombatMechanics
{
    public class ConnectShotButton : MonoBehaviour
    {
        [SerializeField] private Button[] _shotButtons;
        [SerializeField] private RotateButtonParent[] _rotateButtons;

        private bool[] _busyShotAndRotatePair;

        public Dictionary<Button, RotateButtonParent> ShotAndRotatePair { get; private set; }
        public bool[] BusyShotAndRotatePair
        {
            get => _busyShotAndRotatePair;
            set => _busyShotAndRotatePair = value;
        }

        private void Start()
        {
            if (_shotButtons.Length != _rotateButtons.Length)
                throw new System.Exception("Кількість кнопок для вистрілу не дорівнює кількості пар кнопок повороту");

            ShotAndRotatePair = new Dictionary<Button, RotateButtonParent>();
            for (int i = 0; i < _shotButtons.Length; i++)
                ShotAndRotatePair.Add(_shotButtons[i], _rotateButtons[i]);

            _busyShotAndRotatePair = new bool[ShotAndRotatePair.Count];
            for (int i = 0; i < _busyShotAndRotatePair.Length; i++)
                _busyShotAndRotatePair[i] = false;

            SpawnEnd.SpawnEnded += OnSpawnEnd;
        }
        private void OnSpawnEnd()
        {
            int counter = 0;
            foreach (var item in ShotAndRotatePair)
            {
                if (!_busyShotAndRotatePair[counter])
                {
                    ButtonConnection.CallConnectedShotButton(item);
                    break;
                }    
                    
                counter++;
            }

            //for (int i = 0; i < ShotAndRotatePair.Count; i++)
            //{
            //    if (!_busyShotAndRotatePair[i])
            //        ButtonConnection.CallConnectedShotButton(ShotAndRotatePair); // попробовать через foreath
            //}
        }
    }
}