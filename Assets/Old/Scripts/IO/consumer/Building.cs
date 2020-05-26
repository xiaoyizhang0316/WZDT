using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;
using System;

public class Building : MonoBehaviour
{
    public List<GameObject> consumerGoList = new List<GameObject>();

    public int buildingId;

    public Dictionary<int, List<WaveConfig>> waveConfigs = new Dictionary<int, List<WaveConfig>>();

    public List<Transform> consumerPathList = new List<Transform>();

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
        //foreach (StageEnemyData s in datas)
        //{
        //    print(s.waveNumber + datas.IndexOf(s).ToString());
            
        //}
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
            config.consumerType = (ConsumerType)Enum.Parse(typeof(ConsumerType), strList[0]);
            config.num = int.Parse(strList[1]);
            config.buffList = new List<int>();
            string[] tempBuffList = strList[2].Split('|');
            foreach(string tempStr in tempBuffList)
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
            foreach (WaveConfig w in waveConfigs[waveNumber])
            {
                for (int i = 0; i < w.num; i++)
                {
                    string path = "Prefabs/Consumer/" + w.consumerType.ToString();
                    GameObject go = Instantiate(Resources.Load<GameObject>(path), transform);
                    //go.transform.Rotate(new Vector3(90f,0f,0f));
                    go.GetComponent<ConsumeSign>().Init(consumerPathList);
                    go.transform.localScale = Vector3.one;
                    go.transform.localPosition = Vector3.zero + new Vector3(0f, 0f, -0.75f);
                }
            }
        }
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
