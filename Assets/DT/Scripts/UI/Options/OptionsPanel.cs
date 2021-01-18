using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsPanel : MonoSingleton<OptionsPanel>
{
    public GameObject mask;
    public Transform options;
    public Transform returnPanel;
    public Slider musicSlider;
    public Slider soundSlider;

    public Button language;
    public Button close;
    public Button returnToMap;

    public AudioMixer audioMixer;

    public Slider qualitySelect;

    public Toggle fullScreenSwitch;
    
    // Start is called before the first frame update
    void Start()
    {
        musicSlider.onValueChanged.AddListener((float value)=>OnSliderValueChanged(value, MusicType.Music));
        soundSlider.onValueChanged.AddListener((float value) => OnSliderValueChanged(value, MusicType.Sound));

        returnToMap.onClick.AddListener(ShowReturn);
        audioMixer.SetFloat("BackGroundVolume", -80);
        musicSlider.value = PlayerPrefs.GetInt("BackGroundVolume");
        audioMixer.SetFloat("SoundEffectVolume", -80);
        soundSlider.value = PlayerPrefs.GetInt("SoundEffectVolume");
        close.onClick.AddListener(Close);
    }

    public void ShowOPtionsPanel()
    {
        gameObject.SetActive(true);
        options.gameObject.SetActive(true);
        mask.SetActive(true);
        InitQualitySetting();
    }

    private void OnSliderValueChanged(float value, MusicType mt)
    {
        switch (mt)
        {
            case MusicType.Music:
                if (musicSlider.value == -20)
                {
                    audioMixer.SetFloat("BackGroundVolume", -80);
                    PlayerPrefs.SetInt("BackGroundVolume", -80);
                }
                else
                {
                    audioMixer.SetFloat("BackGroundVolume", musicSlider.value);
                    PlayerPrefs.SetInt("BackGroundVolume", (int)musicSlider.value);
                }
                // 设置全局音量大小
                break;
            case MusicType.Sound:
                if (soundSlider.value == -20)
                {
                    audioMixer.SetFloat("SoundEffectVolume", -80);
                    PlayerPrefs.SetInt("SoundEffectVolume", -80);
                }
                else
                {
                    audioMixer.SetFloat("SoundEffectVolume", soundSlider.value);
                    PlayerPrefs.SetInt("SoundEffectVolume", (int)soundSlider.value);
                }
                // 设置全局音效声音大小
                break;
        }
    }

    public void OnQualityValueChange()
    {
        QualitySettings.SetQualityLevel((int)qualitySelect.value);
    }

    public void InitQualitySetting()
    {
        qualitySelect.value = QualitySettings.GetQualityLevel();
        fullScreenSwitch.SetIsOnWithoutNotify(Screen.fullScreen);
    }

    public void OnFullScreenValueChange()
    {
        Screen.SetResolution(1920, 1080, fullScreenSwitch.isOn);
    }

    public void Close()
    {
        options.gameObject.SetActive(false);
        mask.SetActive(false);
        // 保存设置
    }

    void ShowReturn()
    {
        returnPanel.gameObject.SetActive(true);
    }
}

enum MusicType
{
    Music,
    Sound
}