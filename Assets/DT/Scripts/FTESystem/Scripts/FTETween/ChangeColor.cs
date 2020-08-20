using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChangeColor : BaseTween
{
    public Color selectColor;

    public float waitTime;

    public override void Move()
    {
        GetComponent<Image>().DOColor(selectColor, waitTime).SetEase(Ease.Linear).Play().OnComplete(() =>
        {
            GetComponent<Image>().DOColor(Color.white, waitTime).SetEase(Ease.Linear).Play().OnComplete(Move);
        });
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
