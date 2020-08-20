using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MoveBetweenTwoPoint : BaseTween
{
    /// <summary>
    /// 起始位置X
    /// </summary>
    public float startX;

    /// <summary>
    /// 起始位置Y
    /// </summary>
    public float startY;

    /// <summary>
    /// 终止位置X
    /// </summary>
    public float endX;

    /// <summary>
    /// 终止位置Y
    /// </summary>
    public float endY;

    /// <summary>
    /// 动画时间
    /// </summary>
    public float tweTime;

    /// <summary>
    /// 等待时间
    /// </summary>
    public float waitTime;


    public override void Move()
    {
        GetComponent<RectTransform>().DOAnchorPos(new Vector2(endX, endY), tweTime).SetEase(Ease.Linear).Play().OnComplete(() =>
        {
            GetComponent<Image>().DOFade(0f, 0f);
            transform.DOScale(transform.localScale, waitTime).OnComplete(() =>
            {
                GetComponent<Image>().DOFade(1f, 0f);
                GetComponent<RectTransform>().anchoredPosition = new Vector2(startX, startY);
                Move();
            });
        });
    }
}
