using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TipsSign : MonoBehaviour
{
    public GameObject target;

    public int showTime = 30;
    // Start is called before the first frame update
    void Start()
    {
     
   
    }

    // Update is called once per frame
    void Update()
    {
        //if (GetComponentInParent<BaseGuideStep>().timeCount>1500)
        //{
        //    transform.DOLocalMove(target.transform.localPosition,0.5f).Play();
        //}
    }

    public void Check()
    {
       
    }

    public void Show()
    {
        transform.DOLocalMove(target.transform.localPosition, 0.5f).Play();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnEnable()
    {
        Invoke("Show",showTime);
    }
}
