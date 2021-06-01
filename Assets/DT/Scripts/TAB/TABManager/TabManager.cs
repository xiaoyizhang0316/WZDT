using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoSingleton<TabManager>
{
    public GameObject fade;

    public GameObject RoleUI;

    public Transform tf;

    public GameObject line;
    public Dictionary<int, Sprite> buffsprite = new Dictionary<int, Sprite>();

    /// <summary>
    /// 虚线
    /// </summary>
    public GameObject linexu;
    // Start is called before the first frame update
    void Start()
    {
        fade.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            fade.SetActive(true);

         //   fade.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0.5f), 0.2f).Play();
         //   fade.GetComponent<Image>().material.SetInt("_Size",5);
            CreatRoleUI();
            CreatRoleLine();
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            fade.SetActive(false);
            DeleteRole();
            
       //     fade.GetComponent<Image>().DOColor(new Color(1f, 1f, 1f, 1), 0.2f).Play();
//
       //     fade.GetComponent<Image>().material.SetInt("_Size",0);
        }
    }

    public void CreatRoleUI()
    {
        for (int i = 0; i <  PlayerData.My.MapRole.Count; i++)
        {
            GameObject roleUI =  Instantiate(RoleUI, tf);
            if (PlayerData.My.MapRole[i].isNpc && !PlayerData.My.MapRole[i].npcScript.isCanSee)
            {

                roleUI.GetComponent<RoleUISign>().roleicon.sprite = Resources.Load<Sprite>("Sprite/lan/unKnown");
            }
            else  if(PlayerData.My.MapRole[i].isNpc)
            {
                roleUI.GetComponent<RoleUISign>().roleicon.sprite = Resources.Load<Sprite>("Sprite/RoleLogo/"+PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType+PlayerData.My.MapRole[i].baseRoleData.baseRoleData.level);

            }
            else if (!PlayerData.My.MapRole[i].isNpc)
            {
                roleUI.GetComponent<RoleUISign>().roleicon.sprite = Resources.Load<Sprite>("Sprite/hong/"+PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType+PlayerData.My.MapRole[i].baseRoleData.baseRoleData.level);
 
            }

            roleUI.GetComponent<RoleUISign>().obj = PlayerData.My.MapRole[i].gameObject;
            roleUI.GetComponent<RoleUISign>().mapRole = PlayerData.My.MapRole[i] ;
        }
       
 
    }

    public GameObject GetRoleUI(string id)
    {
        for (int i = 0; i <tf.childCount; i++)
        {
            if (tf.GetChild(i).GetComponent<RoleUISign>()!=null &&tf.GetChild(i).GetComponent<RoleUISign>().mapRole.baseRoleData.ID.ToString() == id)
            {
                return tf.GetChild(i).gameObject;
            }
        }

        return null;
    }

    public void CreatRoleLine()
    {
        foreach (var VARIABLE in TradeManager.My.tradeList)
        {
            GameObject lineobj;
            if (PlayerData.My.GetMapRoleById(double.Parse(VARIABLE.Value.tradeData.startRole)).baseRoleData.baseRoleData
                    .roleSkillType == GameEnum.RoleSkillType.Service ||
                PlayerData.My.GetMapRoleById(double.Parse(VARIABLE.Value.tradeData.endRole)).baseRoleData.baseRoleData
                    .roleSkillType == GameEnum.RoleSkillType.Service
            )
            {
                  lineobj = Instantiate(this.linexu,tf);

            }
            else
            {
                lineobj = Instantiate(line,tf);
            }

            lineobj.GetComponentInChildren<Text>().text = VARIABLE.Value.CalculateTC(true).ToString();
            lineobj.GetComponent<WMG_Link>().id = VARIABLE.Key;
            lineobj.GetComponent<WMG_Link>().fromNode = GetRoleUI(VARIABLE.Value.tradeData.startRole);
            lineobj.GetComponent<WMG_Link>().toNode = GetRoleUI(VARIABLE.Value.tradeData.endRole);
            lineobj.transform.SetAsFirstSibling();
        }


    }

    public void DeleteRole()
    {
        for (int i = 0; i <tf.childCount; i++)
        {
            Destroy(tf.GetChild(i).gameObject,0.01f);
        }
    }
}
