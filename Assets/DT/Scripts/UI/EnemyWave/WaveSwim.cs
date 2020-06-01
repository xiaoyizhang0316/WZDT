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
        transform.localPosition = new Vector3(transform.localPosition.x, -waitNumber * 20, transform.localPosition.z);
        transform.DOLocalMoveY(0f, waitNumber).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(gameObject);
        });

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        WaveCount.My.InitWaveBg(waveNumber);
    }

    //public void Init(int buildingId, List<StageEnemyData> datas)
    //{
    //    for (int i = 0; i < datas.Count; i++)
    //    {
    //        GameObject go = Instantiate(singleWaveInfoPrb, transform);
    //        switch (buildingId)
    //        {
    //            case 0:
    //                InitEachEnemy(go, datas[i].point1);
    //                break;
    //            case 1:
    //                InitEachEnemy(go, datas[i].point2);
    //                break;
    //            case 2:
    //                InitEachEnemy(go, datas[i].point3);
    //                break;
    //            case 3:
    //                InitEachEnemy(go, datas[i].point4);
    //                break;
    //            case 4:
    //                InitEachEnemy(go, datas[i].point5);
    //                break;
    //            case 5:
    //                InitEachEnemy(go, datas[i].point6);
    //                break;
    //        }
    //        int waitNumber = StageGoal.My.waitTimeList[i];
    //        print(waitNumber);
    //        go.transform.localPosition = new Vector3(go.transform.localPosition.x, -StageGoal.My.waitTimeList[i] * 20, go.transform.localPosition.z);
    //        go.transform.DOLocalMoveY(500f, StageGoal.My.waitTimeList[i]).SetEase(Ease.Linear).OnComplete(()=> {
    //            Destroy(go);
    //        });
    //    }
    //}

    //public void InitEachEnemy(GameObject game,List<string> enemyInfo)
    //{
    //    for (int i = 0; i < enemyInfo.Count; i++)
    //    {
    //        GameObject go = Instantiate(singleEnemyInfoPrb, game.transform);
    //        ConsumerType type = (ConsumerType)Enum.Parse(typeof(ConsumerType),enemyInfo[i].Split('_')[0]);
    //        int number = int.Parse(enemyInfo[i].Split('_')[1]);
    //        go.GetComponent<SingleWaveEnemyInfo>().Init(type,number);
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
