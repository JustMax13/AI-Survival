using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    [SerializeField] public GameObject PauseUI;


    // Update is called once per frame

    public void PAUSE()
    {
        if (gameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }


    public void Resume()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    void Pause()
    {

        PauseUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;



    }
    
}
