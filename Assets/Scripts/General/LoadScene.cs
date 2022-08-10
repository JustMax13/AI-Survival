using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public void LoadNextScane(string sceneName)
    {
        LoadingScreen.SetActive(true);
        SceneManager.LoadScene(sceneName);     
    }
}