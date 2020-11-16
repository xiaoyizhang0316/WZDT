using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MaskScale : MonoBehaviour
{
    public float startScale = 1.5f;
    public float endScale = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(startScale, 0.01f).OnComplete(()=> {
            transform.DOScale(endScale, 0.75f);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
