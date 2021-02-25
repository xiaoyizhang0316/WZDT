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
        else if (other.CompareTag("MapRole") && other.transform.GetComponentInParent<BaseMapRole>().baseRoleData.inMap && GetComponentInParent<BaseMapRole>().baseRoleData.inMap)
        {
            if (GetComponentInParent<BaseMapRole>().baseRoleData.isNpc)
            {
                if (GetComponentInParent<BaseMapRole>().npcScript.isLock)
                    return;
            }
            BaseMapRole targetRole = other.GetComponentInParent<BaseMapRole>();
            BaseBuff buff = new BaseBuff();
            BuffData data = GameDataMgr.My.GetBuffDataByID(1000);
            float result;
            result = 0 - (GetComponentInParent<BaseMapRole>().baseRoleData.effect * 0.3f + 24f) / 100f;
            int number = Mathf.Min(400, (int)(result * targetRole.baseRoleData.tradeCost));
            data.OnBuffAdd.Add("14_" + number.ToString());
            buff.Init(data);
            buff.SetRoleBuff(null, targetRole, targetRole);
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
