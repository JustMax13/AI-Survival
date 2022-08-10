using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace General
{
    public class LoadFirstScene : MonoBehaviour
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
        private void Update()
        {
            if (SceneManager.GetActiveScene().isLoaded)
            {
                LoadingScreen.SetActive(false);
                Destroy(this);
            }

        }
    }
}