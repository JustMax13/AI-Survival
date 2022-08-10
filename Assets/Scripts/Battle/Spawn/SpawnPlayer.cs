using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using General;

namespace CombatMechanics
{
    public class SpawnPlayer : SpawnPrefab
    {
        [SerializeField]
        private Canvas _playingCanvas;
        public Canvas PlayingCanvas
        {
            get => _playingCanvas;
        }
        [SerializeField]
        private GameObject _moveButtonLeftPrefab;
        public GameObject MoveButtonLeftPrefab
        {
            get => _moveButtonLeftPrefab;
            set
            {
                _moveButtonLeftPrefab = value;
            }
        }
        [SerializeField]
        private GameObject _moveButtonRightPrefab;
        public GameObject MoveButtonRightPrefab
        {
            get => _moveButtonRightPrefab;
            set
            {
                _moveButtonRightPrefab = value;
            }
        }
        private GameObject LeftButton;
        private GameObject RightButton;
        private void Start()
        {
            LeftButton = Instantiate(MoveButtonLeftPrefab);
            RightButton = Instantiate(MoveButtonRightPrefab);

            LeftButton.transform.SetParent(PlayingCanvas.transform);
            RightButton.transform.SetParent(PlayingCanvas.transform);

            LeftButton.transform.localPosition = MoveButtonLeftPrefab.transform.localPosition;
            RightButton.transform.localPosition = MoveButtonRightPrefab.transform.localPosition;
        }
    }
}