using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleTradeButton : MonoBehaviour
{
    public BaseMapRole currentRole;

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
        transform.LookAt(Camera.main.transform.position);
    }
}
