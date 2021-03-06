using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static GameEnum;
using static DataEnum;
using DT.Fight.Bullet;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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

    public List<int> bornBuffList = new List<int>();

    public bool isStart = false;

    public bool isCanSelect = false;

    public List<Vector3> pathList;

    public BaseConsumer baseConsumer;

    public List<GameObject> debuffEffectList = new List<GameObject>();

    public GameObject self;

    public GameObject sheep;

    public GameObject spriteLogo;

    public BulletType lastHitType;

    public int buildingIndex;

    public int consumerIndex;

    public int enterMarketingTime = 0;

    private float scorePer = 1f;

    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void Init(List<Transform> paths)
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
    public virtual void InitEffect()
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
    public virtual void AddEffect(int buffID)
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
    public virtual void RemoveEffect(int buffID)
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
    public virtual void InitAndMove()
    {
        //float waitTime = UnityEngine.Random.Range(0f, 0.5f);
        //Invoke("Move", waitTime);
        Move();
    }

    /// <summary>
    /// 初始化路径点
    /// </summary>
    /// <param name="paths"></param>
    public virtual void InitPath(List<Transform> paths)
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
    public virtual void OnHit(ref ProductData data)
    {
        if (isCanSelect)
        {
            lastHitType = data.bulletType;
            CheckAttackEffect(ref data);
            CheckProduct(ref data);
            int realDamage = (int)data.damage;
            CheckBulletElement(ref realDamage, data);
            CheckDebuff(data);
            ChangeHealth(realDamage);
            if (transform.TryGetComponent(out Animator ani))
                ani.SetBool("OnHit", true);
        }
    }

    public void CheckProduct(ref ProductData data)
    {
        BaseCSB[] buffs = GetComponentsInChildren<BaseCSB>();
        foreach (var item in buffs)
        {
            item.OnProduct(ref data);
        }
    }

    /// <summary>
    /// 消费者被击杀时调用   
    /// </summary>
    public virtual void OnDeath()
    {
        foreach (BaseBuff b in buffList)
        {
            b.OnConsumerBeforeDead();
        }
        if (currentHealth < consumeData.maxHealth)
        {
            return;
        }
        if (SceneManager.GetActiveScene().name != "FTE_0-2" && SceneManager.GetActiveScene().name != "FTE_0-1")
        {
            BaseLevelController.My.CountKillNumber(this);
        }
        if(SceneManager.GetActiveScene().name == "FTE_0-1")
        {
            FTE_0_OtherOp.My.InstantiateFlyMoney(transform.position);
        }
        DeathAward();
        Stop();
        GetComponent<Animator>().SetBool("IsDead", true);
        ComboManager.My.AddComboNum();
        if (!PlayerData.My.isSOLO)
        {
            string str = "ConsumerDead|";
            str += buildingIndex.ToString() + "," + consumerIndex.ToString() + ",";
            str += ((int)(consumeData.killSatisfy * scorePer)).ToString() + ",";
            str += consumeData.killMoney.ToString();
            if (PlayerData.My.isServer)
            {
                PlayerData.My.server.SendToClientMsg(str);
            }
        }
    }

    /// <summary>
    /// 消费者存活时调用
    /// </summary>
    public virtual void OnAlive()
    {
        if (PlayerData.My.qiYeJiaZhi[4])
        {
            int number = UnityEngine.Random.Range(0, 101);
            if (number <= 30)
            {
                tweener.Restart();
                return;
            }
        }
        LivePunish();
        Stop();
        Destroy(gameObject);
    }

    /// <summary>
    /// 生命值检测
    /// </summary>
    public virtual void HealthCheck()
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
    public virtual void ChangeHealth(int num)
    {
        if (isCanSelect)
        {
            List<ConsumerType> lists = new List<ConsumerType> { ConsumerType.OldpaoLegendary,ConsumerType.GoldencollarLegendary,ConsumerType.EliteLegendary,
                ConsumerType.BluecollarLegendary,ConsumerType.WhitecollarLegendary};
            if (lists.Contains(consumerType) && PlayerData.My.dingWei[3])
            {
                num = num * 120 / 100;
            }
            if (PlayerData.My.yingLiMoShi[5])
            {
                int gold = (int)(num * 0.45f);
                StageGoal.My.GetPlayerGold(gold);
                StageGoal.My.Income(gold, IncomeType.Consume);
            }
            currentHealth += num;
            if (currentHealth <= 0)
                currentHealth = 0;
            HealthCheck();
        }
    }

    /// <summary>
    /// 击杀奖励
    /// </summary>
    public virtual void DeathAward()
    {
        int baseScore = consumeData.killSatisfy;
        if (PlayerData.My.qiYeJiaZhi[0])
        {
            baseScore = baseScore * 110 / 100;
        }

        StageGoal.My.GetSatisfy(baseScore);
        StageGoal.My.ScoreGet(ScoreType.消费者得分, consumeData.killSatisfy);
        if (scorePer > 1f)
        {
            if (PlayerData.My.qiYeJiaZhi[1])
            {
                scorePer *= 1.2f;
            }
            if (PlayerData.My.qiYeJiaZhi[2])
            {
                StageGoal.My.LostHealth(2);
            }
            StageGoal.My.ConsumerExtraPerTip();
            DataUploadManager.My.AddData(消费者_口味击杀);
            StageGoal.My.ScoreGet(ScoreType.口味额外得分, (int)(baseScore * (scorePer - 1f)));
        }
        int baseGold = consumeData.killMoney;
        if (PlayerData.My.yingLiMoShi[0])
        {
            baseGold = baseGold * 110 / 100;
        }
        if (!PlayerData.My.yingLiMoShi[5])
        {
            StageGoal.My.GetPlayerGold(baseGold);
            StageGoal.My.Income(baseGold, IncomeType.Consume);
        }
        StageGoal.My.killNumber++;
    }

    /// <summary>
    /// 存活惩罚
    /// </summary>
    public virtual void LivePunish()
    {
        StageGoal.My.LostHealth(consumeData.liveSatisfy);
        StageGoal.My.GetSatisfy((consumeData.killSatisfy * currentHealth / consumeData.maxHealth));
        StageGoal.My.ScoreGet(ScoreType.消费者得分, consumeData.killSatisfy * currentHealth / consumeData.maxHealth);
        StageGoal.My.ConsumerAliveTip();
    }

    /// <summary>
    /// 结算属性抗性
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="data"></param>
    public virtual void CheckBulletElement(ref int damage,ProductData data)
    {
        float per = 1f;
        bool isNormal = true;
        foreach(int i in data.buffList)
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
        if (per < 1f && PlayerData.My.dingWei[2])
        {
            per = Mathf.Min(0.9f, per + 0.1f);
        }
        scorePer = Mathf.Max(1f,per);
        damage = (int)(damage * per);
    }

    /// <summary>
    /// 检测攻击特效
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="data"></param>
    public virtual void CheckAttackEffect(ref ProductData data)
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
                    AddEffect(i);
                }
            }
        }
    }

    /// <summary>
    /// 检测debuff
    /// </summary>
    public virtual void CheckDebuff(ProductData data)
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
    public virtual void Move()
    {
        GetComponent<Animator>().SetFloat("Speed_f",consumeData.moveSpeed);
        isStart = true;
        isCanSelect = true;
        float time = CalculateTime();
        tweener = transform.DOPath(pathList.ToArray(), time,PathType.CatmullRom, PathMode.Full3D).OnComplete(OnAlive).SetEase(Ease.Linear).SetOptions(AxisConstraint.None,AxisConstraint.Z|AxisConstraint.X).SetLookAt(0f);
        CheckBuffDuration();
    }

    /// <summary>
    /// 停止
    /// </summary>
    public virtual void Stop()
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
    public virtual float CalculateTime()
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
    public virtual void ChangeSpeed(int num)
    {
        float speedAdd = num / 100f;
        tweener.timeScale += speedAdd;
        if (!PlayerData.My.isSOLO)
        {
            string str = "ConsumerChangeSpeed|";
            str += gameObject.GetInstanceID().ToString() + ",";
            str += speedAdd.ToString();
            if (PlayerData.My.isServer)
            {
                PlayerData.My.server.SendToClientMsg(str);
            }
        }
        //print("移动速度：" + tweener.timeScale.ToString());
    }

    #region BUFF

    /// <summary>
    /// buff增加时回调
    /// </summary>
    /// <param name="baseBuff"></param>
    public virtual void AddBuff(BaseBuff baseBuff)
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
    public virtual void RemoveBuff(BaseBuff baseBuff)
    {
        baseBuff.ConsumerBuffRemove();
        RemoveEffect(baseBuff.buffId);
        buffList.Remove(baseBuff);
    }

    /// <summary>
    /// 检测所有buff的持续时间
    /// </summary>
    public virtual void CheckBuffDuration()
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
    /// <summary>
    /// 鼠标点击弹出浮动窗口显示消费者信息
    /// </summary>
    public virtual void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        NewCanvasUI.My.consumerInfoFloatWindow.SetActive(true);
        NewCanvasUI.My.consumerInfoFloatWindow.GetComponent<ConsumerFloatWindow>().Init(this);
    }

    private void Update()
    {
        //print(tweener.ElapsedPercentage(false));
        if (PlayerData.My.creatRole == PlayerData.My.playerDutyID)
        {
            spriteLogo.gameObject.SetActive(false);
            hud.gameObject.SetActive(true);
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
        else
        {
            self.SetActive(false);
            sheep.SetActive(false);
            hud.gameObject.SetActive(false);
            spriteLogo.gameObject.SetActive(true);
            spriteLogo.transform.eulerAngles = new Vector3(-90, 0, -135);
        }
        
    }

    private void Start()
    {

    }
}
