using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static GameEnum;

public class BaseConsumer : MonoBehaviour
{
    public float pulseTime = 0f;

    public Tweener pulseTween;

    public virtual void OnReceiveDamage(ref int damage)
    {

    }

    public virtual void PulseFunction()
    {
        //PulseFunction
        pulseTween = transform.DOScale(1f,pulseTime).OnComplete(()=> {
            //TODO
            //PulseFunction
            PulseFunction();
        });
    }


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ConsumeSign>().baseConsumer = this;
        if (pulseTime > 0)
        {
            PulseFunction();
        }
    }
}
