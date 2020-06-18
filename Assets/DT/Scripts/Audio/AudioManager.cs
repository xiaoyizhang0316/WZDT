using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.Audio;
using static GameEnum;
using System;
using UnityEngine.EventSystems;

public class AudioManager : MonoSingleton<AudioManager>
{
    public List<PlaySource> clipList = new List<PlaySource>();
    
    public void PlaySelectType(AudioClipType t)
    {
        for (int i = 0; i < clipList.Count; i++)
        {
            if (clipList[i].type == t)
            {
                GetComponent<AudioSource>().clip = clipList[i].clip;
                GetComponent<AudioSource>().Play();
            }
        }
    }
    
    [Serializable]
    public struct PlaySource
    {
        public AudioClipType type;

        public AudioClip clip;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(!EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hit = Physics.RaycastAll(ray);
                if (hit.Length > 0)
                {
                    if (hit[0].transform.CompareTag("MapLand"))
                    {
                        PlaySelectType(AudioClipType.PointerClick);
                    }
                }
            }
        }
    }
}
