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
               GameObject line =  Instantiate(linePrb, lineTF);
               lines.Add(line);
               line.GetComponent<VectorObject2D>().vectorLine.points2.Clear();
               line.GetComponent<VectorObject2D>().vectorLine.points2.Add(GetRoleByMapRoleSigns(panel.mapStates[index].mapTrades[j].startRole).transform.position);
               line.GetComponent<VectorObject2D>().vectorLine.points2.Add(GetRoleByMapRoleSigns(panel.mapStates[index].mapTrades[j].endRole).transform.position);
               

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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}