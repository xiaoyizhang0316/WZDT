using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LookA : MonoBehaviour
{
    public GameObject target;

    public GameObject pao;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnGUI()
    {
        if (GUILayout.Button("123"))
        {
      
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.DOLookAt(target.transform.position, 0.02f, AxisConstraint.Y );
        pao.transform.DOLookAt(target.transform.position,0.02f,AxisConstraint.W );
    }
}
