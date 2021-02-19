using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

public class WaveCount : MonoSingleton<WaveCount>
{
    public GameObject singleWavePrb;

    public Transform waveBg;

    public GameObject spotEnemyPrb;

    public GameObject closeBtn;

    public bool showDetail = false;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="datas"></param>
    public void Init(List<StageEnemyData> datas,int waveNumber)
    {
        GameObject go = Instantiate(singleWavePrb, transform.Find("Panel"));
        go.GetComponent<WaveSwim>().Init(waveNumber, datas);
    }

    /// <summary>
    /// 清空当前波消费者信息
    /// </summary>
    public void Clear()
    {
        Destroy(GetComponentInChildren<WaveSwim>().gameObject, 0f);
    }

    public void Move()
    {
        GetComponentInChildren<WaveSwim>().Move();
        //for (int i = 0; i < StageGoal.My.maxWaveNumber; i++)
        //{
        //    Destroy(transform.Find("Panel").GetChild(3 + i).gameObject,0f);
        //}
        //for (int i = 0; i < StageGoal.My.maxWaveNumber; i++)
        //{
        //    GameObject go = Instantiate(singleWavePrb, transform.Find("Panel"));
        //    go.GetComponent<WaveSwim>().Init(i, datas, newStartTime);
        //}
    }

    /// <summary>
    /// 初始化波数
    /// </summary>
    /// <param name="waveNumber"></param>
    public void InitWaveBg(int waveNumber)
    {
        waveBg.gameObject.SetActive(true);
        closeBtn.SetActive(true);
        ClearWaveBg();
        StageEnemyData data = StageGoal.My.enemyDatas[waveNumber];
        string str = data.point1[0];
        if (str.Split('_').Length != 1)
        {
            GameObject go = Instantiate(spotEnemyPrb,waveBg);
            go.GetComponent<WaveEnemySign>().Init(0, data.point1);
        }
        str = data.point2[0];
        if (str.Split('_').Length != 1)
        {
            GameObject go = Instantiate(spotEnemyPrb, waveBg);
            go.GetComponent<WaveEnemySign>().Init(1, data.point2);
        }
        str = data.point3[0];
        if (str.Split('_').Length != 1)
        {
            GameObject go = Instantiate(spotEnemyPrb, waveBg);
            go.GetComponent<WaveEnemySign>().Init(2, data.point3);
        }
        str = data.point4[0];
        if (str.Split('_').Length != 1)
        {
            GameObject go = Instantiate(spotEnemyPrb, waveBg);
            go.GetComponent<WaveEnemySign>().Init(3, data.point4);
        }
        str = data.point5[0];
        if (str.Split('_').Length != 1)
        {
            GameObject go = Instantiate(spotEnemyPrb, waveBg);
            go.GetComponent<WaveEnemySign>().Init(4, data.point5);
        }
        str = data.point6[0];
        if (str.Split('_').Length != 1)
        {
            GameObject go = Instantiate(spotEnemyPrb, waveBg);
            go.GetComponent<WaveEnemySign>().Init(5, data.point6);
        }
    }
/// <summary>
/// for FTE_2.5
/// </summary>
    public void InitWave()
    {
        waveBg.gameObject.SetActive(true);
        closeBtn.SetActive(true);
        ClearWaveBg();
        List<string> strList = new List<string>();
        strList.Add("OldpaoRare_6_454");
        GameObject go = Instantiate(spotEnemyPrb,waveBg);
        go.GetComponent<WaveEnemySign>().Init(0, strList);
    }

    /// <summary>
    /// 初始化波数
    /// </summary>
    /// <param name="waveNumber"></param>
    public void InitWaveBg(int waveNumber,int buildingIndex)
    {
        waveBg.gameObject.SetActive(true);
        closeBtn.SetActive(true);
        ClearWaveBg();
        StageEnemyData data = StageGoal.My.enemyDatas[waveNumber];
        switch (buildingIndex)
        {
            case 0:
                {
                    GameObject go = Instantiate(spotEnemyPrb, waveBg);
                    go.GetComponent<WaveEnemySign>().Init(0, data.point1);
                    break;
                }
            case 1:
                {
                    GameObject go = Instantiate(spotEnemyPrb, waveBg);
                    go.GetComponent<WaveEnemySign>().Init(1, data.point2);
                    break;
                }
            case 2:
                {
                    GameObject go = Instantiate(spotEnemyPrb, waveBg);
                    go.GetComponent<WaveEnemySign>().Init(2, data.point3);
                    break;
                }
            case 3:
                {
                    GameObject go = Instantiate(spotEnemyPrb, waveBg);
                    go.GetComponent<WaveEnemySign>().Init(3, data.point4);
                    break;
                }
            case 4:
                {
                    GameObject go = Instantiate(spotEnemyPrb, waveBg);
                    go.GetComponent<WaveEnemySign>().Init(4, data.point5);
                    break;
                }
            case 5:
                {
                    GameObject go = Instantiate(spotEnemyPrb, waveBg);
                    go.GetComponent<WaveEnemySign>().Init(5, data.point6);
                    break;
                }
            default:
                break;
        }
    }

    public void ClearWaveBg()
    {
        for (int i = 0; i < waveBg.childCount; i++)
        {
            Destroy(waveBg.GetChild(i).gameObject);
        }
    }

    private void Awake()
    {
        StageGoal.My.waveCountItem = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        waveBg.gameObject.SetActive(false);
        closeBtn.SetActive(false);
    }
}
