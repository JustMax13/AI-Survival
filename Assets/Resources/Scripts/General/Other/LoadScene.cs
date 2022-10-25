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
            set { _loadingScreen = value; }
        }
        public void LoadNextScane(string sceneName)
        {
            LoadingScene.OnLoadedSomeScene();

            LoadingScreen = Instantiate(LoadingScreen);
            LoadingScreen.GetComponent<Canvas>().worldCamera = gameObject.transform.GetComponent<Canvas>().worldCamera;
            LoadingScreen.GetComponent<Canvas>().sortingLayerName = "UI";
            LoadingScreen.SetActive(true);
            
            SceneManager.LoadScene(sceneName);
        }
        public void LoadNextScane(int sceneIndex)
        {
            LoadingScene.OnLoadedSomeScene();

            LoadingScreen = Instantiate(LoadingScreen);
            LoadingScreen.GetComponent<Canvas>().worldCamera = gameObject.transform.GetComponent<Canvas>().worldCamera;
            LoadingScreen.GetComponent<Canvas>().sortingLayerName = "UI";
            LoadingScreen.SetActive(true);

            SceneManager.LoadScene(sceneIndex);
        }
        public void LoadCurrentScane()
        {
            LoadingScene.OnLoadedSomeScene();

            LoadingScreen = Instantiate(LoadingScreen);
            LoadingScreen.GetComponent<Canvas>().worldCamera = gameObject.transform.GetComponent<Canvas>().worldCamera;
            LoadingScreen.GetComponent<Canvas>().sortingLayerName = "UI";
            LoadingScreen.SetActive(true);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}