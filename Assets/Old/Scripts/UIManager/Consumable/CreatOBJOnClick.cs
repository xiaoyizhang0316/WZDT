using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreatOBJOnClick : MonoBehaviour,IPointerClickHandler
{
    private GameObject currentgo;
 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ConsumableListManager.My.currentSign. consumableId"+ConsumableListManager.My.currentSign. consumableId);
        currentgo = Instantiate(Resources.Load<GameObject>("Prefabs/Consumable/"+ConsumableListManager.My.currentSign. consumableId));

    }

    // Update is called once per frame
    void Update()
    {
        if (currentgo != null)
        {
            currentgo.transform.position = transform.position;

        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(currentgo,0f);
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
                        GameObject go  = Instantiate(Resources.Load<GameObject>("Prefabs/Consumable/"+ConsumableListManager.My.currentSign. consumableId));
                        go.transform.position = role.transform.position;
                         go.GetComponent<BaseSpawnItem>().Init(GameDataMgr.My.GetConsumableDataByID(ConsumableListManager.My.currentSign. consumableId).targetBuffList[0]);

                         for (int j = 0; j <PlayerData.My.MapRole.Count; j++)
                         {
                             PlayerData.My.MapRole[j].TradeLightOff(); 
                         }
                         PlayerData.My.UseConsumable(ConsumableListManager.My.currentSign. consumableId);
                       ConsumableListManager.My.currentSign. InitBuff();
                       ConsumableListManager.My.currentSign.CastBuff(role);
                        Destroy(gameObject);
                        break;
                    }
                }

                if (GameDataMgr.My.GetConsumableDataByID(ConsumableListManager.My.currentSign. consumableId).consumableType ==
                    GameEnum.ConsumableType.AOE&&!hit[i].transform.name.Equals(name))
                { 
                    Debug.Log(hit[i].transform.name);
                    GameObject go1 = Instantiate(Resources.Load<GameObject>("Prefabs/Consumable/"+ConsumableListManager.My.currentSign. consumableId));
                    go1.GetComponent<BaseSpawnItem>().Init(ConsumableListManager.My.currentSign. consumableId);
                    go1.transform.position = hit[i].point+new Vector3(0,0.2f,0);  
                    //BaseMapRole role = hit[i].transform.GetComponentInParent<BaseMapRole>();
                    //print("使用消耗品:" + consumableId.ToString());
                    //InitBuff();
                    //CastBuff(role);
                    PlayerData.My.UseConsumable(ConsumableListManager.My.currentSign. consumableId);
                    ConsumableListManager.My.currentSign.CheckDelete();
                    Destroy(gameObject);
                    break;
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
                            GameObject go1 = Instantiate (Resources.Load<GameObject>("Prefabs/Consumable/"+ConsumableListManager.My.currentSign. consumableId) );
                            go1.transform.position = hit[i].transform.position+new Vector3(0,0.2f,0);
                            go1.GetComponentInChildren <BaseSpawnItem>().Init(ConsumableListManager.My.currentSign. consumableId);

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
                }
                Destroy(gameObject);
                //  if(//怼人)
            }
        }
    }
}
