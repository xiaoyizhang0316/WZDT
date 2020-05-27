using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static GameEnum;
using DT.Fight.Bullet;


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
    /// DOTWeen
    /// </summary>
    public Tweener tweener;

    public Tweener buffTweener;

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

    public List<Vector3> pathList;

    public BaseConsumer baseConsumer;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(List<Transform> paths)
    {
        isStart = false;
        isCanSelect = false;
        currentHealth = 0;
        foreach (ProductElementType p in Enum.GetValues(typeof(ProductElementType)))
        {
            elementResistance.Add(p, 100);
        }
        consumeData = new ConsumeData(consumerType);
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
    public void InitAndMove()
    {
        //float waitTime = UnityEngine.Random.Range(0f, 0.5f);
        //Invoke("Move", waitTime);
        Move();
    }

    /// <summary>
    /// 初始化路径点
    /// </summary>
    /// <param name="paths"></param>
    public void InitPath(List<Transform> paths)
    {
        pathList = new List<Vector3>();
        foreach (Transform t in paths)
        {
            float x = UnityEngine.Random.Range(-0.3f, 0.3f);
            float z = UnityEngine.Random.Range(-0.3f, 0.3f);
            pathList.Add(t.position + new Vector3(x,0f,z));
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
        DeathAward();
        Stop();
    }

    /// <summary>
    /// 消费者存活时调用
    /// </summary>
    public void OnAlive()
    {
        LivePunish();
        Stop();
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
        StageGoal.My.CheckWin();
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
    /// 去目标地点    
    /// </summary>
    public void Move()
    {
        GetComponent<Animator>().SetFloat("Speed_f",0.5f);
        isStart = true;
        isCanSelect = true;
        float time = CalculateTime();
        tweener = transform.DOPath(pathList.ToArray(), time,PathType.CatmullRom, PathMode.Full3D).OnComplete(OnAlive).SetEase(Ease.Linear).SetLookAt(0.01f);
        CheckBuffDuration();
    }

    /// <summary>
    /// 停止
    /// </summary>
    public void Stop()
    {
        tweener.Kill();
        buffTweener.Kill();
        GetComponent<Animator>().SetFloat("Speed_f", 0f);
        BaseMapRole[] temp = FindObjectsOfType<BaseMapRole>();
        foreach (BaseMapRole role in temp)
        {
            role.RemoveConsumerFromShootList(this);
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// 计算全程时间
    /// </summary>
    /// <returns></returns>
    public float CalculateTime()
    {
        if (pathList.Count == 0)
            throw new Exception("路径点数量为0！");
        float result = Vector3.Distance(transform.position,pathList[0]) / consumeData.moveSpeed;
        for (int i = 0; i < pathList.Count - 1; i++)
        {
            result += Vector3.Distance(pathList[i], pathList[i + 1]) / consumeData.moveSpeed;
        }
        return result;
    }

    /// <summary>
    /// 重新计算速度
    /// </summary>
    public void ChangeSpeed(int num)
    {
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

    private void Update()
    {

    }

    private void Start()
    {

    }
}
