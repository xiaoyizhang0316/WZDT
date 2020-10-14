using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermissionManager : MonoSingleton<PermissionManager>
{

    public GameObject severCanvas;
    public GameObject clientCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// /根据权限初始化UI
    /// </summary>
    public void InitUI()
    {
        if (PlayerData.My.server != null || PlayerData.My.isSOLO)
        {
            severCanvas.SetActive(true);
        }
        else
        {
            clientCanvas.SetActive(true);
        }
    }
}
