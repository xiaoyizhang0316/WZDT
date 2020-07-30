using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BossBloodBar : IOIntensiveFramework.MonoSingleton.MonoSingleton<BossBloodBar>
{
    public Image barBG;
    public Text killCount;
    public Image bar;

    public Image boss;

    public List<Sprite> bossList;

    public Image skill_1;
    public Image skill_2;
    public Image skill_3;

    public Button outButton;
    public Button inButton;

    public GameObject detalInfo;

    public Image skillImage;


    public int currentkillCount;
    public Transform pos_0;
    public Transform pos_1;

    public Image buffImg;

    // Start is called before the first frame update
    void Start()
    {
        killCount.text = "1";
        FadeImage();
        currentkillCount = 1;
        inButton.gameObject.SetActive(false);
        skillImage.fillAmount = 0;
        outButton.onClick.AddListener(() =>
        {
            detalInfo.gameObject.transform.DOMove(pos_1.position, 0.3f).OnComplete(() =>
            {   
                outButton.gameObject.SetActive(false);
                inButton.gameObject.SetActive(true);
                skillImage.DOFillAmount(1, 0.3f).OnComplete(() => { InitSkill(); });
            });
        });
        inButton.onClick.AddListener(() =>
        {
            FadeImage();
            skillImage.DOFillAmount(0, 0.3f).OnComplete(() =>
            {
                detalInfo.gameObject.transform.DOMove(pos_0.position, 0.3f).OnComplete(() =>
                {
                    outButton.gameObject.SetActive(true);
                          inButton.gameObject.SetActive(false);
                });
            });
        });
        SetBar(0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InitDetalInfo()
    {
        
    }

    public void FadeImage()
    {
        skill_1.gameObject.SetActive(false);
        skill_2.gameObject.SetActive(false);
        skill_3.gameObject.SetActive(false);
    }

    public void InitSkill()
    {
        if (currentkillCount >= 5)
        {
            skill_1.gameObject.SetActive(true);
        }
        if (currentkillCount >= 10)
        {
            skill_2.gameObject.SetActive(true);
        }

        if (currentkillCount >= 15)
        {
            skill_3.gameObject.SetActive(true);
        }
    }

    public void SetKillCount(int count)
    {
        currentkillCount = count;
        killCount.text = count.ToString();
        InitSkill();
    }

    public void SetBar(float fillAmount, Action end = null)
    {
        bar.transform.GetComponent<RectTransform>()
            .DOSizeDelta(new Vector2(bar.transform.GetComponent<RectTransform>().sizeDelta.x, 480 * fillAmount), 0.3f)
            .OnComplete(() =>
            {
                if (end != null)
                {
                    end();
                }
            });
    }

    public void ChangeColor(Color color)
    {
        //  barBG.color = bar.color;
        bar.color = color;
    }
}