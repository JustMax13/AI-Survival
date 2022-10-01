using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using UnityEngine.SceneManagement;

public class GaveOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverWindow;
    public void Resume()
    {
        _gameOverWindow.SetActive(false);
        Time.timeScale = 1f;
    }
}
