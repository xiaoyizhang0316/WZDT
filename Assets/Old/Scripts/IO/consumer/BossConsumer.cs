﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static GameEnum;
using Random = UnityEngine.Random;

public class BossConsumer : ConsumeSign
{
    public int skillOneTime;
    public int skillTwoTime;
    public List<GameObject> peopleList;

    public GameObject littlePrb;

    private List<Transform> bossPathList = new List<Transform>();
    /// <summary>
    /// 初始化
    /// </summary>
    public override void Init(List<Transform> paths)
    {
        isStart = false;
        isCanSelect = false;
        currentHealth = 0;
        bossPathList.AddRange(paths);
        InitEffect();
        foreach (ProductElementType p in Enum.GetValues(typeof(ProductElementType)))
        {
            elementResistance.Add(p, 100);
        }
        consumeData = new ConsumeData(consumerType);
        ConsumerTypeData data = GameDataMgr.My.GetConsumerTypeDataByType(consumerType);
        foreach (int i in data.bornBuff)
        {
            BuffData buff = GameDataMgr.My.GetBuffDataByID(i);
            BaseBuff baseBuff = new BaseBuff();
            baseBuff.Init(buff);
            baseBuff.SetConsumerBuff(this);
            bornBuffList.Add(i);
        }
        if (PlayerData.My.cheatIndex2)
            consumeData.maxHealth = (int)(consumeData.maxHealth * 0.5f);
        GameObject go = Instantiate(hudPrb, transform);
        hud = go.GetComponent<Hud>();
        hud.Init(this);
        hud.healthImg.fillAmount = 0f;
        go.transform.localPosition = Vector3.zero + new Vector3(0, 3.5f, 0);
        InitPath(paths);
        InitAndMove();
    }

    /// <summary>
    /// 每次刷新消费者调用
    /// </summary>
    /// <param name="targetRole"></param>
    public override void InitAndMove()
    {
        GetComponent<Animator>().SetFloat("Speed_f", consumeData.moveSpeed);
        float time = Vector3.Distance(bossPathList[0].position, transform.position) / consumeData.moveSpeed;
        transform.DOMove(bossPathList[0].position, time).SetEase(Ease.Linear).OnComplete(Move);
        transform.DOLookAt(bossPathList[0].position,0f);
        LostHealth();
        CheckBuffDuration();
        //Move();
    }

    /// <summary>
    /// 初始化路径点
    /// </summary>
    /// <param name="paths"></param>
    public override void InitPath(List<Transform> paths)
    {
        pathList = new List<Vector3>();
        for (int i = 1; i < paths.Count; i++)
        {
            pathList.Add(paths[i].position);
        }
        pathList.Add(paths[0].position);
    }

    private int killCount = 0; 
     
    /// <summary>
    /// Boss本层被击杀时调用   
    /// </summary>
    public override void OnDeath()
    {
        foreach (BaseBuff b in buffList)
        {
            b.OnConsumerBeforeDead();
        }
        if (currentHealth < consumeData.maxHealth)
        {
            return;
        }
        killCount++;
        SummonLittle();
        DeathAward();
        AddPlayerResource();
        ChangeAttribute();
        ChangeModel();
        BaseLevelController.My.CountKillNumber(this);
    }

    /// <summary>
    /// 层数增加提高属性
    /// </summary>
    public void ChangeAttribute()
    {
        currentHealth = 0;
        consumeData.maxHealth = consumeData.maxHealth * 130 / 100;
        consumeData.killMoney = consumeData.killMoney * 130 / 100;
        if (killCount >= 20)
        {
            consumeData.maxHealth = 999999;
        }
        hud.healthImg.color = new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
        hud.UpdateInfo(0);
    }

    /// <summary>
    /// 层数增加换模型/释放特殊技能
    /// </summary>
    public void ChangeModel()
    {
        for (int i = 0; i < peopleList.Count; i++)
        {
            peopleList[i].gameObject.SetActive(false);
        }
        if (killCount <= 5)
        {
            peopleList[0].SetActive(true);
        }
        else if (killCount <= 10)
        {
            peopleList[1].SetActive(true);
            if (killCount == 10)
                SkillOne();
        }
        else if (killCount <= 15)
        {
            peopleList[2].SetActive(true);
            if (killCount == 15)
                SkillTwo();
        }
        else
        {
            peopleList[3].SetActive(true);
        }
    }

    /// <summary>
    /// 层数增加时增加玩家血量
    /// </summary>
    public void AddPlayerResource()
    {
        StageGoal.My.playerHealth += 5 + killCount / 3;
    }

    /// <summary>
    /// 血量发生变化时
    /// </summary>
    /// <param name="num"></param>
    public override void ChangeHealth(int num)
    {
        if (isCanSelect)
        {
            currentHealth += num;
            StageGoal.My.GetSatisfy(num);
            if (currentHealth <= 0)
                currentHealth = 0;
            HealthCheck();
        }
    }

    /// <summary>
    /// 去目标地点    
    /// </summary>
    public override void Move()
    {
        GetComponent<Animator>().SetFloat("Speed_f", consumeData.moveSpeed);
        isStart = true;
        isCanSelect = true;
        float time = CalculateTime();
        tweener = transform.DOPath(pathList.ToArray(), time, PathType.CatmullRom, PathMode.Full3D).SetEase(Ease.Linear).SetLookAt(0.01f).OnComplete(Move);

    }

    /// <summary>
    /// 计算全程时间
    /// </summary>
    /// <returns></returns>
    public override float CalculateTime()
    {
        if (pathList.Count == 0)
            throw new Exception("路径点数量为0！");
        float result = 0f;
        for (int i = 0; i < pathList.Count - 1; i++)
        {
            result += Vector3.Distance(pathList[i], pathList[i + 1]) / consumeData.moveSpeed;
        }
        result += Vector3.Distance(pathList[pathList.Count - 1], pathList[0]) / consumeData.moveSpeed;
        return result;
    }

    /// <summary>
    /// 1技能（重复施法）
    /// </summary>
    public void SkillOne()
    {
        transform.DOScale(transform.localScale, skillOneTime).OnComplete(() =>
        {
            List<MapSign> signs = new List<MapSign>();
            for (int i = 0; i <   MapManager.My._mapSigns.Count; i++)
            {
                if (MapManager.My._mapSigns[i].mapType == MapType.Grass)
                {
                    signs.Add(MapManager.My._mapSigns[i]);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                signs[Random.Range(0, signs.Count)].LostEffect(skillOneTime/3);
            }
            SkillOne();
        });
    }

    /// <summary>
    /// 2技能（重复施法）
    /// </summary>
    public void SkillTwo()
    {
        transform.DOScale(transform.localScale, skillTwoTime).OnComplete(() =>
        {
            List<MapSign> signs = new List<MapSign>();
            for (int i = 0; i <   MapManager.My._mapSigns.Count; i++)
            {
                if (MapManager.My._mapSigns[i].mapType == MapType.Grass)
                {
                    signs.Add(MapManager.My._mapSigns[i]);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                signs[Random.Range(0, signs.Count)].AddCost(1,skillTwoTime/3);
            }
            SkillTwo();
        });
    }

    /// <summary>
    /// 周期性扣除玩家血量
    /// </summary>
    public void LostHealth()
    {
        transform.DOScale(transform.localScale, 10f).OnComplete(() =>
        {
            StageGoal.My.LostHealth(-2 - killCount / 3);
            LostHealth();
        });
    }

    /// <summary>
    /// 召唤小弟
    /// </summary>
    public void SummonLittle()
    {
        if (killCount <= 5)
            return;
        for (int i = 0; i < 8; i++)
        {
            GameObject go = Instantiate(littlePrb, transform.parent);
            go.transform.position = bossPathList[0].position;
            float ran = Random.Range(-2f,2f);
            go.GetComponent<BossSummonConsumer>().Init(bossPathList, tweener.fullPosition + ran, consumeData.moveSpeed);
            go.GetComponent<BossSummonConsumer>().consumeData.maxHealth = (int)(consumeData.maxHealth * 0.2f);
            go.GetComponent<BossSummonConsumer>().consumeData.killMoney = (int)(consumeData.killMoney * 0.2f);
            go.GetComponent<BossSummonConsumer>().consumeData.killSatisfy = 0;
        }
    }

    /// <summary>
    /// 切换口味抗性
    /// </summary>
    public void SwitchElementResistance()
    {

    }

    private void Update()
    {
        //print(tweener.ElapsedPercentage(false));
        if (isIgnoreResistance)
        {
            try
            {
                self.SetActive(false);
                sheep.SetActive(true);
            }
            catch (Exception ex)
            {

            }
        }
        else
        {
            try
            {
                self.SetActive(true);
                sheep.SetActive(false);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
