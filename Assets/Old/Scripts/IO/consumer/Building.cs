using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;
using System;
using DG.Tweening;

public class Building : MonoBehaviour
{
    public List<GameObject> consumerGoList = new List<GameObject>();

    public int buildingId;

    public Dictionary<int, List<WaveConfig>> waveConfigs = new Dictionary<int, List<WaveConfig>>();

    public List<Transform> consumerPathList = new List<Transform>();

    public GameObject pathIndicator;

    public int intervalNumber = -1;

    public float intervalLength = 1f;

    public bool isFinishSpawn;

    public GameObject protalGameObject;

    public bool isUseTSJ = false;

    public List<WaveConfig> extraConsumer = new List<WaveConfig>();

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(List<StageEnemyData> datas)
    {
        for (int i = 0; i < datas.Count; i++)
        {
            switch (buildingId)
            {
                case 0:
                    InitSingleWave(datas[i].waveNumber, datas[i].point1);
                    break;
                case 1:
                    InitSingleWave(datas[i].waveNumber, datas[i].point2);
                    break;
                case 2:
                    InitSingleWave(datas[i].waveNumber, datas[i].point3);
                    break;
                case 3:
                    InitSingleWave(datas[i].waveNumber, datas[i].point4);
                    break;
                case 4:
                    InitSingleWave(datas[i].waveNumber, datas[i].point5);
                    break;
                case 5:
                    InitSingleWave(datas[i].waveNumber, datas[i].point6);
                    break;
                default:
                    throw new Exception("building Id over limit ");
            }
        }     
        protalGameObject.transform.DOScale(0, 0).Play();
    }

    /// <summary>
    /// 初始化每一波
    /// </summary>
    /// <param name="waveNum"></param>
    /// <param name="waveConfig"></param>
    public void InitSingleWave(int waveNum, List<string> waveConfig)
    {
        List<WaveConfig> tempList = new List<WaveConfig>();
        foreach (string str in waveConfig)
        {
            string[] strList = str.Split('_');
            WaveConfig config = new WaveConfig();
            if (strList.Length == 1)
                continue;
            config.consumerType = (ConsumerType)Enum.Parse(typeof(ConsumerType), strList[0]);
            config.num = int.Parse(strList[1]);
            config.buffList = new List<int>();
            string[] tempBuffList = strList[2].Split('|');
            foreach (string tempStr in tempBuffList)
            {
                config.buffList.Add(int.Parse(tempStr));
            }
            tempList.Add(config);
        }
        waveConfigs.Add(waveNum, tempList);
    }

    /// <summary>
    /// 每一波召唤消费者
    /// </summary>
    /// <param name="waveNumber"></param>
    public void SpawnConsumer(int waveNumber)
    {
        if (!waveConfigs.ContainsKey(waveNumber))
        {
            throw new Exception("配置表或者配置消费者刷新点错误");
        }
        else
        {
            StartCoroutine(SpawnWaveConsumer(waveNumber));
        }
    }

    /// <summary>
    /// 每波召唤消费者（协程）
    /// </summary>
    /// <param name="waveNumber"></param>
    /// <returns></returns>
    public IEnumerator SpawnWaveConsumer(int waveNumber)
    {
        int val = 0;
        isFinishSpawn = false;
        if (waveConfigs[waveNumber].Count == 0)
        {
            isFinishSpawn = true;
            yield break;
        }
        if (waveConfigs[waveNumber][0].num > 0)
        {
            DrawPathLine();
            protalGameObject.transform.DOScale(1, 1);
        }
        List<WaveConfig> result = new List<WaveConfig>();
        result.AddRange(waveConfigs[waveNumber]);
        result.AddRange(extraConsumer);
        foreach (WaveConfig w in result)
        {
            for (int i = 0; i < w.num; i++)
            {
                string path = "Prefabs/Consumer/" + w.consumerType.ToString();
                GameObject go = Instantiate(Resources.Load<GameObject>(path), transform);
                go.GetComponent<ConsumeSign>().Init(consumerPathList);
                go.transform.position = transform.position;
                go.transform.localPosition = Vector3.zero + new Vector3(0f, 0f, 0f);
                foreach (int num in w.buffList)
                {
                    if (num != -1)
                    {
                        BuffData buff = GameDataMgr.My.GetBuffDataByID(num);
                        BaseBuff baseBuff = new BaseBuff();
                        baseBuff.Init(buff);
                        baseBuff.SetConsumerBuff(go.GetComponent<ConsumeSign>());
                    }
                }
                float waitTime = 1f;
                val++;
                if (val == intervalNumber)
                {
                    val = 0;
                    waitTime = intervalLength;
                }
                go.GetComponent<ConsumeSign>().InitRangeBuff();
                Tweener twe = transform.DOScale(1f, waitTime);
                yield return twe.WaitForCompletion();
            }
        }
        isFinishSpawn = true;
        protalGameObject.transform.DOScale(0, 1).timeScale = 1f / DOTween.timeScale;
    }

    /// <summary>
    /// 生成消费者路径线路
    /// </summary>
    public void DrawPathLine()
    {
        List<Vector3> list = new List<Vector3>();
        for (int i = 0; i < consumerPathList.Count; i++)
        {
            list.Add(consumerPathList[i].position + new Vector3(0f, 0.1f, 0f));
        }
        GameObject go = Instantiate(pathIndicator, transform);
        go.transform.position = transform.position;
        Tweener twe = go.transform.DOPath(list.ToArray(), 0.1f, PathType.CatmullRom, PathMode.Full3D).OnComplete(() =>
        {
            Destroy(go);
        }).SetEase(Ease.Linear).SetLookAt(0.01f);
        twe.ForceInit();

        GetComponent<LineRenderer>().positionCount = twe.PathGetDrawPoints().Length;
        GetComponent<LineRenderer>().SetPositions(twe.PathGetDrawPoints());
        GetComponent<LineRenderer>().material.mainTextureOffset = new Vector2(0.85f, 0f);
        GetComponent<LineRenderer>().material.DOOffset(new Vector2(-1.6f, 0f), 1.5f).OnComplete(() =>
        {
            GetComponent<LineRenderer>().material.mainTextureOffset = new Vector2(0.85f, 0f);
            GetComponent<LineRenderer>().material.DOOffset(new Vector2(-1.6f, 0f), 1.5f).SetEase(Ease.Linear);
        }).SetEase(Ease.Linear);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        BuildingManager.My.buildings.Add(this);
    }

    [Serializable]
    public class WaveConfig
    {
        public ConsumerType consumerType;

        public int num;

        public List<int> buffList;
    }
}
