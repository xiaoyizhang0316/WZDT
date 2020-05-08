using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;

public class DealConfigData : MonoSingletonDontDestroy<DealConfigData>
{
    /// <summary>
    /// 固定
    /// </summary>
    public List<float> fix;

    /// <summary>
    /// 剩余
    /// </summary>
    public List<float> rest;

    /// <summary>
    /// 分成
    /// </summary>
    public List<float> divide;

    /// <summary>
    /// 免费
    /// </summary>
    public List<float> free;

    /// <summary>
    /// 不免费
    /// </summary>
    public List<float> noFree;

    /// <summary>
    /// 先钱
    /// </summary>
    public List<float> moneyFirst;

    /// <summary>
    /// 后钱
    /// </summary>
    public List<float> moneyLast;

    void Start()
    {
        fix = new List<float> {-0.5f,-0.5f,-0.5f,-0.5f, -0.5f, -0.5f, -0.5f, -0.5f };
        rest = new List<float> { -0.4f, -0.5f, -0.4f, -0.1f, -0.2f, -0.3f, -0.2f, 0.3f };
        divide = new List<float> { -0.1f, -0.2f, -0.1f, -0.3f, 0.1f, 0f, 0.3f, -0.2f };
        free = new List<float> { 0f, -1f, 0f, -0.5f, 0f, -1f, 0f, -0.5f };
        noFree = new List<float> { -0.5f, -0.5f, -0.5f, -0.5f, 0f, 0f, 0f, 0f };
        moneyFirst = new List<float> { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f };
        moneyLast = new List<float> { 1.1f, 1.2f, 1.1f, 1.1f, 0.7f, 0.8f, 0.7f, 0.7f };
    }

    public List<float> GetDealConfigAdd(bool isInside,string dealConfig)
    {
        if (isInside)
        {
            switch(dealConfig)
            {
                case "固定":
                    return fix.GetRange(0, 4);
                case "剩余":
                    return rest.GetRange(0, 4);
                case "分成":
                    return divide.GetRange(0, 4);
                case "免费":
                    return free.GetRange(0, 4);
                case "不免费":
                    return noFree.GetRange(0, 4);
                case "先钱":
                    return moneyFirst.GetRange(0, 4);
                case "后钱":
                    return moneyLast.GetRange(0, 4);
            }
        }
        else
        {
            switch (dealConfig)
            {
                case "固定":
                    return fix.GetRange(4, 4);
                case "剩余":
                    return rest.GetRange(4, 4);
                case "分成":
                    return divide.GetRange(4, 4);
                case "免费":
                    return free.GetRange(4, 4);
                case "不免费":
                    return noFree.GetRange(4, 4);
                case "先钱":
                    return moneyFirst.GetRange(4, 4);
                case "后钱":
                    return moneyLast.GetRange(4, 4);
            }
        }
        return new List<float>() { 0f, 0f, 0f, 0f };
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
