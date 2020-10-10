using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChangeColor : BaseTween
{
    /// <summary>
    /// 选择的颜色
    /// </summary>
    public Color selectColor;

    /// <summary>
    /// 动画时间
    /// </summary>
    public float waitTime;

    public override void Move()
    {
        GetComponent<Image>().DOColor(selectColor, waitTime).SetEase(Ease.Linear).Play().OnComplete(() =>
        {
            GetComponent<Image>().DOColor(Color.white, waitTime).SetEase(Ease.Linear).Play().OnComplete(()=> {
                Move();
            });
        });
    }

    private void Start()
    {
        Move();
    }
}
