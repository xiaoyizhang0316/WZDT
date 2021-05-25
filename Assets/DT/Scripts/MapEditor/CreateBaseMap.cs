using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBaseMap : MonoBehaviour
{

    public int X;

    public int Y;

    public GameObject land;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j <Y; j++)
            {

                GameObject  tf =Instantiate(land,transform);
                tf.transform.position  = new Vector3(i*1.725f,0,j*1.725f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
