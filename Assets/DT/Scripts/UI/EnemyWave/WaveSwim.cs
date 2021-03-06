using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GameEnum;
using static DataEnum;

public class WaveSwim : MonoBehaviour,IPointerClickHandler
{
    public int waveNumber;

    public GameObject waveNumberPrb;

    public List<Sprite> waveSprites;

    private Tweener twe;

    public void Init(int number, List<StageEnemyData> datas,int offset = 0)
    {
        waveNumber = number;
        string str = datas[number].point1[0];
        if (str.Split('_').Length != 1)
        {
            GameObject go =Instantiate(waveNumberPrb, transform);
            go.GetComponent<Image>().sprite = waveSprites[0];
            Client_WaveNumber client_Wave;
            if (go.TryGetComponent<Client_WaveNumber>(out client_Wave))
            {
                client_Wave.waveNumber = waveNumber;
                client_Wave.buildingNumber = 0;
            }
        }
        str = datas[number].point2[0];
        if (str.Split('_').Length != 1)
        {
            GameObject go = Instantiate(waveNumberPrb, transform);
            go.GetComponent<Image>().sprite = waveSprites[1];
            Client_WaveNumber client_Wave;
            if (go.TryGetComponent<Client_WaveNumber>(out client_Wave))
            {
                client_Wave.waveNumber = waveNumber;
                client_Wave.buildingNumber = 1;
            }
        }
        str = datas[number].point3[0];
        if (str.Split('_').Length != 1)
        {
            GameObject go = Instantiate(waveNumberPrb, transform);
            go.GetComponent<Image>().sprite = waveSprites[2];
            Client_WaveNumber client_Wave;
            if (go.TryGetComponent<Client_WaveNumber>(out client_Wave))
            {
                client_Wave.waveNumber = waveNumber;
                client_Wave.buildingNumber = 2;
            }
        }
        str = datas[number].point4[0];
        if (str.Split('_').Length != 1)
        {
            GameObject go = Instantiate(waveNumberPrb, transform);
            go.GetComponent<Image>().sprite = waveSprites[3];
            Client_WaveNumber client_Wave;
            if (go.TryGetComponent<Client_WaveNumber>(out client_Wave))
            {
                client_Wave.waveNumber = waveNumber;
                client_Wave.buildingNumber = 3;
            }
        }
        str = datas[number].point5[0];
        if (str.Split('_').Length != 1)
        {
            GameObject go = Instantiate(waveNumberPrb, transform);
            go.GetComponent<Image>().sprite = waveSprites[4];
            Client_WaveNumber client_Wave;
            if (go.TryGetComponent<Client_WaveNumber>(out client_Wave))
            {
                client_Wave.waveNumber = waveNumber;
                client_Wave.buildingNumber = 4;
            }
        }
        str = datas[number].point6[0];
        if (str.Split('_').Length != 1)
        {
            GameObject go = Instantiate(waveNumberPrb, transform);
            go.GetComponent<Image>().sprite = waveSprites[5];
            Client_WaveNumber client_Wave;
            if (go.TryGetComponent<Client_WaveNumber>(out client_Wave))
            {
                client_Wave.waveNumber = waveNumber;
                client_Wave.buildingNumber = 5;
            }
        }
        int waitNumber = StageGoal.My.waitTimeList[number];
        if (waitNumber - offset <= 30)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -(waitNumber - offset) * 40f / 3f, transform.localPosition.z);
            twe = transform.DOLocalMoveY(0f, waitNumber - offset).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }
        else
        {
            if (PlayerData.My.creatRole == PlayerData.My.playerDutyID)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, -(waitNumber - offset) * 2f - 340f, transform.localPosition.z);
            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x, -(waitNumber - offset) * 3f - 310f, transform.localPosition.z);
            } 
            twe = transform.DOLocalMoveY(-400f, waitNumber - 30f - offset).SetEase(Ease.Linear).OnComplete(() =>
            {
                //print("Timecount before 30: " + StageGoal.My.timeCount);
                twe = transform.DOLocalMoveY(0f, 30f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    StageGoal.My.timeCount = waitNumber;
                    //print("Timecount: " + StageGoal.My.timeCount);
                    Destroy(gameObject);
                });
            });
        }
        //else
        //{
        //    transform.localPosition = new Vector3(transform.localPosition.x, -(waitNumber - offset) - 520f, transform.localPosition.z);
        //    twe = transform.DOLocalMoveY(-700f, waitNumber - 180f - offset).SetEase(Ease.Linear).OnComplete(() =>
        //    {
        //        twe = transform.DOLocalMoveY(-400f, 150f).SetEase(Ease.Linear).OnComplete(() =>
        //        {
        //            //print("Timecount before 30: " + StageGoal.My.timeCount);
        //            twe = transform.DOLocalMoveY(0f, 30f).SetEase(Ease.Linear).OnComplete(() =>
        //            {
        //                StageGoal.My.timeCount = waitNumber;
        //                //print("Timecount: " + StageGoal.My.timeCount);
        //                Destroy(gameObject);
        //            });
        //        });
        //    });
        //}
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (WaveCount.My.showDetail)
        {
            return;
        }
        WaveCount.My.InitWaveBg(waveNumber);
        DataUploadManager.My.AddData(消费者_点击进度条);
    }
}
