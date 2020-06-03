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

    public void Init(List<StageEnemyData> datas)
    {
        for (int i = 0; i < StageGoal.My.maxWaveNumber - 1; i++)
        {
            GameObject go = Instantiate(singleWavePrb, transform.Find("Panel"));
            go.GetComponent<WaveSwim>().Init(i,datas);
        }
    }

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

    public void ClearWaveBg()
    {
        for (int i = 0; i < waveBg.childCount; i++)
        {
            Destroy(waveBg.GetChild(0).gameObject);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
