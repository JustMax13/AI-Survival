using UnityEngine;
using UnityEngine.UI;

namespace CombatMechanics
{
    public class ConnectedShotButton : MonoBehaviour
    {
        [SerializeField] private Button[] _shotButtons;

        private bool[] _busyShotButton;

        public Button[] ShotButtons { get => _shotButtons; }
        public bool[] BusyShotButton
        {
            get => _busyShotButton;
            set { _busyShotButton = value; }
        }

        private void Start()
        {
            SpawnEnd.SpawnEnded += OnSpawnEnd;

            _busyShotButton = new bool[_shotButtons.Length];
            for (int i = 0; i < _busyShotButton.Length; i++) _busyShotButton[i] = false;
        }
        private void OnSpawnEnd()
        {
            for (int i = 0; i < _shotButtons.Length; i++)
            {
                if (!_busyShotButton[i])
                    ButtonConnection.CallConnectedShotButton(_shotButtons[i]);
            }
        }
    }
}