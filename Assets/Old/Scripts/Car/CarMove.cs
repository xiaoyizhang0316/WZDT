using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    public float startSpeed;

    public float CurrentSpeed;

    public bool isStart;

    public Tweener tweener;

    public Transform target;

    Action<TradeData> action;

    public TradeData tradeData;

    public BaseMapRole BaseMapRole;
    public float actualTimePercent;

    /// <summary>
    /// BUFF列表
    /// </summary>
    public Dictionary<int, float> buffList;

    public GameObject virtualCar;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Init(BaseMapRole baseMapRole, float _CurrentSpeed, Transform _target, Action<TradeData> _action, TradeData p)
    {
        BaseMapRole = baseMapRole;
        isStart = false;
        //Debug.Log("小车初始化");
        startSpeed = _CurrentSpeed;
        CurrentSpeed = startSpeed;
        target = _target;
        buffList = new Dictionary<int, float>();
        action = _action;
        tradeData = p;
        transform.DOLookAt(_target.position, 0.3f);
        GameObject go = Instantiate(virtualCar, transform.position, transform.rotation, transform);
        go.GetComponent<VirtualCar>().Init(_target);
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
        //StopCar();
        float speedAdd = 1f;
        CurrentSpeed = startSpeed;
        if (buffList.Count > 0)
        {
            foreach (var v in buffList)
            {
                speedAdd += v.Value;
            }
        }
        tweener.timeScale = speedAdd;
        //CurrentSpeed = CurrentSpeed * speedAdd;
        //MoveCar();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private bool addSpeed;
    public void MoveCar()
    {
        isStart = true;

        if (BaseMapRole.CarCount > 0)
        {
            BaseMapRole.CarCount--;
            startSpeed *= 1.2f;
            addSpeed = true;
        }
        else
        {
            StageGoal.My.CostMoney(200f);
        }


        tweener = transform
            .DOMove(target.position, Vector3.Distance(this.transform.position, target.position) / CurrentSpeed)
            .OnComplete(() =>
            {
                action(tradeData);
                if (addSpeed)
                {
                    BaseMapRole.CarCount++;
                    startSpeed /= 1.2f;
                    addSpeed = false;
                }
            }).SetEase(Ease.Linear);
    }

    public void StopCar()
    {
        tweener.Kill();
    }
}