﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static GameEnum;

public class BossConsumer : ConsumeSign
{
    /// <summary>
    /// 初始化
    /// </summary>
    public new void Init(List<Transform> paths)
    {
        isStart = false;
        isCanSelect = false;
        currentHealth = 0;
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
    /// 初始化所有特效
    /// </summary>
    public new void InitEffect()
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
    public new void AddEffect(int buffID)
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
    public new void RemoveEffect(int buffID)
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
    public new void InitAndMove()
    {
        //float waitTime = UnityEngine.Random.Range(0f, 0.5f);
        //Invoke("Move", waitTime);
        CheckBuffDuration();
        Move();
    }

    /// <summary>
    /// 初始化路径点
    /// </summary>
    /// <param name="paths"></param>
    public new void InitPath(List<Transform> paths)
    {
        pathList = new List<Vector3>();
        foreach (Transform t in paths)
        {
            float x = UnityEngine.Random.Range(-0.3f, 0.3f);
            float z = UnityEngine.Random.Range(-0.3f, 0.3f);
            pathList.Add(t.position + new Vector3(x, 0f, z));
        }
    }

    /// <summary>
    /// 消费者被击中时调用
    /// </summary>
    /// <param name="data"></param>
    public new void OnHit(ref ProductData data)
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

    /// <summary>
    /// 消费者被击杀时调用   
    /// </summary>
    public new void OnDeath()
    {
        foreach (BaseBuff b in buffList)
        {
            b.OnConsumerBeforeDead();
        }
        if (currentHealth < consumeData.maxHealth)
        {
            return;
        }
        currentHealth = 0;
        hud.healthImg.color = new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
        BaseLevelController.My.CountKillNumber(this);
        DeathAward();
    }

    /// <summary>
    /// 消费者存活时调用
    /// </summary>
    public new void OnAlive()
    {
        LivePunish();
        Stop();
        Destroy(gameObject);
    }

    /// <summary>
    /// 生命值检测
    /// </summary>
    public new void HealthCheck()
    {
        float per = currentHealth / (float)consumeData.maxHealth;
        hud.UpdateInfo(per);
        if (currentHealth >= consumeData.maxHealth)
        {
            OnDeath();
        }
    }

    /// <summary>
    /// 血量发生变化时
    /// </summary>
    /// <param name="num"></param>
    public new void ChangeHealth(int num)
    {
        if (isCanSelect)
        {
            currentHealth += num;
            StageGoal.My.playerSatisfy += num;
            if (currentHealth <= 0)
                currentHealth = 0;
            HealthCheck();
        }
    }

    /// <summary>
    /// 击杀奖励
    /// </summary>
    public new void DeathAward()
    {
        StageGoal.My.GetSatisfy(consumeData.killSatisfy);
        StageGoal.My.GetPlayerGold(consumeData.killMoney);
        StageGoal.My.Income(consumeData.killMoney, IncomeType.Consume);
        StageGoal.My.killNumber++;
    }

    /// <summary>
    /// 存活惩罚
    /// </summary>
    public new void LivePunish()
    {
        StageGoal.My.LostHealth(consumeData.liveSatisfy);
    }

    /// <summary>
    /// 结算属性抗性
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="data"></param>
    public new void CheckBulletElement(ref int damage, ProductData data)
    {
        float per = 1f;
        bool isNormal = true;
        foreach (int i in data.buffList)
        {
            BuffData b = GameDataMgr.My.GetBuffDataByID(i);
            if (b.bulletBuffType == BulletBuffType.Element)
            {
                per += elementResistance[b.elementType] / 100f - 1f;
                isNormal = false;
            }
        }
        if (isNormal)
        {
            per += elementResistance[ProductElementType.Normal] / 100f - 1f;
        }
        damage = (int)(damage * per);
    }

    /// <summary>
    /// 检测攻击特效
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="data"></param>
    public new void CheckAttackEffect(ref ProductData data)
    {
        foreach (int i in data.buffList)
        {
            BuffData b = GameDataMgr.My.GetBuffDataByID(i);
            if (b.bulletBuffType == BulletBuffType.AttackEffect)
            {
                int number = UnityEngine.Random.Range(1, 101);
                if (number <= b.attackEffect)
                {
                    BaseBuff buff = new BaseBuff();
                    buff.Init(b);
                    buff.OnProduct(ref data);
                    buff.SetConsumerBuff(this);
                    AddEffect(i);
                }
            }
        }
    }

    /// <summary>
    /// 检测debuff
    /// </summary>
    public new void CheckDebuff(ProductData data)
    {
        foreach (int i in data.buffList)
        {
            BuffData b = GameDataMgr.My.GetBuffDataByID(i);
            if (b.bulletBuffType == BulletBuffType.Debuff)
            {
                BaseBuff buff = new BaseBuff();
                buff.Init(b);
                buff.SetConsumerBuff(this);
                AddEffect(i);
            }
        }
    }

    /// <summary>
    /// 去目标地点    
    /// </summary>
    public new void Move()
    {
        GetComponent<Animator>().SetFloat("Speed_f", consumeData.moveSpeed);
        isStart = true;
        isCanSelect = true;
        float time = CalculateTime();
        tweener = transform.DOPath(pathList.ToArray(), time, PathType.CatmullRom, PathMode.Full3D).OnComplete(Move).SetEase(Ease.Linear).SetLookAt(0.01f);

    }

    /// <summary>
    /// 停止
    /// </summary>
    public new void Stop()
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
    public new float CalculateTime()
    {
        if (pathList.Count == 0)
            throw new Exception("路径点数量为0！");
        float result = Vector3.Distance(transform.position, pathList[0]) / consumeData.moveSpeed;
        for (int i = 0; i < pathList.Count - 1; i++)
        {
            result += Vector3.Distance(pathList[i], pathList[i + 1]) / consumeData.moveSpeed;
        }
        return result;
    }

    /// <summary>
    /// 重新计算速度
    /// </summary>
    public new void ChangeSpeed(int num)
    {
        float speedAdd = num / 100f;
        tweener.timeScale += speedAdd;
        //print("移动速度：" + tweener.timeScale.ToString());
    }

    #region BUFF

    /// <summary>
    /// buff增加时回调
    /// </summary>
    /// <param name="baseBuff"></param>
    public new void AddBuff(BaseBuff baseBuff)
    {
        for (int i = 0; i < buffList.Count; i++)
        {
            if (buffList[i].buffId == baseBuff.buffId)
                return;
        }
        buffList.Add(baseBuff);
        baseBuff.ConsumerBuffAdd();
        AddEffect(baseBuff.buffId);
    }

    /// <summary>
    /// buff删除时回调
    /// </summary>
    /// <param name="baseBuff"></param>
    public new void RemoveBuff(BaseBuff baseBuff)
    {
        baseBuff.ConsumerBuffRemove();
        RemoveEffect(baseBuff.buffId);
        buffList.Remove(baseBuff);
    }

    /// <summary>
    /// 检测所有buff的持续时间
    /// </summary>
    public new void CheckBuffDuration()
    {
        for (int i = 0; i < buffList.Count; i++)
        {
            buffList[i].OnConsumerTick();
            if (buffList[i].duration != -1)
            {
                buffList[i].duration--;
                if (buffList[i].duration == 0)
                {
                    RemoveBuff(buffList[i]);
                }
            }
        }
        buffTweener = transform.DOScale(transform.localScale, 1f).OnComplete(() => {
            CheckBuffDuration();
        });
    }

    #endregion

    public new void OnMouseDown()
    {
        NewCanvasUI.My.consumerInfoFloatWindow.SetActive(true);
        NewCanvasUI.My.consumerInfoFloatWindow.GetComponent<ConsumerFloatWindow>().Init(this);
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
