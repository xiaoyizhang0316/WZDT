using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;
using System;

public class Building : MonoBehaviour
{
    public List<GameObject> consumerGoList = new List<GameObject>();

    public int buildingId;

    public BuildingType buildingType;

    public int buildingQualityNeed;

    public int buildingSweet;

    public int buildingCrisp;

    public int capacity;

    public List<BuildingConfig> buildingConfigs;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        InitQuality();
        for (int i = 0; i < buildingConfigs.Count; i++)
        {
            for (int j = 0; j < buildingConfigs[i].num; j++)
            {
                int sex = UnityEngine.Random.Range(0, 2);
                string path;
                if (sex == 0)
                    path = "Prefabs/Consumer/Male/male_";
                else
                    path = "Prefabs/Consumer/Female/female_";
                int num = UnityEngine.Random.Range(1, 11);
                path += num.ToString();
                GameObject go = Instantiate(Resources.Load<GameObject>(path),transform);
                go.GetComponent<ConsumeSign>().Init(buildingConfigs[i].consumerType, buildingQualityNeed,buildingSweet,buildingCrisp);
                go.transform.localScale = Vector3.one;
                consumerGoList.Add(go);
                go.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 获得已经出门购物过消费者的人数
    /// </summary>
    /// <returns></returns>
    public int GetActiveConsuemr()
    {
        int result = 0;
        foreach (GameObject go in consumerGoList)
        {
            if (go.GetComponent<ConsumeSign>().totalSatisfy != 0)
            {
                result++;
            }
        }
        return result;
    }

    /// <summary>
    /// 统计楼宇中总体满意度
    /// </summary>
    /// <returns></returns>
    public float GetTotalSatisfy()
    {
        float result = 0;
        foreach (GameObject go in consumerGoList)
        {
            result += go.GetComponent<ConsumeSign>().totalSatisfy;
        }
        return result;
    }

    /// <summary>
    /// 为居民楼设置质量要求
    /// </summary>
    public void InitQuality()
    {
        switch (buildingType)
        {
            case BuildingType.Residential:
                buildingQualityNeed = UnityEngine.Random.Range(StageGoal.My.consumerQualityNeed - 10, StageGoal.My.consumerQualityNeed - 6);
                buildingSweet = UnityEngine.Random.Range(Mathf.Max(-5, StageGoal.My.standardSweet - 2), Mathf.Min(5,StageGoal.My.standardSweet + 3));
                buildingCrisp = UnityEngine.Random.Range(Mathf.Max(-5, StageGoal.My.standardCrisp - 2), Mathf.Min(5,StageGoal.My.standardCrisp + 2));
                break;
            case BuildingType.Bungalow:
                buildingQualityNeed = UnityEngine.Random.Range(StageGoal.My.consumerQualityNeed - 6, StageGoal.My.consumerQualityNeed - 1);
                buildingSweet = UnityEngine.Random.Range(Mathf.Max(-5, StageGoal.My.standardSweet - 2), Mathf.Min(5, StageGoal.My.standardSweet + 3));
                buildingCrisp = UnityEngine.Random.Range(Mathf.Max(-5, StageGoal.My.standardCrisp - 2), Mathf.Min(5, StageGoal.My.standardCrisp + 2));
                break;
            case BuildingType.Office:
                buildingQualityNeed = UnityEngine.Random.Range(StageGoal.My.consumerQualityNeed - 2, StageGoal.My.consumerQualityNeed + 7);
                buildingSweet = UnityEngine.Random.Range(Mathf.Max(-5, StageGoal.My.standardSweet - 2), Mathf.Min(5, StageGoal.My.standardSweet + 3));
                buildingCrisp = UnityEngine.Random.Range(Mathf.Max(-5, StageGoal.My.standardCrisp - 2), Mathf.Min(5, StageGoal.My.standardCrisp + 2));
                break;
            case BuildingType.Villa:
                buildingQualityNeed = UnityEngine.Random.Range(StageGoal.My.consumerQualityNeed + 10, StageGoal.My.consumerQualityNeed + 15);
                buildingSweet = UnityEngine.Random.Range(Mathf.Max(-5, StageGoal.My.standardSweet - 2), Mathf.Min(5, StageGoal.My.standardSweet + 3));
                buildingCrisp = UnityEngine.Random.Range(Mathf.Max(-5, StageGoal.My.standardCrisp - 2), Mathf.Min(5, StageGoal.My.standardCrisp + 2));
                break;
            case BuildingType.BuildingType1:
                buildingQualityNeed = UnityEngine.Random.Range(StageGoal.My.consumerQualityNeed + 5, StageGoal.My.consumerQualityNeed + 12);
                buildingSweet = UnityEngine.Random.Range(Mathf.Max(-5, StageGoal.My.standardSweet - 2), Mathf.Min(5, StageGoal.My.standardSweet + 3));
                buildingCrisp = UnityEngine.Random.Range(Mathf.Max(-5, StageGoal.My.standardCrisp - 2), Mathf.Min(5, StageGoal.My.standardCrisp + 2));
                break;
            case BuildingType.BuildingType2:
                buildingQualityNeed = UnityEngine.Random.Range(StageGoal.My.consumerQualityNeed - 8, StageGoal.My.consumerQualityNeed + 10);
                buildingSweet = UnityEngine.Random.Range(Mathf.Max(-5, StageGoal.My.standardSweet - 2), Mathf.Min(5, StageGoal.My.standardSweet + 3));
                buildingCrisp = UnityEngine.Random.Range(Mathf.Max(-5, StageGoal.My.standardCrisp - 2), Mathf.Min(5, StageGoal.My.standardCrisp + 2));
                break;
        }
        buildingQualityNeed = Mathf.Max(1, buildingQualityNeed);
    }

    public void OnMouseEnter()
    {
        BuildingPopUp.My.Init(this);
    }

    public void OnMouseExit()
    {
        BuildingPopUp.My.MenuHide();
    }

    public void OnMouseUp()
    {
        if (!UIManager.My.NeedRayCastPanel())
        {
            BuildingInfo.My.Init(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnConsumer", 10f, 10f);
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
    public class BuildingConfig
    {
        public ConsumerType consumerType;

        public int num;
    }
}
