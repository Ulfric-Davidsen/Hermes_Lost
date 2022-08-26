using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace IHS.Menus
{
    public class LoadPrefs : MonoBehaviour
    {
        [Header("General Setting")]
        [SerializeField] private bool canUse = false;
        [SerializeField] private MenuController menuController;

        [Header("Volume Setting")]
        [SerializeField] private TMP_Text volumeTextValue = null;
        [SerializeField] private Slider volumeSlider = null;

        [Header("Brightness Setting")]
        [SerializeField] private TMP_Text brightnessTextValue = null;
        [SerializeField] private Slider brightnessSlider = null;

        [Header("Quality Level Setting")]
        [SerializeField] private TMP_Dropdown qualityDropdown;

        [Header("Fullscreen Setting")]
        [SerializeField] private Toggle fullScreenToggle;

        [Header("Sensitivity Setting")]
        [SerializeField] private TMP_Text controllerSensTextValue = null;
        [SerializeField] private Slider controllerSensSlider = null;

        [Header("Invert Y Setting")]
        [SerializeField] private Toggle invertYToggle = null;

        void Awake()
        {
            if (canUse)
            {
                //Load volume setting
                if (PlayerPrefs.HasKey("masterVolume"))
                {
                    float localVolume = PlayerPrefs.GetFloat("masterVolume");

                    volumeTextValue.text = localVolume.ToString("0.0");
                    volumeSlider.value = localVolume;
                    AudioListener.volume = localVolume;
                }

                //Load quality setting
                if (PlayerPrefs.HasKey("masterQuality"))
                {
                    int localQuality = PlayerPrefs.GetInt("masterQuality");

                    qualityDropdown.value = localQuality;
                    QualitySettings.SetQualityLevel(localQuality);
                }


                //Load fullscreen setting
                if (PlayerPrefs.HasKey("masterFullscreen"))
                {
                    int localFullscreen = PlayerPrefs.GetInt("masterFullscreen");

                    if (localFullscreen == 1)
                    {
                        Screen.fullScreen = true;
                        fullScreenToggle.isOn = true;
                    }
                    else
                    {
                        Screen.fullScreen = false;
                        fullScreenToggle.isOn = false;
                    }
                }


                //Load brightness setting
                if (PlayerPrefs.HasKey("masterBrightness"))
                {
                    float localBrightness = PlayerPrefs.GetFloat("masterBrightness");

                    brightnessTextValue.text = localBrightness.ToString("0.0");
                    brightnessSlider.value = localBrightness;
                    //Change brightness here
                }

                //Load sensitivity setting
                if (PlayerPrefs.HasKey("masterSens"))
                {
                    float localSensitivity = PlayerPrefs.GetFloat("masterSens");

                    controllerSensTextValue.text = localSensitivity.ToString("0.0");
                    controllerSensSlider.value = localSensitivity;
                    menuController.mainControllerSens = Mathf.RoundToInt(localSensitivity);
                }

                //Load Invert Y Setting
                if (PlayerPrefs.HasKey("masterInvertY"))
                {
                    if (PlayerPrefs.GetInt("masterInvertY") == 1)
                    {
                        invertYToggle.isOn = true;
                    }
                    else
                    {
                        invertYToggle.isOn = false;
                    }
                }
                
            }
        }
    }
}
