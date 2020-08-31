using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ComboManager : IOIntensiveFramework.MonoSingleton.MonoSingleton<ComboManager>
{
    public Text num;

    public GameObject combo;
    public bool isShow;
    public int combonum = 0;
    public float showTime = 0;

    public void AddComboNum()
    {
        if (SceneManager.GetActiveScene().name == "FTE_0-1" || SceneManager.GetActiveScene().name == "FTE_0-2")
        {
            return;
        }
            combonum++;

        num.text = combonum.ToString();
        if (combonum >= 10)
        {
            combo.SetActive(true);


            isShow = true;
            showTime = 0;
            num.transform.localScale = Vector3.zero;

            num.transform.DOScale(1 + combonum * 0.01f, 0.4f).SetEase(Ease.InOutBounce).Play();
        }
    }

    public void Update()
    {
        if (isShow)
        {
            showTime += Time.deltaTime;
            if (showTime > 3)
            {
                isShow = false;
                combo.SetActive(false);
                combonum = 0;
                num.transform.localScale = Vector3.one;
            }
        }
    }

    public void Start()
    {
        combo.SetActive(false);
    }
}