using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TestTWE : MonoBehaviour
{
    public float startX;

    public float startY;

    public float endX;

    public float endY;

    public float tweTime;

    public float waitTime;

    public void Move()
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

    private void Start()
    {
        Move();
    }

}
