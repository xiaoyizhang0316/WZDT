using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static GameEnum;

public class MapSign : MonoBehaviour,IDragHandler
{
    public MapType mapType;
    public  float sensitivityAmt = 1;

    public bool isCanPlace = true;

    // Start is called before the first frame update
    private void Awake()
    {
         MapManager.My._mapSigns.Add(this);
    }
    public void OnDrag(PointerEventData eventData)
    {
  
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
