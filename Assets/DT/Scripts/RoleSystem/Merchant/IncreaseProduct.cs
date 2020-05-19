using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseProduct : MonoBehaviour
{

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Product") && GetComponentInParent<RolePosSign>().isRelease)
        {
            double id = GetComponentInParent<BaseMapRole>().baseRoleData.ID;
            int num = GetComponentInParent<BaseMapRole>().baseRoleData.effect;
            other.GetComponent<GoodsSign>().AddSpeedBuff(id,num);
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
