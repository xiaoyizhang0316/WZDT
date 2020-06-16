using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleTradeButton : MonoBehaviour
{
    public BaseMapRole currentRole;

    /// <summary>
    /// 发起交易
    /// </summary>
    public void StartTrade()
    {
        NewCanvasUI.My.CreateTrade(currentRole);

    }

    // Start is called before the first frame update
    void Start()
    {
        currentRole = transform.GetComponentInParent<BaseMapRole>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.LookAt(Camera.main.transform.position);
        GetComponent<Image>().fillAmount = currentRole.warehouse.Count / (float)currentRole.baseRoleData.bulletCapacity;
    }
}
