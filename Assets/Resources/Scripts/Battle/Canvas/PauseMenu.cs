using UnityEngine;

namespace CombatMechanics
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseUI;
        [SerializeField] private GameObject _pauseButton;
        [SerializeField] private GameObject[] _setOffWhenPause;

        private static bool _gameIsPaused = false;

        public void ButtonPause()
        {
            if (_gameIsPaused) Resume();
            else Pause();
        }

        private void SetActiveAll(bool value)
        {
            foreach (var item in _setOffWhenPause)
                item.SetActive(value);
        }
        public void Resume()
        {
            _pauseUI.SetActive(false);
            SetActiveAll(true);

            Time.timeScale = 1f;
            _gameIsPaused = false;
        }
        public void Pause()
        {
            SetActiveAll(false);
            _pauseUI.SetActive(true);

            Time.timeScale = 0f;
            _gameIsPaused = true;
        }
        private void OnGameOver()
        {
            SetActiveAll(false);
            _pauseButton.SetActive(false);
        }
        private void Start()
        {
            GameOverEvent.PlayerWon += OnGameOver;
            GameOverEvent.EnemyWon += OnGameOver;
        }
        private void OnDestroy()
        {
            GameOverEvent.PlayerWon -= OnGameOver;
            GameOverEvent.EnemyWon -= OnGameOver;
        }
    }
}