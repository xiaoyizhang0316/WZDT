using System;
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


    public void Start()
    {
        Move();
    }

    public override void Move()
    {
        float yOffset = 16f * Screen.height / 9f / Screen.width;

        GetComponent<RectTransform>().DOAnchorPos(new Vector2(endX, endY), tweTime).Play().SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                GetComponent<Image>().DOFade(0f, 0f).Play();
                transform.DOScale(transform.localScale, waitTime).OnComplete(() =>
                {
                    GetComponent<Image>().DOFade(1f, 0f).Play();
                    GetComponent<RectTransform>().anchoredPosition = new Vector2(startX, startY);
                    Move();
                }).Play().OnPause(() => { Move(); });
            }).Play().OnPause(() => { Move(); });
    }
   
}
