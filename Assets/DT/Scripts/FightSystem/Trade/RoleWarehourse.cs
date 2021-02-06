using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleWarehourse : MonoBehaviour
{
    public BaseMapRole currentRole;
    // Start is called before the first frame update
    void Start()
    {
        currentRole = transform.GetComponentInParent<BaseMapRole>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerData.My.creatRole == PlayerData.My.playerDutyID)
        {
            GetComponent<Image>().fillAmount = currentRole.warehouse.Count / (float)currentRole.baseRoleData.bulletCapacity;
        }
    }
}
