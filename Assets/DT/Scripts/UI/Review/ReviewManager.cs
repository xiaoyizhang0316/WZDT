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
    public void ShowCurrentReview()
    {
        ClearRolesLines();
        CreatRoles();
        CreatLines();
        
    }

    public ReviewPanel.ReviewRole GetRoleByMapRoles(double ID)
    {
        for (int i = 0; i < panel.mapStates.Count; i++)
        {
            for (int j = 0; j < panel.mapStates[i].mapTrades.Count; j++)
            {
                if (panel.mapStates[i].mapRoles[j].roleId == ID)
                {
                    return panel.mapStates[i].mapRoles[j];
                }
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
    public void CreatRoles()
    {
        for (int i = 0; i < panel.mapStates.Count; i++)
        {
            for (int j = 0; j < panel.mapStates[i].mapRoles.Count; j++)
            {
                //生成生产性角色
                if (panel.mapStates[i].mapRoles[j].roleType == GameEnum.RoleType.Seed)
                {
                    GameObject obj = Instantiate(rolePrb, seed);
                    obj.GetComponent<ReviewRoleSign>().role = panel.mapStates[i].mapRoles[j];
                }

                if (panel.mapStates[i].mapRoles[j].roleType == GameEnum.RoleType.Peasant)
                {
                    GameObject obj = Instantiate(rolePrb, peasant);
                    obj.GetComponent<ReviewRoleSign>().role = panel.mapStates[i].mapRoles[j];
                }

                if (panel.mapStates[i].mapRoles[j].roleType == GameEnum.RoleType.CutFactory ||
                    panel.mapStates[i].mapRoles[j].roleType == GameEnum.RoleType.JuiceFactory ||
                    panel.mapStates[i].mapRoles[j].roleType == GameEnum.RoleType.CanFactory
                )
                {
                    GameObject obj = Instantiate(rolePrb, merchant);
                    obj.GetComponent<ReviewRoleSign>().role = panel.mapStates[i].mapRoles[j];
                }

                if (panel.mapStates[i].mapRoles[j].roleType == GameEnum.RoleType.Dealer)
                {
                    GameObject obj = Instantiate(rolePrb, dealer);
                    obj.GetComponent<ReviewRoleSign>().role = panel.mapStates[i].mapRoles[j];
                }
            }

            for (int j = 0; j < panel.mapStates[i].mapTrades.Count; j++)
            {
                var starrole = GetRoleByMapRoles(panel.mapStates[i].mapTrades[j].startRole);
                var endrole = GetRoleByMapRoles(panel.mapStates[i].mapTrades[j].endRole);
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
    }

    
    public void CreatLines()
    {
        for (int i = 0; i < panel.mapStates.Count; i++)
        {
            for (int j = 0; j <  panel.mapStates[i].mapTrades.Count; j++)
            { 
               GameObject line =  Instantiate(linePrb, lineTF);
               lines.Add(line);
               line.GetComponent<VectorObject2D>().vectorLine.points2.Clear();
               line.GetComponent<VectorObject2D>().vectorLine.points2.Add(GetRoleByMapRoleSigns(panel.mapStates[i].mapTrades[j].startRole).transform.position);
               line.GetComponent<VectorObject2D>().vectorLine.points2.Add(GetRoleByMapRoleSigns(panel.mapStates[i].mapTrades[j].endRole).transform.position);
               

            }
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