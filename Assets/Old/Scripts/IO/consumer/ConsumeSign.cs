using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static GameEnum;
using DT.Fight.Bullet;
using DG.Tweening;


public class ConsumeSign : MonoBehaviour
{
    /// <summary>
    /// 消费者数据
    /// </summary>
    public ConsumeData consumeData;

    /// <summary>
    /// 消费者类别
    /// </summary>
    public ConsumerType consumerType;

    /// <summary>
    /// 目标商店
    /// </summary>
    public BaseMapRole targetShop;

    /// <summary>
    /// DOTWeen
    /// </summary>
    public Tweener tweener;

    /// <summary>
    /// 当前生命值
    /// </summary>
    public int currentHealth;

    /// <summary>
    /// 属性抗性
    /// </summary>
    public Dictionary<ProductElementType, int> elementResistance = new Dictionary<ProductElementType, int>();

    /// <summary>
    /// 是否无视抗性
    /// </summary>
    public bool isIgnoreResistance = false;

    public GameObject hudPrb;

    public Hud hud;

    /// <summary>
    /// BUFF列表
    /// </summary>
    public List<BaseBuff> buffList = new List<BaseBuff>();

    public bool isStart = false;

    public bool isCanSelect = false;

    public Transform home;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(ConsumerType type,Transform building)
    {
        consumerType = type;
        home = building;
        foreach (ProductElementType p in Enum.GetValues(typeof(ProductElementType)))
        {
            elementResistance.Add(p, 100);
        }
        consumeData = new ConsumeData(consumerType);
        GameObject go = Instantiate(hudPrb, transform);
        hud = go.GetComponent<Hud>();
        hud.Init(this);
        go.transform.localPosition = Vector3.zero + new Vector3(0, 2.2f, 0);
    }

    /// <summary>
    /// 每次刷新消费者调用
    /// </summary>
    /// <param name="targetRole"></param>
    public void InitAndMove(BaseMapRole targetRole)
    {
        isStart = false;
        isCanSelect = false;
        int num = buffList.Count;
        for (int i = 0; i < num; i++)
        {
            RemoveBuff(buffList[0]);
        }
        currentHealth = 0;
        hud.healthImg.fillAmount = 0f;
        targetShop = targetRole;
        float waitTime = UnityEngine.Random.Range(0f, 2f);
        transform.DOLookAt(home.position, 0.1f);
        Invoke("MoveToShop", waitTime);
        if (consumeData.liveTime > 0)
        {
            Invoke("OnAlive", consumeData.liveTime + waitTime);
        }
    }

    /// <summary>
    /// 消费者被击中时调用
    /// </summary>
    /// <param name="data"></param>
    public void OnHit(ref ProductData data)
    {
        if (isCanSelect)
        {
            CheckAttackEffect(ref data);
            int realDamage = (int)data.damage;
            CheckBulletElement(ref realDamage, data);
            CheckDebuff(data);
            ChangeHealth(realDamage);
        }
    }

    /// <summary>
    /// 消费者被击杀时调用   
    /// </summary>
    public void OnDeath()
    {
        CancelInvoke("OnAlive");
        DeathAward();
        DeathBackHome();
        Stop();
    }

    /// <summary>
    /// 消费者存活时调用
    /// </summary>
    public void OnAlive()
    {
        CancelInvoke("OnAlive");
        LivePunish();
        AliveBackHome();
    }

    /// <summary>
    /// 生命值检测
    /// </summary>
    public void HealthCheck()
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
    public void ChangeHealth(int num)
    {
        if (isCanSelect)
        {
            currentHealth += num;
            HealthCheck();
        }
    }

    /// <summary>
    /// 击杀奖励
    /// </summary>
    public void DeathAward()
    {
        StageGoal.My.GetSatisfy(consumeData.killSatisfy);
        StageGoal.My.GetPlayerGold(consumeData.killMoney);
    }

    /// <summary>
    /// 存活惩罚
    /// </summary>
    public void LivePunish()
    {
        StageGoal.My.LostHealth(consumeData.liveSatisfy);
    }

    /// <summary>
    /// 结算属性抗性
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="data"></param>
    public void CheckBulletElement(ref int damage,ProductData data)
    {
        float per = 1f;
        foreach(int i in data.buffList)
        {
            BuffData b = GameDataMgr.My.GetBuffDataByID(i);
            if (b.bulletBuffType == BulletBuffType.Element)
            {
                per += elementResistance[b.elementType] / 100f - 1f;
            }
        }
        damage = (int)(damage * per);
    }

    /// <summary>
    /// 检测攻击特效
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="data"></param>
    public void CheckAttackEffect(ref ProductData data)
    {
        foreach (int i in data.buffList)
        {
            BuffData b = GameDataMgr.My.GetBuffDataByID(i);
            if (b.bulletBuffType == BulletBuffType.AttackEffect)
            {
                int number = UnityEngine.Random.Range(1,101);
                if (number <= b.attackEffect)
                {
                    BaseBuff buff = new BaseBuff();
                    buff.Init(b);
                    buff.OnProduct(ref data);
                    buff.SetConsumerBuff(this);

                }
            }
        }
    }

    /// <summary>
    /// 检测debuff
    /// </summary>
    public void CheckDebuff(ProductData data)
    {
        foreach (int i in data.buffList)
        {
            BuffData b = GameDataMgr.My.GetBuffDataByID(i);
            if (b.bulletBuffType == BulletBuffType.Debuff)
            {
                BaseBuff buff = new BaseBuff();
                buff.Init(b);
                buff.SetConsumerBuff(this);
            }
        }
    }

    /// <summary>
    /// 去目标的角色商店    
    /// </summary>
    public void MoveToShop()
    {
        GetComponent<Animator>().SetBool("walk", true);
        isStart = true;
        isCanSelect = true;
        Invoke("LookAtHome", 0.5f);
        InvokeRepeating("CheckBuffDuration", 0f, 1f);
        tweener = transform.DOMove(targetShop.transform.position, Vector3.Distance(transform.position, targetShop.transform.position) / consumeData.moveSpeed * 2f).OnComplete(() => OnAlive()).SetEase(Ease.Linear);
    }

    /// <summary>
    /// 看向家的方向
    /// </summary>
    public void LookAtHome()
    {
        transform.DOLookAt(home.position, 0f);
    }

    /// <summary>
    /// 存活自动回家
    /// </summary>
    public void AliveBackHome()
    {
        Stop();
        isCanSelect = false;
        isStart = false;
        targetShop.RemoveConsumerFromShootList(this);
        print("消费者存活");
        tweener = transform.DOMove(home.transform.position, Vector3.Distance(transform.position, home.position) / consumeData.moveSpeed).OnComplete(BackHome);
        GetComponent<Animator>().SetBool("walk", true);
        //float waitTime = UnityEngine.Random.Range(0.5f, 1.5f);
        //Invoke("BackHome", waitTime);
    }

    /// <summary>
    /// 死亡自动回家
    /// </summary>
    public void DeathBackHome()
    {
        Stop();
        isCanSelect = false;
        isStart = false;
        targetShop.RemoveConsumerFromShootList(this);
        print("消费者死亡");
        BackHome();
    }

    /// <summary>
    /// 停止
    /// </summary>
    public void Stop()
    {
        tweener.Kill();
        CancelInvoke("CheckBuffDuration");
        GetComponent<Animator>().SetBool("walk", false);
    }

    /// <summary>
    /// 回家
    /// </summary>
    public void BackHome()
    {
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 重新计算速度
    /// </summary>
    public void ChangeSpeed(int num)
    {
        print(num);
        float speedAdd = num / 100f;
        tweener.timeScale += speedAdd;
    }

    #region BUFF

    /// <summary>
    /// buff增加时回调
    /// </summary>
    /// <param name="baseBuff"></param>
    public void AddBuff(BaseBuff baseBuff)
    {
        for (int i = 0; i < buffList.Count; i++)
        {
            if (buffList[i].buffId == baseBuff.buffId)
                return;
        }
        buffList.Add(baseBuff);
        baseBuff.ConsumerBuffAdd();
    }

    /// <summary>
    /// buff删除时回调
    /// </summary>
    /// <param name="baseBuff"></param>
    public void RemoveBuff(BaseBuff baseBuff)
    {
        baseBuff.ConsumerBuffRemove();
        buffList.Remove(baseBuff);
    }

    /// <summary>
    /// 检测所有buff的持续时间
    /// </summary>
    public void CheckBuffDuration()
    {
        if (buffList.Count == 0)
        {
            return;
        }
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
    }

    #endregion

    private void Update()
    {
        if (isStart)
        {
            LookAtHome();
        }
    }
}
