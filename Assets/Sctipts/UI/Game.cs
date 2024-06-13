using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject PausePage;
    public GameObject GamePage;

    bool pauseOpened = false;


    private void Start()
    {
        PausePage.SetActive(false);
        GamePage.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!pauseOpened)
                OpenPause();
            else
                ContinueGame();
        }
    }

    public void OpenPause()
    {
        pauseOpened = true;
        Time.timeScale = 0.0f;
        GamePage.SetActive(false);
        PausePage.SetActive(true);
    }

    public void ContinueGame()
    {
        if (pauseOpened)
        {
            pauseOpened = false;
            Time.timeScale = 1.0f;
            PausePage.SetActive(false);
            GamePage.SetActive(true);
        }
    }
}
