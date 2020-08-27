using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChangeScale : BaseTween
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Move()
    {
        transform.DOScale(0.7f, 0.5f).Play().OnComplete(()=> {
            transform.DOScale(1f, 0.5f).Play().OnComplete(Move);
        });
    }
}
