using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace General
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField]
        private GameObject _loadingScreen;
        public GameObject LoadingScreen
        {
            get => _loadingScreen;
            set
            {
                _loadingScreen = value;
            }
        }
        public void LoadNextScane(string sceneName)
        {
            LoadingScreen.SetActive(true);
            SceneManager.LoadScene(sceneName);
        }
    }
}