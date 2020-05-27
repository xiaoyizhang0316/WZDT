using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static GameEnum;

public class MapSign : MonoBehaviour,IDragHandler
{
    public MapType mapType;

    public int x;

    public int y;

    public int height = 0;

    public bool isCanPlace = true;

    // Start is called before the first frame update
    private void Awake()
    {
         MapManager.My._mapSigns.Add(this);
         isCanPlace = GetComponent<MeshRenderer>().enabled && isCanPlace;
    }

    public void OnDrag(PointerEventData eventData)
    {
  
    }
    void Start()
    {
        if (mapType == MapType.Road)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            go.transform.position = transform.position;
            go.transform.SetParent(transform.parent.parent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
