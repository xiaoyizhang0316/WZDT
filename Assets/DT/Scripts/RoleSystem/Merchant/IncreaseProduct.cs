using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseProduct : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Product") && GetComponentInParent<BaseMapRole>().baseRoleData.inMap)
        {
            double id = GetComponentInParent<BaseMapRole>().baseRoleData.ID;
            int num = GetComponentInParent<BaseMapRole>().baseRoleData.efficiency;
            other.GetComponent<GoodsSign>().AddSpeedBuff(id,num);
        }
        else if (other.CompareTag("MapRole"))
        {
            BaseMapRole targetRole = other.GetComponent<BaseMapRole>();
            for (int i = 0; i < targetRole.buffList.Count; i++)
            {
                if (targetRole.buffList[i].buffId == 999)
                {
                    return;
                }
            }
            //TODO
            //BaseBuff buff = new BaseBuff();
            //BuffData data = GameDataMgr.My.GetBuffDataByID(000);
            //buff.Init(data);
            //buff.SetRoleBuff(targetRole, targetRole, targetRole);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Product"))
        {
            double id = GetComponentInParent<BaseMapRole>().baseRoleData.ID;
            other.GetComponent<GoodsSign>().RemoveSpeedBuff(id);
        }
    }
}
