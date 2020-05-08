using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IOIntensiveFramework.MonoSingleton;
using static GameEnum;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestScript : MonoBehaviour
{
    public void Testsssss()
    {
        double id1 = 0;
        double id2 = 0;
        foreach (Role r in PlayerData.My.RoleData)
        {
            if (r.baseRoleData.roleType == RoleType.Seed)
                id1 = r.ID;
        }
        foreach (Role r in PlayerData.My.RoleData)
        {
            if (r.baseRoleData.roleType == RoleType.Peasant)
            {
                id2 = r.ID;
            }
        }
        TradeManager.My.CreateTradeAI(id1, id2, "送货", ProductType.Seed, TradeDestinationType.Import);

    }
    public void Test22222()
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Trade/TradeSign"));
        go.transform.SetParent(TradeManager.My.transform);
        go.GetComponent<TradeSign>().Init(PlayerData.My.RoleData[0].ID.ToString(), PlayerData.My.RoleData[1].ID.ToString());
        UIManager.My.Panel_CreateTrade.SetActive(true);
        CreateTradeManager.My.Open(go);
        //Time.timeScale = 0;
        //CreatRoleManager.My.DeleteTemplate();
        //WorkerSign[] temp = FindObjectsOfType<WorkerSign>();
        //foreach (WorkerSign w in temp)
        //EquipListManager.My.GetComponentInChildren<VerticalLayoutGroup>();
        //LayoutRebuilder.MarkLayoutForRebuild(EquipListManager.My.transform.Find("Viewport/Content").GetComponent<RectTransform>());
    }


    public void Test()
    {
        //CreatRoleManager.My.Open(new Role());
    }
    // Start is called before the first frame update
    void Start()
    {
       //Debug.Log( JsonUtility.ToJson(new SkillsData()));
        
    }
        // Update is called once per frame
        void Update()
    {
        
    }

    public void OnMouseDown()
    {
        print("21312312312312");
    }
}
