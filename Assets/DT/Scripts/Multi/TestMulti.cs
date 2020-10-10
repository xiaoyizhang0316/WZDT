using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestMulti : NetworkBehaviour
{
    [ClientRpc]
    public void RpcTest()
    {
        Debug.Log("dasds213124eadfadfdsf34321e2");
        transform.position += Vector3.one;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            RpcTest();
        }
    }

}
