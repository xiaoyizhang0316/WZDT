using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T8Manager : MonoSingleton<T8Manager>
{
    public GameObject npcmaoyi;

    public GameObject consumerPort1;
    public GameObject consumerPort2;

    public GameObject endPointPort1;
    public GameObject endPointPort2;

    public GameObject ggj;
    public GameObject dlg;
    public GameObject tsg;
    // Start is called before the first frame update
    void Start()
    {
        ggj.SetActive(true);
        dlg.SetActive(false);
        tsg.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
