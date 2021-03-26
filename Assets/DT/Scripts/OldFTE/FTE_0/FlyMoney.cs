using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyMoney : MonoBehaviour
{
    public void Flying(Transform target)
    { 
        transform.parent = target;
        transform.localScale = Vector3.one;
        transform.localEulerAngles = Vector3.zero;
        transform.DOLocalMove(Vector3.zero, 0.3f).OnComplete(()=> {
            transform.parent.DOScale(1.2f, 0.3f).OnComplete(()=> { transform.parent.DOScale(1, 0.3f);

            Destroy(gameObject);
            });
        });
    }
}
