using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using static GameEnum;
using static DataEnum;
using DT.Fight.Bullet;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ConsumeSign : MonoBehaviour,ICloneable
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

    public int tasteKillAdd = 0;

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
        for (int i = 0; i < BaseLevelController.My.consumerBuffList.Count; i++)
        {
            BaseBuff buff = BaseLevelController.My.consumerBuffList[i].CopyNew();
            buff.SetConsumerBuff(this);
        }
        if (PlayerData.My.cheatIndex2)
            consumeData.maxHealth = (int)(consumeData.maxHealth * 0.5f);
        GameObject go = Instantiate(hudPrb, transform);
        if (PlayerData.My.isPrediction)
        {
            foreach(Transform tran in GetComponentsInChildren<Transform>()){//遍历当前物体及其所有子物体
                tran.gameObject.layer = 11;//更改物体的Layer层
            }
        }
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
                    if (!PlayerData.My.isPrediction)
                    {
                        debuffEffectList[i].SetActive(true);
                        debuffEffectList[i].GetComponent<ParticleSystem>().Play();
                    }
                }
            }
        }
        else
        {
            if (!PlayerData.My.isPrediction)
            {
                GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/BuffEffect/" + buffID.ToString()), transform);
                go.transform.localPosition = Vector3.zero;
            }
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
                if (!PlayerData.My.isPrediction)
                {
                    debuffEffectList[i].GetComponent<ParticleSystem>().Stop();
                    debuffEffectList[i].SetActive(false);
                }
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
        if (CommonParams.fteList.Contains(SceneManager.GetActiveScene().name))
        {
            foreach (Transform t in paths)
            {
                pathList.Add(t.position + new Vector3(0,0.1f,0));
            }
        }
        else
        {
            foreach (Transform t in paths)
            {
                float x = UnityEngine.Random.Range(-0.3f, 0.3f);
                float z = UnityEngine.Random.Range(-0.3f, 0.3f);
                pathList.Add(t.position + new Vector3(x,0.1f,z));
            }
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
        if (!CommonParams.fteList.Contains(SceneManager.GetActiveScene().name))
        {
            BaseLevelController.My.CountKillNumber(this);
        }
        /*if(SceneManager.GetActiveScene().name == "FTE_0-1")
        {
            FTE_0_OtherOp.My.InstantiateFlyMoney(transform.position);
        }*/

        if (SceneManager.GetActiveScene().name.Equals("FTE_1.6"))
        {
            T5_Manager.My.AddKillNum(buildingIndex);
        }
        if (SceneManager.GetActiveScene().name.Equals("FTE_3.5"))
        {
            T7_Manager.My.AddKillNum(buildingIndex);
        }

        if (SceneManager.GetActiveScene().name.Equals("FTE_4.5") )
        {
            T8Manager.My.CheckTasteKill(buildingIndex);
        }

        if (SceneManager.GetActiveScene().name.Equals("FTE_0"))
        {
            T_N0_Manager.My.isTasteKill = true;
        }
        DeathAward();
        Stop();
        GetComponent<Animator>().SetBool("IsDead", true);
        if(!PlayerData.My.isPrediction)
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
        LivePunish();
        Stop();
        Destroy(gameObject);
    }

    /// <summary>
    /// 生命值检测
    /// </summary>
    public virtual void HealthCheck()
    {
        float per = Mathf.Min(1f, currentHealth / (float)consumeData.maxHealth);
        hud.UpdateInfo(per);
        if (currentHealth >= consumeData.maxHealth)
        {
            if (currentHealth >= consumeData.maxHealth * 1.3)
            {
                consumeData.killMoney = (int) (consumeData.killMoney* 1.2f);
            }
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
            currentHealth += num;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }
            HealthCheck();
        }
    }

    /// <summary>
    /// 击杀奖励
    /// </summary>
    public virtual void DeathAward()
    {
        int baseScore = consumeData.killSatisfy;
        StageGoal.My.GetSatisfy(baseScore);
        
        StageGoal.My.GetHealth(consumeData.liveSatisfy);
        StageGoal.My.ScoreGet(ScoreType.消费者得分, consumeData.killSatisfy);
        if (scorePer > 1f)
        {
            //StageGoal.My.ConsumerExtraPerTip();
            DataUploadManager.My.AddData(消费者_口味击杀);
            StageGoal.My.ScoreGet(ScoreType.口味额外得分, (int)(baseScore * (scorePer - 1f)));
            SkillCheckManager.My.AddKillNum(lastHitType != BulletType.NormalPP, true);
        }
        else
        {
            SkillCheckManager.My.AddKillNum(lastHitType != BulletType.NormalPP, false);
        }
        int baseGold = consumeData.killMoney;
        StageGoal.My.GetPlayerGold(baseGold); 
        StageGoal.My.Income(baseGold, IncomeType.Consume);
        if (PlayerData.My.isPrediction)
        {
            StageGoal.My.predictKillNum++;
        }
        else
        {
            StageGoal.My.killNumber++;
        }
    }

    /// <summary>
    /// 存活惩罚
    /// </summary>
    public virtual void LivePunish()
    {
        int baseGold = consumeData.killMoney * currentHealth / consumeData.maxHealth / 2;
        StageGoal.My.GetPlayerGold(baseGold);
        StageGoal.My.Income(baseGold, IncomeType.Consume);
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
        tweener = transform.DOPath(pathList.ToArray(), time,PathType.Linear, PathMode.Full3D).OnComplete(OnAlive).SetEase(Ease.Linear).SetOptions(AxisConstraint.None,AxisConstraint.Z|AxisConstraint.X).SetLookAt(0f);
        CheckBuffDuration();
    }

    public bool isBlocked = false;

    /// <summary>
    /// 被阻挡
    /// </summary>
    public void Block()
    {
        tweener.Pause();
        isBlocked = true;
        transform.DOMove(transform.position, 1f).OnComplete(() => {
            tweener.Play();
            isBlocked = false;
        });
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
        if (PlayerData.My.isPrediction)
        {
            return;
        }
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

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
