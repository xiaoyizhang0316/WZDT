using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseProduct : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {
        //print("降交易成本");
        if (other.CompareTag("Product") && GetComponentInParent<BaseMapRole>().baseRoleData.inMap)
        {
            double id = GetComponentInParent<BaseMapRole>().baseRoleData.ID;
            int num = GetComponentInParent<BaseMapRole>().baseRoleData.efficiency;
            other.GetComponent<GoodsSign>().AddSpeedBuff(id,num);
        }
        else if (other.CompareTag("MapRole") && other.transform.GetComponentInParent<BaseMapRole>().baseRoleData.inMap && GetComponentInParent<BaseMapRole>().baseRoleData.inMap)
        {
            //print("降交易成本");
            BaseMapRole targetRole = other.GetComponentInParent<BaseMapRole>();
            BaseBuff buff = new BaseBuff();
            BuffData data = GameDataMgr.My.GetBuffDataByID(1000);
            float result;
            result = 0 - (GetComponentInParent<BaseMapRole>().baseRoleData.effect * 0.3f + 24f) / 100f;
            data.OnBuffAdd.Add("14_" + result.ToString());
            buff.Init(data);
            buff.SetRoleBuff(targetRole, targetRole, targetRole);
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
