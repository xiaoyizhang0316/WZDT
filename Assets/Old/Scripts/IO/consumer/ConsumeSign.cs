using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static GameEnum;


public class ConsumeSign : MonoBehaviour
{
    /// <summary>
    /// 消费者数据
    /// </summary>
    public ConsumeData consumeData;

    /// <summary>
    /// 初始默认速度
    /// </summary>
    public float startSpeed;

    /// <summary>
    /// 当前速度
    /// </summary>
    public float currentSpeed;

    /// <summary>
    /// 消费者类别
    /// </summary>
    public ConsumerType consumerType;

    /// <summary>
    /// 质量要求
    /// </summary>
    public int qualityNeed;

    /// <summary>
    /// 目标商店位置
    /// </summary>
    public Transform target;

    /// <summary>
    /// 目标商店
    /// </summary>
    public BaseMapRole targetShop;

    /// <summary>
    /// DOTWeen
    /// </summary>
    public Tweener tweener;

    /// <summary>
    /// 总计花费
    /// </summary>
    public float totalPay;

    /// <summary>
    /// 总计满意度
    /// </summary>
    public float totalSatisfy;

    /// <summary>
    /// 上次购买数量
    /// </summary>
    public int lastBuyNumber;

    /// <summary>
    /// 上次购买单价
    /// </summary>
    public float lastBuySinglePrice;

    /// <summary>
    /// 上次交易成本
    /// </summary>
    public float lastTC;

    /// <summary>
    /// 上次满意度
    /// </summary>
    public float lastSatisfy;

    /// <summary>
    /// BUFF列表
    /// </summary>
    public Dictionary<int, float> buffList = new Dictionary<int, float>();

    public Dictionary<Transform, float> roleDistanceTime = new Dictionary<Transform, float>();

    public bool isStart = false;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(ConsumerType type, int _qualityNeed,int sweet,int crisp)
    {
        consumerType = type;
        qualityNeed = _qualityNeed;
        consumeData = new ConsumeData(consumerType,sweet,crisp);
        startSpeed = 1f;
        currentSpeed = startSpeed;
        totalPay = 0f;
        lastBuyNumber = 0;
        lastBuySinglePrice = 0f;
        totalSatisfy = 0f;
        lastSatisfy = 0f;
    }

    public void ActiveAndMove(BaseMapRole targetRole)
    {
        targetShop = targetRole;
        target = targetRole.transform;
        float waitTime = UnityEngine.Random.Range(0f, 2f);
        transform.DOLookAt(target.position, 0f);
        Invoke("MoveToShop", waitTime);
        Invoke("OverTimeBackHome", consumeData.searchDistance + waitTime);
        //MoveToShop();
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
        //Stop();
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

    /// <summary>
    /// 去目标的角色商店    
    /// </summary>
    public void MoveToShop()
    {
        GetComponent<Animator>().SetBool("walk", true);
        isStart = true;
        transform.DOLookAt(target.position, 0.5f);
        tweener = transform.DOMove(target.position, Vector3.Distance(this.transform.position, target.position) / currentSpeed).OnComplete(() => Shopping(targetShop)).SetEase(Ease.Linear);

    }

    /// <summary>
    /// 超时自动回家
    /// </summary>
    public void OverTimeBackHome()
    {
        Stop();
        print("超时自动回家");
        float waitTime = UnityEngine.Random.Range(0.5f, 1.5f);
        Invoke("BackHome", waitTime);
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
    /// 消费者进店购物结算
    /// </summary>
    /// <param name="targetShop"></param>
    public void Shopping(BaseMapRole targetShop)
    {
        GetComponent<Animator>().SetBool("walk", false);
        CancelInvoke("OverTimeBackHome");
        int maxProIndex = -1;
        targetShop.OnConsumerInShop(ref consumeData);
        int money = 0;
        ///口味
        if (consumeData.preference == 0)
        {
            if (targetShop.shop.Count == 0)
            {
                print("没货返回");
                CalculateSatisfy(targetShop, money);
                return;
            }
            int maxValue = CheckMatch(targetShop.shop[0], 0);
            int temp;
            for (int i = 0; i < targetShop.shop.Count; i++)
            {
                temp = CheckMatch(targetShop.shop[i], 0);
                maxProIndex = 0;
                if (temp < maxValue)
                {
                    maxValue = temp;
                    maxProIndex = i;
                }
            }

            if (maxValue <= 4)
            {
                money = BuyProduct(0, targetShop.shop[maxProIndex], targetShop);
            }
            else if (maxValue <= 8)
            {
                money = BuyProduct(1, targetShop.shop[maxProIndex], targetShop);
            }
            else if (maxValue <= 12)
            {
                money = BuyProduct(2, targetShop.shop[maxProIndex], targetShop);
            }
            CalculateSatisfy(targetShop, money);
        }
        ///价格
        ///
        else if (consumeData.preference == 1)
        {
            if (targetShop.shop.Count == 0)
            {
                CalculateSatisfy(targetShop, money);
                return;
            }
            maxProIndex = 0;
            int maxValue = CheckMatch(targetShop.shop[0], 1);
            int temp;
            for (int i = 0; i < targetShop.shop.Count; i++)
            {
                temp = CheckMatch(targetShop.shop[i], 1);
                if (temp < maxValue)
                {
                    maxValue = temp;
                    maxProIndex = i;
                }
            }
            if (maxValue <= 10)
            {
                money = BuyProduct(0, targetShop.shop[maxProIndex], targetShop);
            }
            else if (maxValue > 10 && maxValue <= 20)
            {
                money = BuyProduct(1, targetShop.shop[maxProIndex], targetShop);
            }
            else if (maxValue > 20 && maxValue <= 30)
            {
                money = BuyProduct(2, targetShop.shop[maxProIndex], targetShop);
            }
            CalculateSatisfy(targetShop, money);
        }
    }

    /// <summary>
    /// 将单个产品与消费者进行匹配
    /// </summary>
    /// <param name="p"></param>
    /// <param name="preference"></param>
    /// <returns></returns>
    public int CheckMatch(ProductData p, int preference)
    {
        if (p.productType == consumeData.needProductType)
        {
            if (p.Quality < qualityNeed)
            {
                //print("质量达不到要求");
                return 10000;
            }
            ///口味
            if (preference == 0)
            {
                return Math.Abs(consumeData.needCrisp - p.Crisp) + Math.Abs(consumeData.needSweetness - p.Sweetness);
            }
            ///价格
            else
            {
                int price = (int)((p.Quality + p.Brand) / 2 * StageGoal.My.qualityRecognition)  ;
                return (price - consumeData.mentalPrice);
            }
        }
        return 10000;
    }

    /// <summary>
    /// 满意度结算
    /// </summary>
    /// <param name="type"></param>
    public void CalculateSatisfy(BaseMapRole mapRole, int money)
    {
        float result;
        if (money != 0)
        {
            result = (money) - (CalculateTC(mapRole));
        }
        else
        {
            result = 0 - (CalculateTC(mapRole) / 2);
        }
        mapRole.OnConsumerSatisfy(ref result);
        mapRole.GetConsumerSatisfy(result);
        lastSatisfy = result;
        totalSatisfy += result;
        float waitTime = UnityEngine.Random.Range(2f, 5f);
        Invoke("BackHome",waitTime);
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
    /// 计算消费者和商店之间的交易成本
    /// </summary>
    /// <returns></returns>
    public float CalculateTC(BaseMapRole mapRole)
    {
        float result = 0f;
      // result += (float)(consumeData.search + mapRole.baseRoleData.search) ;
      // result += (float)(consumeData.bargain + mapRole.baseRoleData.bargain);
      // result += (float)(consumeData.delivery + mapRole.baseRoleData.delivery) ;
        result = result / 1f;
        lastTC = result;
        //print("交易成本：" + result);
        return (result);
    }

    /// <summary>
    /// 购买商品
    /// </summary>
    /// <param name="type"></param>
    /// <param name="p"></param>
    /// <param name="mapRole"></param>
    public int BuyProduct(int type, ProductData p, BaseMapRole mapRole)
    {
        float heat = StageGoal.My.customerSatisfy / StageGoal.My.maxCustomerSatisfy;
        int heatNum;
        float per = 0f;
        if (heat < 0.2f)
        {
            heatNum = UnityEngine.Random.Range(8, 13);
        }
        else if (heat < 0.6f)
        {
            heatNum = UnityEngine.Random.Range(9, 13);
        }
        else if (heat < 0.9f)
        {
            heatNum = UnityEngine.Random.Range(10, 14);
        }
        else
        {
            heatNum = UnityEngine.Random.Range(11, 15);
        }
        switch(type)
        {
            case 0:
                per = 1.1f;
                break;
            case 1:
                per = 1f;
                break;
            case 2:
                per = 0.9f;
                break;
        }
        int price = (int)((p.Quality + p.Brand ) / 2 * StageGoal.My.qualityRecognition);
        int buyNum = (int)(consumeData.buyPower * heatNum);
        buyNum = mapRole.LessenGoods(p, buyNum);
        mapRole.GetConsumerMoney(buyNum * price);
        lastBuyNumber = buyNum;
        lastBuySinglePrice = price;
        totalPay += buyNum * price * per;
        return (int)(buyNum * price * per);
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
