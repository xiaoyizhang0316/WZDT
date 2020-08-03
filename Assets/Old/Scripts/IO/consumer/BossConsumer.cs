using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static GameEnum;
using Random = UnityEngine.Random;

public class BossConsumer : ConsumeSign
{
    private int skillOneTime = 60;
    private int skillTwoTime = 60;
    public List<GameObject> peopleList;
    public GameObject skillOneEffect;
    public GameObject skillTwoEffect;

    public GameObject littlePrb;

    private List<Transform> bossPathList = new List<Transform>();

    public List<int> randomList = new List<int>() { 701, 702, 703, 704, 705 };

    public List<int> tempBuffList = new List<int>() { 701, 702, 703, 704, 705 };

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
        BossBloodBar.My.SetBar(0f);
        //GameObject go = Instantiate(hudPrb, transform);
        //hud = go.GetComponent<Hud>();
        //hud.Init(this);
        //hud.healthImg.fillAmount = 0f;
        //go.transform.localPosition = Vector3.zero + new Vector3(0, 3.5f, 0);
        InitPath(paths);
        InitAndMove();
    }

    /// <summary>
    /// 初始化所有特效
    /// </summary>
    public override void InitEffect()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).CompareTag("Effect"))
            {
                debuffEffectList.Add(transform.GetChild(i).gameObject);
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 激活特效
    /// </summary>
    /// <param name="buffID"></param>
    public override void AddEffect(int buffID)
    {
        if (buffID <= 1000)
        {
            for (int i = 0; i < debuffEffectList.Count; i++)
            {
                if (debuffEffectList[i].name.Equals(buffID.ToString()))
                {
                    debuffEffectList[i].SetActive(true);
                    debuffEffectList[i].GetComponent<ParticleSystem>().Play();
                }
            }
        }
        else
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/BuffEffect/" + buffID.ToString()), transform);
            go.transform.localPosition = Vector3.zero;
        }
    }

    /// <summary>
    /// 移除特效
    /// </summary>
    /// <param name="buffID"></param>
    public override void RemoveEffect(int buffID)
    {
        for (int i = 0; i < debuffEffectList.Count; i++)
        {
            if (debuffEffectList[i].name.Equals(buffID.ToString()))
            {
                debuffEffectList[i].GetComponent<ParticleSystem>().Stop();
                debuffEffectList[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// 每次刷新消费者调用
    /// </summary>
    /// <param name="targetRole"></param>
    public override void InitAndMove()
    {
        //float waitTime = UnityEngine.Random.Range(0f, 0.5f);
        //Invoke("Move", waitTime);
        GetComponent<Animator>().SetFloat("Speed_f", consumeData.moveSpeed);
        float time = Vector3.Distance(bossPathList[0].position, transform.position) / consumeData.moveSpeed;
        transform.DOMove(bossPathList[0].position, time).SetEase(Ease.Linear).OnComplete(Move);
        transform.DOLookAt(bossPathList[0].position, 0f);
        SwitchElementResistance();
        LostHealth();
        CheckBuffDuration();
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

    /// <summary>
    /// 消费者被击中时调用
    /// </summary>
    /// <param name="data"></param>
    public override void OnHit(ref ProductData data)
    {
        if (isCanSelect)
        {
            lastHitType = data.bulletType;
            CheckAttackEffect(ref data);
            int realDamage = (int)data.damage;
            CheckBulletElement(ref realDamage, data);
            CheckDebuff(data);
            ChangeHealth(realDamage);
            if (transform.TryGetComponent(out Animator ani))
                ani.SetBool("OnHit", true);
        }
    }

    private int killCount = 1;


    /// <summary>
    /// 消费者被击杀时调用   
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
        AddPlayerResource();
        ChangeAttribute();
        ChangeModel();
        BaseLevelController.My.CountKillNumber(this);
        DeathAward();
    }

    public override void DeathAward()
    {
        StageGoal.My.GetSatisfy(consumeData.killSatisfy);
        StageGoal.My.GetPlayerGold(consumeData.killMoney);
        StageGoal.My.Income(consumeData.killMoney, IncomeType.Consume);

    }


    /// <summary>
    /// 层数增加提高生命上限
    /// </summary>
    public void ChangeAttribute()
    {
        currentHealth = 0;
        BossBloodBar.My.SetBar(0f, () =>
        {
            BossBloodBar.My.ChangeColor(new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f)));
            BossBloodBar.My.SetKillCount(killCount);
        });
        consumeData.maxHealth = consumeData.maxHealth * 120 / 100;
        consumeData.killMoney = consumeData.killMoney += 2000;
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
        if (killCount < 5)
        {
            peopleList[0].SetActive(true);
            self = peopleList[0];
            BossBloodBar.My.boss.sprite = BossBloodBar.My.bossList[0];
        }
        else if (killCount < 10)
        {
            peopleList[1].SetActive(true);
            self = peopleList[1];
            BossBloodBar.My.boss.sprite = BossBloodBar.My.bossList[1];
        }
        else if (killCount < 15)
        {
            peopleList[2].SetActive(true);
            self = peopleList[2];
            BossBloodBar.My.boss.sprite = BossBloodBar.My.bossList[2];
            if (killCount == 10)
                SkillOne();
        }
        else
        {
            peopleList[3].SetActive(true);
            self = peopleList[3];
            BossBloodBar.My.boss.sprite = BossBloodBar.My.bossList[3];
            if (killCount == 15)
                SkillTwo();
        }
    }

    /// <summary>
    /// 层数增加时增加玩家血量
    /// </summary>
    public void AddPlayerResource()
    {
        StageGoal.My.playerHealth += 30 + (killCount / 3) * 10;
    }

    /// <summary>
    /// 生命值检测
    /// </summary>
    public override void HealthCheck()
    {
        float per = currentHealth / (float)consumeData.maxHealth;
        BossBloodBar.My.SetBar(per);
        if (currentHealth >= consumeData.maxHealth)
        {
            OnDeath();
        }
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
    /// 停止
    /// </summary>
    public override void Stop()
    {
        tweener.Kill();
        buffTweener.Kill();
        isCanSelect = false;
        GetComponent<Animator>().SetFloat("Speed_f", 0f);
        BaseMapRole[] temp = FindObjectsOfType<BaseMapRole>();
        foreach (BaseMapRole role in temp)
        {
            role.RemoveConsumerFromShootList(this);
        }
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

    public override void OnMouseDown()
    {
        NewCanvasUI.My.consumerInfoFloatWindow.SetActive(true);
        NewCanvasUI.My.consumerInfoFloatWindow.GetComponent<ConsumerFloatWindow>().Init(this);
    }

    public void SkillOne()
    {

        transform.DOScale(transform.localScale, skillOneTime).OnComplete(() =>
        {
            //TODO 
            //    List<MapSign> signs = new List<MapSign>();
            //    for (int i = 0; i <   MapManager.My._mapSigns.Count; i++)
            //    {
            //        if (MapManager.My._mapSigns[i].mapType == MapType.Grass)
            //        {
            //            signs.Add(MapManager.My._mapSigns[i]);
            //        }
            //    }

            for (int i = 0; i < 3; i++)
            {
                MapSign sign = RandomGetMapSign();
                for (int j = 0; j < 30; j++)
                {

                    if (sign.lostEffect)
                    {
                        sign = RandomGetMapSign();
                    }
                    else
                    {
                        break;
                    }
                }
                sign.LostEffect(skillOneTime / 3);
                var lins = DrawLine(transform.transform.position, sign.transform.position);

                GameObject effect = Instantiate(skillOneEffect, transform);
                effect.transform.position = transform.position;
                effect.transform.parent = Camera.main.transform;
                effect.transform.DOPath(lins.ToArray(), 3).SetEase(Ease.Linear);
                Destroy(effect, 3);
            }

            SkillOne();
        });
    }

    public void SkillTwo()
    {

        transform.DOScale(transform.localScale, skillTwoTime).OnComplete(() =>
        {


            for (int i = 0; i < 3; i++)
            {
                MapSign sign = RandomGetMapSign();
                for (int j = 0; j < 30; j++)
                {

                    if (sign.lostEffect)
                    {
                        sign = RandomGetMapSign();
                    }
                    else
                    {
                        break;
                    }
                }

                sign.AddCost(999, skillTwoTime / 3);
                var lins = DrawLine(transform.transform.position, sign.transform.position);
                GameObject effect = Instantiate(skillTwoEffect, transform);
                effect.transform.position = transform.position;
                effect.transform.parent = Camera.main.transform;
                effect.transform.DOPath(lins.ToArray(), 3).SetEase(Ease.Linear);
                Destroy(effect, 3);
            }

            SkillTwo();
        });
    }
    public float per;
    public List<Vector3> DrawLine(Vector3 startTarget, Vector3 Target)
    {
        List<Vector3> pointList = new List<Vector3>();
        int vertexCount = 20; //采样点数量
        pointList.Clear();
        pointList.Add(startTarget);
        if (startTarget != null && Target != null)
        {
            float x = startTarget.x * per + Target.x * (per);
            //float y = startTarget.localPosition.y * per + Target.localPosition.y * (1f - per) ;
            float y = 10;
            float z = startTarget.z * per + Target.z * (per);
            Vector3 point3 = new Vector3(x, y, z);
            for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
            {
                Vector3 tangentLineVertex1 = Vector3.Lerp(startTarget, point3, ratio);
                Vector3 tangentLineVectex2 = Vector3.Lerp(point3, Target, ratio);
                Vector3 bezierPoint = Vector3.Lerp(tangentLineVertex1, tangentLineVectex2, ratio);
                pointList.Add(bezierPoint);
            }
        }

        pointList.Add(Target);

        return pointList;
    }

    /// <summary>
    /// 每秒扣血
    /// </summary>
    public void LostHealth()
    {
        transform.DOScale(transform.localScale, 1f).OnComplete(() =>
        {
            StageGoal.My.LostHealth(-1);
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
            print(tweener.fullPosition);
            float ran = Random.Range(-2f, 2f);
            go.GetComponent<BossSummonConsumer>().Init(bossPathList, tweener.fullPosition + ran, consumeData.moveSpeed);
            go.GetComponent<BossSummonConsumer>().consumeData.maxHealth = (int)(consumeData.maxHealth * 0.2f);
            go.GetComponent<BossSummonConsumer>().consumeData.killMoney = (int)(consumeData.killMoney * 0.05f);
            go.GetComponent<BossSummonConsumer>().consumeData.killSatisfy = 0;
        }
    }

    /// <summary>
    /// 切换口味抗性
    /// </summary>
    public void SwitchElementResistance()
    {
        if (buffList.Count > 0)
        {
            if (tempBuffList.Contains(buffList[0].buffId))
            {
                int prevBuff = buffList[0].buffId;
                RemoveBuff(buffList[0]);
                tempBuffList.Remove(prevBuff);
                bornBuffList.Remove(prevBuff);
                if (tempBuffList.Count == 0)
                    tempBuffList.AddRange(randomList);
            }
        }
        int index = Random.Range(0, tempBuffList.Count);
        BuffData buff = GameDataMgr.My.GetBuffDataByID(tempBuffList[index]);
        BaseBuff baseBuff = new BaseBuff();
        baseBuff.Init(buff);
        baseBuff.SetConsumerBuff(this);
        bornBuffList.Insert(0, tempBuffList[index]);
        BossBloodBar.My.buffImg.GetComponent<WaveBuffSign>().Init(tempBuffList[index]);
        transform.DOScale(transform.localScale, 10).OnComplete(SwitchElementResistance);
    }


    public MapSign RandomGetMapSign()
    {
        int count = 0;
        List<MapSign> signs = new List<MapSign>();
        for (int i = 0; i < MapManager.My._mapSigns.Count; i++)
        {
            if (MapManager.My._mapSigns[i].mapType == MapType.Grass)
            {
                signs.Add(MapManager.My._mapSigns[i]);
                count += MapManager.My._mapSigns[i].weighting;
            }
        }

        int weighting = Random.Range(0, count);

        MapSign sign = GetWeightingForMapSign(weighting);

        return sign;
    }


    public MapSign GetWeightingForMapSign(int range)
    {
        int count = 0;
        List<MapSign> signs = new List<MapSign>();
        for (int i = 0; i < MapManager.My._mapSigns.Count; i++)
        {
            if (MapManager.My._mapSigns[i].mapType == MapType.Grass)
            {
                signs.Add(MapManager.My._mapSigns[i]);
                count += MapManager.My._mapSigns[i].weighting;
                if (count >= range)
                {
                    return MapManager.My._mapSigns[i];
                }
            }
        }
        return null;
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

    private void OnGUI()
    {
        if (GUILayout.Button("扣血"))
        {
            ChangeHealth(1000);
        }
    }
}
