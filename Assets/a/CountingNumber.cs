using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CountingNumber : MonoBehaviour
{
    /// <summary>
    /// 动画速度类型枚举
    /// </summary>
    public enum SpeedType
    {
        Linear,

        EaseIn,

        EaseOut
    }
    /// <summary>
    /// 3种速度类型
    /// </summary>
    public SpeedType speedType;

    /// <summary>
    /// 文字组件
    /// </summary>
    public Text targetText;

    /// <summary>
    /// 3种速度曲线
    /// </summary>
    public List<AnimationCurve> curveList;

    /// <summary>
    /// 控制动画同时只能播放一次
    /// </summary>
    private bool isRun = false;

    public void ChangeTo(int target, float duration)
    {
        int originalNum;
        try
        {
            originalNum = int.Parse(targetText.text);
            if (originalNum == target || isRun)
                return;
        }
        catch (Exception ex)
        {
            originalNum = 0;
        }
        StartCoroutine(ChangeText(duration, originalNum, target));
    }

    /// <summary>
    /// 文字变更协程调用
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="original"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    IEnumerator ChangeText(float duration,int original,int target)
    {
        isRun = true;
        AnimationCurve currentCurve;
        switch (speedType)
        {
            case SpeedType.EaseIn:
                currentCurve = curveList[1];
                break;
            case SpeedType.EaseOut:
                currentCurve = curveList[2];
                break;
            default:
                currentCurve = curveList[0];
                break;
        }
        int total = Mathf.Abs(target - original);
        float perIncrease = duration / total;
        float count = 0;
        while (count < duration)
        {
            count += perIncrease;
            if (original > target)
            {
                
                targetText.text = ((int)(original - total * currentCurve.Evaluate(count / duration))).ToString();
            }
            else
            {
                targetText.text = ((int)(original + total * currentCurve.Evaluate(count / duration))).ToString();
            }
            float time = Mathf.Min(perIncrease, Time.deltaTime);
            yield return new WaitForSeconds(time);
        }
        targetText.text = target.ToString();
        isRun = false;
    }

    /// <summary>
    /// 测试用方法
    /// </summary>
    public void TestFunc()
    {
        ChangeTo(200, 3f);
    }
}