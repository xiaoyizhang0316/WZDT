using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTest : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody righ;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        righ.AddForceAtPosition(new Vector3(0,0,10),new Vector3(0,0,10));

    }
}
