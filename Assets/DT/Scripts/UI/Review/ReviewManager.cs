﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public List<ReviewRoleSign> signs;
    public List<GameObject> lines;
    public Transform lineTF;
    public int index;
    public GameObject roles;
    public GameObject linePRBs;
 
    public int countseed;
    public int countpeasant;
    public int countmerchant;
    public int countdealer;
    public void ShowCurrentReview(int index)
    {
        if (this.index == index)
        {
            return;
        }

        this.index = index;
        ClearRolesLines();

        CreatRoles();
        
        Rank();
        
        
        CreatLines();
    }


    public ReviewPanel.ReviewRole GetRoleByMapRoles(double ID)
    {
        for (int j = 0; j < panel.mapStates[index].mapRoles.Count; j++)
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
            if (signs[i].isInit&& signs[i].role.roleId == ID)
            {
                return signs[i];
            }
        }

        Debug.Log(ID);
        return new ReviewRoleSign();
    }

    public ReviewRoleSign GetRoleSign(GameEnum.RoleType type)
    {
        if (type == GameEnum.RoleType.Seed)
        {
            for (int i = 0; i < seed.childCount; i++)
            {
                if (!seed.GetChild(i).GetComponent<ReviewRoleSign>().isInit)
                {
                    countseed++;
                    return seed.GetChild(i).GetComponent<ReviewRoleSign>();
                }
            }
        }

        if (type == GameEnum.RoleType.Peasant)
            {
                for (int i = 0; i < peasant.childCount; i++)
                {
                    if (!peasant.GetChild(i).GetComponent<ReviewRoleSign>().isInit)
                    {
                        countpeasant++;
                        return peasant.GetChild(i).GetComponent<ReviewRoleSign>();
                    }
                }
            }

            if (type == GameEnum.RoleType.CutFactory || type == GameEnum.RoleType.CanFactory ||
                type == GameEnum.RoleType.CrispFactory || type == GameEnum.RoleType.JuiceFactory ||
                type == GameEnum.RoleType.PackageFactory || type == GameEnum.RoleType.SweetFactory ||
                type == GameEnum.RoleType.WholesaleFactory || type == GameEnum.RoleType.SoftFactory||
                type == GameEnum.RoleType.Merchant
                )
                
            {
                for (int i = 0; i < merchant.childCount; i++)
                {
                    if (!merchant.GetChild(i).GetComponent<ReviewRoleSign>().isInit)
                    {
                        countmerchant++;
                        return merchant.GetChild(i).GetComponent<ReviewRoleSign>();
                    }
                }
            }

            if (type == GameEnum.RoleType.Dealer)
            {
                for (int i = 0; i < dealer.childCount; i++)
                {
                    if (!dealer.GetChild(i).GetComponent<ReviewRoleSign>().isInit)
                    {
                        countdealer++;
                        return dealer.GetChild(i).GetComponent<ReviewRoleSign>();
                    }
                }
            }
     

        Debug.Log("都已经初始化");
                    return null;
                }


                public void CreatRoles()
                {
                    for (int j = 0; j < panel.mapStates[index].mapRoles.Count; j++)
                    {
                        //生成生产性角色
                            Debug.Log(panel.mapStates[index].mapRoles[j].roleType+"j");
                        GetRoleSign(panel.mapStates[index].mapRoles[j].roleType)
                            .InitRole(panel.mapStates[index].mapRoles[j]);
                    }

                    for (int j = 0; j < panel.mapStates[index].mapTrades.Count; j++)
                    {
                        var starrole = GetRoleByMapRoles(panel.mapStates[index].mapTrades[j].startRole);
                        var endrole = GetRoleByMapRoles(panel.mapStates[index].mapTrades[j].endRole);
                        if (!CheckRole(starrole.roleId))
                        {
                           
                                GetRoleSign(endrole.roleType)
                                    .InitRole( starrole);
                         
                        }
                    }
                }


                public void CreatLines()
                {
                    for (int j = 0; j < panel.mapStates[index].mapTrades.Count; j++)
                    {
                        Debug.Log("当前" + index + "j" + j);
                        DrawLine(GetRoleByMapRoleSigns(panel.mapStates[index].mapTrades[j].startRole).gameObject,
                            GetRoleByMapRoleSigns(panel.mapStates[index].mapTrades[j].endRole).gameObject
                        );
                    }
                }


                public void ClearRolesLines()
                {
                    for (int i = 0; i < signs.Count; i++)
                    {
                        signs[i].ClearRole() ;
                    }

                    for (int i = 0; i < lines.Count; i++)
                    {
                        Destroy(lines[i].gameObject);
                    }

                    countdealer = 0;
                    countmerchant = 0;
                    countpeasant = 0;
                    countseed = 0;
                    lines.Clear();
                }

                public bool CheckRole(double ID)
                {
                    for (int i = 0; i < signs.Count; i++)
                    {
                        if (signs[i].isInit&& signs[i].role.roleId == ID)
                        {
                            return true;
                        }
                    }

                    return false;
                }


                public void Rank()
                {
                    List<ReviewRoleSign> tempList = new List<ReviewRoleSign>();
                    for (int i = 0; i <seed.childCount; i++)
                    {
                        if ( seed.GetChild(i).GetComponent<ReviewRoleSign>().isInit)
                        {
                            tempList.Add( seed.GetChild(i).GetComponent<ReviewRoleSign>());
                        }
                    }
                    RankPosition(tempList );
                    tempList.Clear();
                    
                    for (int i = 0; i <peasant.childCount; i++)
                    {
                        if ( peasant.GetChild(i).GetComponent<ReviewRoleSign>().isInit)
                        {
                            tempList.Add( peasant.GetChild(i).GetComponent<ReviewRoleSign>());
                        }
                    }
                    RankPosition(tempList );
                    tempList.Clear();
                    for (int i = 0; i <merchant.childCount; i++)
                    {
                        if ( merchant.GetChild(i).GetComponent<ReviewRoleSign>().isInit)
                        {
                            tempList.Add( merchant.GetChild(i).GetComponent<ReviewRoleSign>());
                        }
                    }
                    RankPosition(tempList );
                    tempList.Clear();
                    for (int i = 0; i <dealer.childCount; i++)
                    {
                        if ( dealer.GetChild(i).GetComponent<ReviewRoleSign>().isInit)
                        {
                            tempList.Add( dealer.GetChild(i).GetComponent<ReviewRoleSign>());
                        }
                    }
                    RankPosition(tempList );
                    tempList.Clear();
                }

                public void RankPosition(List<ReviewRoleSign> signs)
                {
                    if (signs.Count <= 6)
                    {
                        //一行
                        for (int i = 0; i <signs.Count; i++)
                        {
                            float tem = 720 / (signs.Count+1);
                            signs[i].transform.localPosition = new Vector3(0,360-tem*(i+1));
                            signs[i].ChangeParent();
                        
                        }
                    }
                    else
                    {
                        for (int i = 0; i <6; i++)
                        {
                            float tem = 720 / (6+1);
                            signs[i].transform.localPosition = new Vector3(-80,360-tem*(i+1));
                        
                        }
                        for (int i = 6; i <signs.Count ; i++)
                        {
                            float tem = 720 / (signs.Count+1);
                            signs[i].transform.localPosition = new Vector3(80,360-tem*(i-6+1));
                        
                        }
                        
                    }

                }

                void DrawLine(GameObject posA, GameObject posB)
                {
                    GameObject line = Instantiate(linePrb, lineTF);
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