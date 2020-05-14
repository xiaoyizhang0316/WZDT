using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Test_1 : MonoBehaviour
{
    public Transform etg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        etg.LookAt(new Vector3(transform.position.x,transform.position.y,transform.position.z),Vector3.up);
    }
}
