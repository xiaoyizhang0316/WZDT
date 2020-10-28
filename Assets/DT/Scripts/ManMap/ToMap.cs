using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(()=> {
            NetworkMgr.My.ConnectToMap();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
