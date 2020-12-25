using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_RingCycleItem : MonoBehaviour
{
    public Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        //initPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition -= new Vector3(0, 0.01f, 0);
        if (transform.localPosition.y <= -3f )
        {
            transform.localPosition = new Vector3(0, -0.629823f, 0);
        }
    }
}
