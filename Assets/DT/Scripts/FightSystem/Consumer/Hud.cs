using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using System;
using UnityEngine.UI;

[Serializable]
public class Hud :  MonoBehaviour
{

    public Material _material;
    private ConsumeSign targetConsumer;
    public Image healthImg;

    public void Init(ConsumeSign sign)
    {
        targetConsumer = sign;
    }

    public void UpdateInfo(float per)
    {
        healthImg.DOFillAmount(per,0.2f);
    }


    private void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }
}