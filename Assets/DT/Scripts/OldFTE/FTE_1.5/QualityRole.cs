using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityRole : BaseMapRole
{
    public int checkQuality;
    public int checkBuff=-1;
    public bool needCheck;
    public bool donotAdd = false;
    public List<int> checkBuffList;

    public Renderer signal;
    public Material normal;
    public Material wrong;
    public Material good;

    public override void AddPruductToWareHouse(ProductData data)
    {
        if (donotAdd)
        {
            return;
        }
        
        if (needCheck)
        {
            if (checkBuffList == null || checkBuffList.Count == 0)
            {
                if (checkBuff == -1 && checkQuality ==-1)
                {
                    warehouse.Add(data);
                    // 绿
                    signal.material = good;
                }
                else
                {
                    if (checkBuff == -1)
                    {
                        if (data.damage >= checkQuality)
                        {
                            warehouse.Add(data);
                            // 绿
                            signal.material = good;
                        }
                        else
                        {
                            // 红
                            signal.material = wrong;
                            //HttpManager.My.ShowTip("输送至"+baseRoleData.baseRoleData.roleName+"的产品未达标！");
                        }
                    }
                    else
                    {
                        if (checkQuality == -1)
                        {
                            if (data.buffList.Contains(checkBuff))
                            {
                                warehouse.Add(data);
                                //绿
                                signal.material = good;
                            }
                            else
                            {
                                if (data.wasteBuffList.Contains(checkBuff))
                                {
                                    HttpManager.My.ShowTip("<color=green>'"+baseRoleData.baseRoleData.roleName+ "'</color>所需要的口味被顶掉（哈密瓜最多可以附加上两种口味效果）",null, 4);
                                }
                                // 红
                                signal.material = wrong;
                                //HttpManager.My.ShowTip("输送至"+baseRoleData.baseRoleData.roleName+"的产品未达标！");
                            }
                        }
                        else
                        {
                            if (data.damage >= checkQuality && data.buffList.Contains(checkBuff))
                            {
                                warehouse.Add(data);
                                // 绿
                                signal.material = good;
                            }
                            else
                            {
                                // 红
                                signal.material = wrong;
                                //HttpManager.My.ShowTip("输送至"+baseRoleData.baseRoleData.roleName+"的产品未达标！");
                            }
                        }
                    }
                }
            }
            else
            {
                if (checkQuality == -1)
                {
                    if (ContaintAllBuffs(data))
                    {
                        warehouse.Add(data);
                        signal.material = good;
                    }
                    else
                    {
                        signal.material = wrong;
                    }
                }
                else
                {
                    if (ContaintAllBuffs(data))
                    {
                        if (data.damage >= checkQuality)
                        {
                            warehouse.Add(data);
                            signal.material = good;
                        }
                        else
                        {
                            signal.material = wrong;
                        }
                    }
                    else
                    {
                        signal.material = wrong;
                    }
                }
            }
        }
        else
        {
            warehouse.Add(data);
        }
        //signal.material = normal;
    }

    bool ContaintAllBuffs(ProductData data)
    {
        for (int i = 0; i < checkBuffList.Count; i++)
        {
            if (!data.buffList.Contains(checkBuffList[i]))
            {
                return false;
            }
        }

        return true;
    }

    public void QualityReset()
    {
        ClearWarehouse();
        signal.material = normal;
    }

    public void CheckEnd()
    {
        needCheck = false;
        checkBuff = -1;
        checkQuality = -1;
        checkBuffList?.Clear();
        donotAdd = false;
        ClearWarehouse();
        signal.material = normal;
    }
}