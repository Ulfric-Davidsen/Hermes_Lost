using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace IHS.Menus
{
    public class MenuController : MonoBehaviour
    {
        [Header("Volume Settings")]
        [SerializeField] private TMP_Text volumeTextValue = null;
        [SerializeField] private Slider volumeSlider = null;
        [SerializeField] private float defaultVolume = 1.0f;

        [Header("Gameplay Settings")]
        [SerializeField] private TMP_Text controllerSensTextValue = null;
        [SerializeField] private Slider controllerSensSlider = null;
        [SerializeField] private int defaultSens = 5;
        public int mainControllerSens = 5;

        [Header("Toggle Settings")]
        [SerializeField] private Toggle invertYToggle = null;

        [Header("Graphics Settings")]
        [SerializeField] private TMP_Text brightnessTextValue = null;
        [SerializeField] private Slider brightnessSlider = null;
        [SerializeField] private float defaultBrightness = 1.0f;

        [Space(10)]
        [SerializeField] private TMP_Dropdown qualityDropdown;
        [SerializeField] private int defaultQualityLevel = 2;
        [SerializeField] private Toggle fullScreenToggle;

        private int _qualityLevel;
        private bool _isFullscreen;
        private float _brightnessLevel;

        [Header("Resolution Dropdown")]
        public TMP_Dropdown resolutionDropdown;
        private Resolution[] resolutions;

        [Header("Confirmation Prompt")]
        [SerializeField] private GameObject confirmationPrompt = null;
        [SerializeField] private int confirmationShowTime = 2;

        [Header("Levels To Load")]
        [SerializeField] private GameObject noSavedGameDialogue = null;
        public string _newGameLevel;
        private string levelToLoad;

        void Start()
        {
            GetResolution();

            if (PlayerPrefs.HasKey("masterQuality"))
            {
                return;
            }
            else
            {
                InitiateDefaultGraphics();
            }

            if (PlayerPrefs.HasKey("masterVolume"))
            {
                return;
            }
            else
            {
                InitiateDefaultAudio();
            }
            
            if (PlayerPrefs.HasKey("masterSens"))
            {
                return;
            }
            else
            {
                InitiateDefaultGameplay();
            }

        }

        ///PUBLIC///
        
        public void NewGameDialogueYes()
        {
            SceneManager.LoadScene(_newGameLevel);
        }

        public void LoadGameDialogueYes()
        {
            SceneManager.LoadScene(levelToLoad);

            if (PlayerPrefs.HasKey("SavedLevel"))
            {
                levelToLoad = PlayerPrefs.GetString("SavedLevel");
                SceneManager.LoadScene(levelToLoad);
            }
            else
            {
                noSavedGameDialogue.SetActive(true);
            }
        }

        public void ExitButton()
        {
            Application.Quit();
        }

        public void SetVolume(float volume)
        {
            AudioListener.volume = volume;
            volumeTextValue.text = volume.ToString("0.0");
        }

        public void VolumeApply()
        {
            PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
            StartCoroutine(ConfirmationBox());

        }

        public void SetControllerSens(float sensitivity)
        {
            mainControllerSens = Mathf.RoundToInt(sensitivity);
            controllerSensTextValue.text = sensitivity.ToString("0");
        }

        public void GameplayApply()
        {
            if(invertYToggle.isOn)
            {
                PlayerPrefs.SetInt("masterInvertY", 1);
            }
            else
            {
                PlayerPrefs.SetInt("masterInvertY", 0);
            }

            PlayerPrefs.SetFloat("masterSens", mainControllerSens);
            StartCoroutine(ConfirmationBox());
        }

        public void SetBrightness(float brightness)
        {
            _brightnessLevel = brightness;
            brightnessTextValue.text = brightness.ToString("0.0");

        }

        public void SetFullscreen(bool isFullscreen)
        {
            _isFullscreen = isFullscreen;
        }

        public void SetQuality(int qualityIndex)
        {
            _qualityLevel = qualityIndex;
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        public void GraphicsApply()
        {
            PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
            //Change with Post Processing, etc.

            PlayerPrefs.SetInt("masterQuality", _qualityLevel);
            QualitySettings.SetQualityLevel(_qualityLevel);

            PlayerPrefs.SetInt("masterFullscreen", (_isFullscreen ? 1 : 0));
            Screen.fullScreen = _isFullscreen;

            StartCoroutine(ConfirmationBox());
        }

        public void ResetButton(string MenuType)
        {
            if (MenuType == "Graphics")
            {
                InitiateDefaultGraphics();
                GraphicsApply();
            }

            if (MenuType == "Audio")
            {
                InitiateDefaultAudio();
                VolumeApply();
            }

            if (MenuType == "Gameplay")
            {
                InitiateDefaultGameplay();
                GameplayApply();
            }
        }

        public IEnumerator ConfirmationBox()
        {
            confirmationPrompt.SetActive(true);
            yield return new WaitForSeconds(confirmationShowTime);
            confirmationPrompt.SetActive(false);
        }

        ///PRIVATE///

        void GetResolution()
        {
            //Get all resolutions
            resolutions = Screen.resolutions;
            //Clear dropdown options
            resolutionDropdown.ClearOptions();
            //Create list of options
            List<string> options = new List<string>();

            int currentResolutionIndex = 0;

            //Search through length of resolutions array
            for (int i = 0; i < resolutions.Length; i++)
            {
                //Put width x height into string
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);
                //Check if resolution found = current width x height
                if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                {
                    //Set to current resolution
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        void InitiateDefaultGraphics()
        {
            brightnessTextValue.text = defaultBrightness.ToString("0.0");
            brightnessSlider.value = defaultBrightness;
            //Reset brightness value
            
            qualityDropdown.value = defaultQualityLevel;
            QualitySettings.SetQualityLevel(defaultQualityLevel);

            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;
        }

        void InitiateDefaultAudio()
        {
            AudioListener.volume = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            volumeSlider.value = defaultVolume;
        }

        void InitiateDefaultGameplay()
        {
            controllerSensTextValue.text = defaultSens.ToString("0");
            controllerSensSlider.value = defaultSens;
            mainControllerSens = defaultSens;
            invertYToggle.isOn = false;
        }

    }

}