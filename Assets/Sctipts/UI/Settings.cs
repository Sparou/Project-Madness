using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public TMPro.TMP_Dropdown ResolutionDropdown;
    public TMPro.TMP_Dropdown QualityDropdown;

    Resolution[] resolutions;

    void Start()
    {
        ResolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;

        int CurrentResoltionIndex = 0;

        for (int i = 0; i<resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                CurrentResoltionIndex = i;
        }

        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.RefreshShownValue();
        LoadSettings(CurrentResoltionIndex);
    }


    public void SetFulScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int  qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SettingsToMenu()
    {
        return;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettingPreference", QualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference", ResolutionDropdown.value);
        PlayerPrefs.SetInt("FullScreenPreference",System.Convert.ToInt32(Screen.fullScreen));
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
    }
}
    