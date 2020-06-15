﻿using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsPanel : MonoSingleton<OptionsPanel>
{
    public Transform options;
    public Transform returnPanel;
    public Slider musicSlider;
    public Slider soundSlider;

    public Button language;
    public Button close;
    public Button returnToMap;

    public AudioMixer audioMixer;
    
    // Start is called before the first frame update
    void Start()
    {
        musicSlider.onValueChanged.AddListener((float value)=>OnSliderValueChanged(value, MusicType.Music));
        soundSlider.onValueChanged.AddListener((float value) => OnSliderValueChanged(value, MusicType.Sound));

        returnToMap.onClick.AddListener(ShowReturn);
        audioMixer.SetFloat("BackGroundVolume", musicSlider.value);
        audioMixer.SetFloat("SoundEffectVolume", soundSlider.value);
        close.onClick.AddListener(Close);
    }

    public void ShowOPtionsPanel()
    {
        options.gameObject.SetActive(true);
    }

    private void OnSliderValueChanged(float value, MusicType mt)
    {
        switch (mt)
        {
            case MusicType.Music:
                audioMixer.SetFloat("BackGroundVolume", musicSlider.value);
                // 设置全局音量大小
                break;
            case MusicType.Sound:
                audioMixer.SetFloat("SoundEffectVolume", soundSlider.value);
                // 设置全局音效声音大小
                break;
        }
    }

    public void Close()
    {
        options.gameObject.SetActive(false);
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