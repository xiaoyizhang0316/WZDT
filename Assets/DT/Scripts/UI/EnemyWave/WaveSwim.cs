using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GameEnum;

public class WaveSwim : MonoBehaviour,IPointerClickHandler
{
    public int waveNumber;

    public GameObject waveNumberPrb;

    public List<Sprite> waveSprites;

    public void Init(int number, List<StageEnemyData> datas)
    {
        waveNumber = number;
        string str = datas[number].point1[0];
        if (str.Split('_').Length != 1)
        {
            GameObject go =Instantiate(waveNumberPrb, transform);
            go.GetComponent<Image>().sprite = waveSprites[0];
        }
        str = datas[number].point2[0];
        if (str.Split('_').Length != 1)
        {
            GameObject go = Instantiate(waveNumberPrb, transform);
            go.GetComponent<Image>().sprite = waveSprites[1];
        }
        str = datas[number].point3[0];
        if (str.Split('_').Length != 1)
        {
            GameObject go = Instantiate(waveNumberPrb, transform);
            go.GetComponent<Image>().sprite = waveSprites[2];
        }
        str = datas[number].point4[0];
        if (str.Split('_').Length != 1)
        {
            GameObject go = Instantiate(waveNumberPrb, transform);
            go.GetComponent<Image>().sprite = waveSprites[3];
        }
        str = datas[number].point5[0];
        if (str.Split('_').Length != 1)
        {
            GameObject go = Instantiate(waveNumberPrb, transform);
            go.GetComponent<Image>().sprite = waveSprites[4];
        }
        str = datas[number].point6[0];
        if (str.Split('_').Length != 1)
        {
            GameObject go = Instantiate(waveNumberPrb, transform);
            go.GetComponent<Image>().sprite = waveSprites[5];
        }
        int waitNumber = StageGoal.My.waitTimeList[number];
        if (waitNumber <= 30)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -waitNumber * 40f / 3f, transform.localPosition.z);
            transform.DOLocalMoveY(0f, waitNumber).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -waitNumber * 2f - 370f, transform.localPosition.z);
            transform.DOLocalMoveY(-400f, waitNumber - 30).SetEase(Ease.Linear).OnComplete(() =>
            {
                transform.DOLocalMoveY(0f, 30).SetEase(Ease.Linear).OnComplete(() =>
                {
                    Destroy(gameObject);
                });
            });
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        WaveCount.My.InitWaveBg(waveNumber);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
