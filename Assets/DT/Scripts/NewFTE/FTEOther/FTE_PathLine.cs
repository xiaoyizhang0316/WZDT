using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_PathLine : MonoBehaviour
{
    float offsetX = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        offsetX -= Time.deltaTime;
        GetComponent<LineRenderer>().material.mainTextureOffset = new Vector2(offsetX,0);
    }
}
