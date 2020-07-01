using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class ReviewManager : MonoSingleton<ReviewManager>
{
    public Transform seed;
    public Transform peasant;
    public Transform merchant;
    public Transform dealer;

    public GameObject rolePrb;
    public GameObject linePrb;

    public ReviewPanel panel;
    public  List<ReviewRoleSign> signs;
    public  List<GameObject> lines;
    public Transform lineTF;
    public int index;
    public GameObject linePRBs;
    public void ShowCurrentReview(int index)
    {
 
        this.index = index;
        ClearRolesLines();
        CreatRoles();
        CreatLines();
        
    }

    public ReviewPanel.ReviewRole GetRoleByMapRoles(double ID)
    {
     
            for (int j = 0; j < panel.mapStates[index].mapTrades.Count; j++)
            {
                if (panel.mapStates[index].mapRoles[j].roleId == ID)
                {
                    return panel.mapStates[index].mapRoles[j];
                }
            }
       

        return new ReviewPanel.ReviewRole();
    }
    public ReviewRoleSign GetRoleByMapRoleSigns(double ID)
    {
        for (int i = 0; i < signs.Count; i++)
        {
                
                if (signs[i].role.roleId == ID)
                {
                    return signs[i];
                }
           
        }
        Debug.Log(ID);
        return new ReviewRoleSign();
    }
    public void CreatRoles( )
    {
       
            for (int j = 0; j < panel.mapStates[index].mapRoles.Count; j++)
            {
                //生成生产性角色
                if (panel.mapStates[index].mapRoles[j].roleType == GameEnum.RoleType.Seed)
                {
                    GameObject obj = Instantiate(rolePrb, seed);
                    obj.GetComponent<ReviewRoleSign>().role = panel.mapStates[index].mapRoles[j];
                }

                if (panel.mapStates[index].mapRoles[j].roleType == GameEnum.RoleType.Peasant)
                {
                    GameObject obj = Instantiate(rolePrb, peasant);
                    obj.GetComponent<ReviewRoleSign>().role = panel.mapStates[index].mapRoles[j];
                }

                if (panel.mapStates[index].mapRoles[j].roleType == GameEnum.RoleType.CutFactory ||
                    panel.mapStates[index].mapRoles[j].roleType == GameEnum.RoleType.JuiceFactory ||
                    panel.mapStates[index].mapRoles[j].roleType == GameEnum.RoleType.CanFactory
                )
                {
                    GameObject obj = Instantiate(rolePrb, merchant);
                    obj.GetComponent<ReviewRoleSign>().role = panel.mapStates[index].mapRoles[j];
                }

                if (panel.mapStates[index].mapRoles[j].roleType == GameEnum.RoleType.Dealer)
                {
                    GameObject obj = Instantiate(rolePrb, dealer);
                    obj.GetComponent<ReviewRoleSign>().role = panel.mapStates[index].mapRoles[j];
                }
            }

            for (int j = 0; j < panel.mapStates[index].mapTrades.Count; j++)
            {
                var starrole = GetRoleByMapRoles(panel.mapStates[index].mapTrades[j].startRole);
                var endrole = GetRoleByMapRoles(panel.mapStates[index].mapTrades[j].endRole);
                if (!CheckRole(starrole.roleId))
                {
                     

                    if (endrole.roleType == GameEnum.RoleType.Seed)
                    {
                        GameObject obj = Instantiate(rolePrb, seed);
                        obj.GetComponent<ReviewRoleSign>().role = starrole;
                    }

                    if (endrole.roleType == GameEnum.RoleType.Peasant)
                    {
                        GameObject obj = Instantiate(rolePrb, peasant);
                        obj.GetComponent<ReviewRoleSign>().role = starrole;
                    }

                    if (endrole.roleType == GameEnum.RoleType.CutFactory ||
                        endrole.roleType == GameEnum.RoleType.JuiceFactory ||
                        endrole.roleType == GameEnum.RoleType.CanFactory
                    )
                    {
                        GameObject obj = Instantiate(rolePrb, merchant);
                        obj.GetComponent<ReviewRoleSign>().role = starrole;
                    }

                    if (endrole.roleType == GameEnum.RoleType.Dealer)
                    {
                        GameObject obj = Instantiate(rolePrb, dealer);
                        obj.GetComponent<ReviewRoleSign>().role = starrole;
                    }
                }
            } 
    }

    
    public void CreatLines()
    {
      
            for (int j = 0; j <  panel.mapStates[index].mapTrades.Count; j++)
            { 
              
                   Debug.Log("当前"+index+"j"+j);
               DrawLine( GetRoleByMapRoleSigns(panel.mapStates[index].mapTrades[j].startRole).gameObject, GetRoleByMapRoleSigns(panel.mapStates[index].mapTrades[j].endRole).gameObject
               );
            }
        }
   

    public void ClearRolesLines()
    {
        for (int i = 0; i <signs.Count; i++)
        {
           Destroy(signs[i].gameObject); 
        }
        for (int i = 0; i <lines.Count; i++)
        {
            Destroy(lines[i].gameObject); 
        }
        signs.Clear();
        lines.Clear();
    }

    public bool  CheckRole(double ID)
    {
        for (int i = 0; i <signs.Count; i++)
        {
            if (signs[i].role.roleId == ID)
            {
                return true;
            }
        }

        return false;
    }
 

    

    void DrawLine(GameObject posA, GameObject posB)
    {
        GameObject line =    Instantiate(linePrb, lineTF);
        lines.Add(line);
        line.GetComponent<WMG_Link>().fromNode = posA;
        line.GetComponent<WMG_Link>().toNode = posB;
        //RectTransform ImageRectTrans =    line.GetComponent<RectTransform>();
        //   Vector3 differenceVector = posB - posA;
//
        //   // 设置线的长度（Width）
        //   ImageRectTrans.sizeDelta = new Vector2(differenceVector.magnitude, 2);
        //   // 从左向右划线
        //
        //   // 设置线的起始点
        //   ImageRectTrans.position = posA;
        //   // 设置线相对于水平向右方向的角度
        //   float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
        //   ImageRectTrans.transform.eulerAngles =new Vector3(0, 0, angle); 
    }
   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}