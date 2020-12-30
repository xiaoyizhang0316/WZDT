using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityRole : BaseMapRole
{
    public int checkQuality;
    public int checkBuff;
    public bool needCheck;

    public Renderer signal;
    public Material normal;
    public Material wrong;
    public Material good;

    public override void AddPruductToWareHouse(ProductData data)
    {
        if (needCheck)
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
                            // 红
                            signal.material = wrong;
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
                        }
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
}