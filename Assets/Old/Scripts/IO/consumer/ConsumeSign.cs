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
    /// BUFF列表
    /// </summary>
    public Dictionary<int, float> buffList = new Dictionary<int, float>();

    public bool isStart = false;

    public bool isCanSelect = false;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(ConsumerType type)
    {
        consumerType = type;
        consumeData = new ConsumeData(consumerType);
    }

    public void InitAndMove(BaseMapRole targetRole)
    {
        currentHealth = 0;
        targetShop = targetRole;
        float waitTime = UnityEngine.Random.Range(0f, 2f);
        transform.DOLookAt(targetShop.transform.position, 0f);
        Invoke("MoveToShop", waitTime);
        if (consumeData.liveTime > 0)
        {
            Invoke("OnAlive", consumeData.liveTime + waitTime);
        }
    }

    public void OnHit(ProductData data)
    {

    }

    public void OnDeath()
    {
        CancelInvoke("OnAlive");
        DeathBackHome();
        Stop();
    }

    public void OnAlive()
    {
        AliveBackHome();
    }

    /// <summary>
    /// 去目标的角色商店    
    /// </summary>
    public void MoveToShop()
    {
        GetComponent<Animator>().SetBool("walk", true);
        isStart = true;
        isCanSelect = true;
        transform.DOLookAt(targetShop.transform.position, 0.5f);
        tweener = transform.DOMove(targetShop.transform.position, Vector3.Distance(transform.position, targetShop.transform.position) / consumeData.moveSpeed).OnComplete(() => OnAlive()).SetEase(Ease.Linear);
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
        float waitTime = UnityEngine.Random.Range(0.5f, 1.5f);
        Invoke("BackHome", waitTime);
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
    /// 增加buff
    /// </summary>
    /// <param name="id"></param>
    /// <param name="effect"></param>
    public void AddBuff(int id, float effect)
    {
        if (isStart)
        {
            if (buffList.ContainsKey(id))
            {
                if (Mathf.Abs(buffList[id] - effect) > 0.01f)
                {
                    buffList[id] = effect;
                    RecheckBuff();
                }
                else
                    return;
            }
            else
            {
                buffList.Add(id, effect);
                RecheckBuff();
            }
        }
    }

    /// <summary>
    /// 删除buff
    /// </summary>
    /// <param name="id"></param>
    public void RemoveBuff(int id)
    {
        if (isStart)
        {
            if (buffList.ContainsKey(id))
            {
                buffList.Remove(id);
                RecheckBuff();
            }
        }
    }

    /// <summary>
    /// 重新计算速度
    /// </summary>
    public void RecheckBuff()
    {
        float speedAdd = 1f;
        if (buffList.Count > 0)
        {
            foreach (var v in buffList)
            {
                speedAdd += v.Value;
            }
        }
        tweener.timeScale = speedAdd;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
