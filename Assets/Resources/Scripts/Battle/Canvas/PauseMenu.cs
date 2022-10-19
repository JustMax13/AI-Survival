using UnityEngine;
using UnityEngine.SceneManagement;
using General;
namespace CombatMechanics
{
    public class PauseMenu : MonoBehaviour
    {
        private static bool _gameIsPaused = false;
        [SerializeField] private GameObject _pauseUI;
        [SerializeField] private GameObject _moveButton;
        [SerializeField] private GameObject _shotButton;
        [SerializeField] private GameObject _pauseButton;

        public void ButtonPause()
        {
            if (_gameIsPaused) Resume();
            else Pause();
        }

        private void SetActiveAllInterface(bool value)
        {
            _moveButton.SetActive(value);
            _shotButton.SetActive(value);
        }
        public void Resume()
        {
            _pauseUI.SetActive(false);
            SetActiveAllInterface(true);

            Time.timeScale = 1f;
            _gameIsPaused = false;
        }
        public void Pause()
        {
            SetActiveAllInterface(false);
            _pauseUI.SetActive(true);
            
            Time.timeScale = 0f;
            _gameIsPaused = true;
        }
        private void OnGameOver()
        {
            Pause();
            SetActiveAllInterface(false);
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