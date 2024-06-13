using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pausePanel;
    public GameObject settingsPanel;

    public Toggle ToggleMusic;
    public Slider SliderVolumeMusic;
    public float volume;

    public TMPro.TMP_Dropdown ResolutionDropdown;
    public TMPro.TMP_Dropdown QualityDropdown;

    Resolution[] resolutions;

    private void Start()
    {
        settingsPanel.SetActive(false);
        LoadSetStart();
    }

    public void LoadSetStart()
    {
        ResolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;

        int CurrentResoltionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                CurrentResoltionIndex = i;
        }

        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.RefreshShownValue();
        LoadSettings(CurrentResoltionIndex);

        ValueMusic();
    }

    public void SliderMusic()
    {
        volume = SliderVolumeMusic.value;
        SaveSettings();
        ValueMusic();
    }

    public void TogMusic(bool isOn)
    {
        if (isOn == true)
            volume = 1;
        else
            volume = 0;
        SaveSettings();
        ValueMusic();
    }

    private void ValueMusic()
    {
        SliderVolumeMusic.value = volume;
        if (volume == 0) { ToggleMusic.isOn = false; } else { ToggleMusic.isOn = true; }
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SettingsPanale()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void SetFulScreen(bool isFullScreen)
    {
        Screen.SetResolution(Screen.width, Screen.height, isFullScreen);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SettingsToPause()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettingPreference", QualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference", ResolutionDropdown.value);
        PlayerPrefs.SetInt("FullScreenPreference", System.Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void LoadSettings(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey("QualitySettingPreference"))
            QualityDropdown.value = PlayerPrefs.GetInt("QualitySettingPreference");
        else
            QualityDropdown.value = 3;

        if (PlayerPrefs.HasKey("ResolutionPreference"))
            ResolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
        else
            ResolutionDropdown.value = currentResolutionIndex;

        if (PlayerPrefs.HasKey("FullScreenPreference"))
            Screen.fullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullScreenPreference"));
        else
            Screen.fullScreen = true;

        if (PlayerPrefs.HasKey("Volume"))
            volume = PlayerPrefs.GetFloat("Volume", volume);
    }
}
