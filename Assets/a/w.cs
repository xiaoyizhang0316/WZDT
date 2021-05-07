using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class w : MonoBehaviour
{
    public  GameObject prb;

    public GameObject tf;

    public int x;
    public int z;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++)
            {
            GameObject   gameObject =  Instantiate(prb, tf.transform);
            gameObject.transform.localPosition = new Vector3(i,0,j);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
