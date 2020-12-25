using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Red : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalJump(Vector3.up,10,3,1).SetLoops(-1);
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
