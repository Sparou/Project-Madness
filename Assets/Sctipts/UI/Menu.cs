using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject settingsPanel;
    public void Play()
    {
        SceneManager.LoadScene("Cave for inv");
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void SettingsPanale()
    {
        settingsPanel.SetActive(true);
    }

    public void ExitSets()
    {
        settingsPanel.SetActive(false);
    }
}
