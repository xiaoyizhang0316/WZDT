using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T6Manager : MonoSingleton<T6Manager>
{
    public GameObject npcnong;
    public GameObject npcDealer;
    public GameObject sumSpor;
    public GameObject sumSpor2;
    public GameObject endPoint;
    public GameObject endPoint2;

    public GameObject gjj;
    // Start is called before the first frame update
    void Start()
    {
        npcnong.gameObject.SetActive(true);
        npcDealer.gameObject.SetActive(false);
        sumSpor.gameObject.SetActive(false);
        sumSpor2.gameObject.SetActive(false);
        endPoint.gameObject.SetActive(false);
        endPoint2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
