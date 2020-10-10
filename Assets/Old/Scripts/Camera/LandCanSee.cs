using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandCanSee : MonoBehaviour
{
    public bool iscansee;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        iscansee = false;
    }

    private void OnBecameVisible()
    {
        iscansee = true;
    }
}
