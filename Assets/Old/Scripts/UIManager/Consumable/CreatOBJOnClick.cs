﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreatOBJOnClick : MonoBehaviour,IPointerClickHandler
{

    public GameObject roadblock;
    public GameObject bomb;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    Debug.Log("ConsumableListManager.My.isClick"+ConsumableListManager.My.isClick);
        if (ConsumableListManager.My.isClick)
        {
            Debug.Log("释放" + GameDataMgr.My.GetConsumableDataByID(ConsumableListManager.My.currentSign. consumableId).consumableType);
            ConsumableListManager.My.isClick = false;


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hit = Physics.RaycastAll(ray);
            Debug.Log(hit.Length);
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].transform.tag.Equals("MapRole"))
                {
                    if ( GameDataMgr.My.GetConsumableDataByID(ConsumableListManager.My.currentSign. consumableId).consumableType ==
                        GameEnum.ConsumableType.Role)
                    {
                        BaseMapRole role = hit[i].transform.GetComponentInParent<BaseMapRole>();
                      
                        ConsumableListManager.My.currentSign. InitBuff();
                        ConsumableListManager.My.currentSign.CastBuff(role);
                        Destroy(gameObject);
                        break;
                    }
                }


                if (hit[i].transform.tag.Equals("MapLand"))
                {
                    Debug.Log("Mapland");
                    if (hit[i].transform.GetComponent<MapSign>().mapType == GameEnum.MapType.Road)
                    {
                        if ( GameDataMgr.My.GetConsumableDataByID(ConsumableListManager.My.currentSign. consumableId).consumableType ==
                            GameEnum.ConsumableType.SpawnItem)
                        {
                            Debug.Log("SpawnItem");

                            print(hit[i].point);
                            GameObject go1 = Instantiate(roadblock);
                            go1.transform.position = hit[i].transform.position;
                            //BaseMapRole role = hit[i].transform.GetComponentInParent<BaseMapRole>();
                            //print("使用消耗品:" + consumableId.ToString());
                            //InitBuff();
                            //CastBuff(role);
                            PlayerData.My.UseConsumable(ConsumableListManager.My.currentSign. consumableId);
                            ConsumableListManager.My.currentSign. CheckDelete();
                            Destroy(gameObject);
                            break;
                        }
                    }

                    if (GameDataMgr.My.GetConsumableDataByID(ConsumableListManager.My.currentSign. consumableId).consumableType ==
                        GameEnum.ConsumableType.AOE)
                    {
                        print(hit[i].point);
                        GameObject go1 = Instantiate(bomb);
                        go1.transform.position = hit[i].point;
                        //BaseMapRole role = hit[i].transform.GetComponentInParent<BaseMapRole>();
                        //print("使用消耗品:" + consumableId.ToString());
                        //InitBuff();
                        //CastBuff(role);
                        PlayerData.My.UseConsumable(ConsumableListManager.My.currentSign. consumableId);
                        ConsumableListManager.My.currentSign.CheckDelete();
                        Destroy(gameObject);
                        break;
                    }
                }

                //  if(//怼人)
            }
        }
    }
}