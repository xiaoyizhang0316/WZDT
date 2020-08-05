using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LookA : MonoBehaviour
{
    public GameObject target;

    public GameObject pao;

    public Vector3 lastPos;

    public GameObject body;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnGUI()
    {
        //if (GUILayout.Button("123"))
        //{
      
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInParent<BaseMapRole>().shootTarget != null&& GetComponentInParent<BaseMapRole>().tradeList.Count>0 )
        {
            target = GetComponentInParent<BaseMapRole>().shootTarget.gameObject;
            lastPos = target.transform.position;
            body.transform.DOLookAt(target.transform.position, 0.02f, AxisConstraint.Y);
            pao.transform.DOLookAt(target.transform.position + new Vector3(0f,1f,0f), 0.02f, AxisConstraint.W);
        }
        //else
        //{
        //    pao.transform.DOLookAt(lastPos + new Vector3(0f, 2f, 0f), 0.02f, AxisConstraint.W);
        //}
    }
}
