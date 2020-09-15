using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;
using System;
using DG.Tweening;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public List<GameObject> consumerGoList = new List<GameObject>();

    public int buildingId;

    public Dictionary<int, List<WaveConfig>> waveConfigs = new Dictionary<int, List<WaveConfig>>();

    public List<Transform> consumerPathList = new List<Transform>();

    public GameObject pathIndicator;

    public bool isFinishSpawn;

    public GameObject protalGameObject;

    public bool isUseTSJ = false;

    public List<WaveConfig> extraConsumer = new List<WaveConfig>();

    public Image countDownSprite;

    public List<Material> materials;

    /// <summary>
    /// 使用透视镜
    /// </summary>
    public void UseTSJ()
    {
        countDownSprite.transform.parent.gameObject.SetActive(true);
        isUseTSJ = true;
        countDownSprite.fillAmount = 1f;
        countDownSprite.DOFillAmount(0f, 20f).OnComplete(() =>
        {
            countDownSprite.transform.parent.gameObject.SetActive(false);
            isUseTSJ = false;
        });
    }

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
        else if(gameObject.activeInHierarchy)
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
        isFinishSpawn = false;
        if (waveConfigs[waveNumber].Count == 0)
        {
            isFinishSpawn = true;
            yield break;
        }
        if (waveConfigs[waveNumber][0].num > 0)
        {
            DrawPathLine();
            protalGameObject.transform.DOScale(new Vector3(1,1,0.52f), 1);
        }
        List<WaveConfig> result = new List<WaveConfig>();
        result.AddRange(waveConfigs[waveNumber]);
        result.AddRange(extraConsumer);
        foreach (WaveConfig w in result)
        {
            for (int i = 0; i < w.num; i++)
            {
                float waitTime;
                if (NetworkMgr.My.useLocalJson)
                {
                    waitTime = GameDataMgr.My.consumerWaitTime[w.consumerType];
                }
                else
                {
                    waitTime = 0.75f;
                }
                Tweener twe = transform.DOScale(1f, waitTime);
                yield return twe.WaitForCompletion();
                string path = "Prefabs/Consumer/" + w.consumerType.ToString();
                GameObject go = Instantiate(Resources.Load<GameObject>(path), transform);
                go.GetComponent<ConsumeSign>().Init(consumerPathList);
                go.GetComponent<ConsumeSign>().buildingIndex = buildingId;
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
                        go.GetComponent<ConsumeSign>().bornBuffList.Add(num);
                    }
                }
                //go.GetComponent<ConsumeSign>().InitRangeBuff();
            }
        }
        isFinishSpawn = true;
        protalGameObject.transform.DOScale(0, 1);
    }

    public IEnumerator BornEnemy()
    {
        ConsumerType ct;
        yield return new WaitForSeconds(0.3f);
        DrawPathLine();
        protalGameObject.transform.DOScale(new Vector3(1, 1, 0.52f), 1);
        yield return new WaitForSeconds(0.5f);
        //GameObject.Find("Build/ConsumerSpot").GetComponent<Building>().SpawnConsumer(1);
        while (true)
        {
            yield return new WaitForSeconds(0.75f);
            ct = (ConsumerType)(UnityEngine.Random.Range(0, 2)==1?1:8);
            string path = "Prefabs/Consumer/" + ct.ToString();
            GameObject go = Instantiate(Resources.Load<GameObject>(path), transform);
            go.GetComponent<ConsumeSign>().Init(consumerPathList);
            go.GetComponent<ConsumeSign>().buildingIndex = buildingId;
            go.transform.position = transform.position;
            go.transform.localPosition = Vector3.zero + new Vector3(0f, 0f, 0f);
            //foreach (int num in w.buffList)
            //{
            //    if (num != -1)
            //    {
            //        BuffData buff = GameDataMgr.My.GetBuffDataByID(num);
            //        BaseBuff baseBuff = new BaseBuff();
            //        baseBuff.Init(buff);
            //        baseBuff.SetConsumerBuff(go.GetComponent<ConsumeSign>());
            //        go.GetComponent<ConsumeSign>().bornBuffList.Add(num);
            //    }
            //}
            float waitTime = 0.5f;
            Tweener twe = transform.DOScale(1f, waitTime);
            yield return twe.WaitForCompletion();
        }
    }

    /// <summary>
    /// 生成消费者路径线路
    /// </summary>
    public void DrawPathLine()
    {
        isPathLineShow = false;
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
        GetComponent<LineRenderer>().textureMode = LineTextureMode.Stretch;
        GetComponent<LineRenderer>().positionCount = twe.PathGetDrawPoints().Length;
        GetComponent<LineRenderer>().SetPositions(twe.PathGetDrawPoints());
        GetComponent<LineRenderer>().material = materials[0];
        GetComponent<LineRenderer>().material.mainTextureOffset = new Vector2(0.85f, 0f);
        GetComponent<LineRenderer>().material.DOOffset(new Vector2(-1.6f, 0f), 1.5f).OnComplete(() =>
        {
            GetComponent<LineRenderer>().material.mainTextureOffset = new Vector2(0.85f, 0f);
            GetComponent<LineRenderer>().material.DOOffset(new Vector2(-1.6f, 0f), 1.5f).SetEase(Ease.Linear);
        }).SetEase(Ease.Linear);
    }

    private bool isPathLineShow = false;

    public void ShowPathLine()
    {
        GetComponent<LineRenderer>().textureMode = LineTextureMode.Tile;
        //GetComponent<LineRenderer>().material.mainTextureOffset = new Vector2(0f, 0f);
        GetComponent<LineRenderer>().material = materials[1];
        isPathLineShow = true;
    }

    public void StopShowPathLine()
    {
        if (isPathLineShow)
        {
            GetComponent<LineRenderer>().material = materials[0];
            GetComponent<LineRenderer>().textureMode = LineTextureMode.Stretch;
            //GetComponent<LineRenderer>().material.mainTextureOffset = new Vector2(0.85f, 0f);
            isPathLineShow = false;
        }
    }

    public void OnMouseEnter()
    {
        ShowPathLine();
    }

    public void OnMouseExit()
    {
        StopShowPathLine();
    }

    // Start is called before the first frame update
    void Start()
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
        GetComponent<LineRenderer>().textureMode = LineTextureMode.Stretch;
        GetComponent<LineRenderer>().positionCount = twe.PathGetDrawPoints().Length;
        GetComponent<LineRenderer>().SetPositions(twe.PathGetDrawPoints());
    }

    // Update is called once per frame
    void Update()
    {
        if (isPathLineShow)
        {
            GetComponent<LineRenderer>().material.mainTextureOffset += new Vector2(-0.02f, 0f);
        }
    }

    private void Awake()
    {
        BuildingManager.My.buildings.Add(this);
        GetComponent<LineRenderer>().startColor = protalGameObject.GetComponent<ParticleSystem>().startColor;
        GetComponent<LineRenderer>().endColor = protalGameObject.GetComponent<ParticleSystem>().startColor;
        countDownSprite.color = protalGameObject.GetComponent<ParticleSystem>().startColor;
        countDownSprite.transform.parent.LookAt(Camera.main.transform.position);
        countDownSprite.transform.parent.gameObject.SetActive(false);
        Invoke("InitSetLand", 0.5f);
    }

    /// <summary>
    /// 初始化设置地块占用
    /// </summary>
    public void InitSetLand()
    {
        RaycastHit[] hit;
        hit = Physics.RaycastAll(transform.position + new Vector3(0f, 5f, 0f), Vector3.down);
        for (int j = 0; j < hit.Length; j++)
        {
            if (hit[j].transform.tag.Equals("MapLand"))
            {
                //print(hit[j].transform);
                MapManager.My.SetLand(hit[j].transform.GetComponent<MapSign>().x, hit[j].transform.GetComponent<MapSign>().y);
            }
        }
    }

    [Serializable]
    public class WaveConfig
    {
        public ConsumerType consumerType;

        public int num;

        public List<int> buffList;
    }
}
