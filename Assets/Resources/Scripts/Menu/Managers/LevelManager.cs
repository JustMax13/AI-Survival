using Menu.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Button _playerButton;

        private static Button _playerButtonStatic;

        public static Button PlayerButton { get => _playerButtonStatic; private set => _playerButtonStatic = value; }

        private void Start()
        {
            _playerButtonStatic = _playerButton;

            LevelButton.ButtonOnSelect += SomeButtonSelect;
            LevelButton.ButtonOnDeselect += SomeButtonDeselect;
        }
        private void SomeButtonSelect(LevelButton levelButton)
        {
            _playerButtonStatic.interactable = true;
        }
        private void SomeButtonDeselect()
        {
            _playerButtonStatic.interactable = false;
        }

        private void OnDestroy()
        {
            LevelButton.ButtonOnSelect -= SomeButtonSelect;
            LevelButton.ButtonOnDeselect -= SomeButtonDeselect;
        }
    }
}