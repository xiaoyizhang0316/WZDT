using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PermissionManager : MonoSingleton<PermissionManager>
{

    public GameObject severCanvas;
    public GameObject clientCanvas;

    public GameObject soloCanvas;
    
    public Button Button_consumer;
    public Button Button_financial;
    public Button Button_RoleInfo;

    public bool isnpc;
    // Start is called before the first frame update
    void Awake()
    {
        InitUI();
    }

    public void Start()
    {
     
        Button_consumer.onClick.AddListener(() =>
        {
          WaveCount.My.transform.SetAsLastSibling();
        });

        Button_financial.onClick.AddListener(() =>
        {
            DataStatPanel.My.ShowStat();
         DataStatPanel.My.transform.SetAsLastSibling();
        });

        Button_RoleInfo.onClick.AddListener(() =>
        {
            if (isnpc)
            {
                NPCListInfo.My.transform.SetAsLastSibling();
            }
            else
            {
                RoleUpdateInfo.My.transform.SetAsLastSibling();
            }
        });
    }

    public void OnGUI()
    {
        if (GUILayout.Button("123"))
        {
            Debug.Log(PlayerData.My.MapRole[PlayerData.My.MapRole.Count-1].GetWarehouseJson());
        }
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

        if (PlayerData.My.creatRole ==PlayerData.My.playerDutyID)
        {
            severCanvas.SetActive(true);
            foreach (BaseMapRole role in PlayerData.My.MapRole)
            {
                role.CheckRoleDuty();
            }
        }
        else
        {
            Camera.main.transform.DOMove(BaseLevelController.My.newCameraPos, 2f).Play();
            //Camera.main.DOOrthoSize(BaseLevelController.My.orthoSize, 2f).Play();
            Camera.main.transform.DORotate(BaseLevelController.My.newCameraRot, 2f).OnComplete(() => {
                foreach (BaseMapRole role in PlayerData.My.MapRole)
                {
                    role.CheckRoleDuty();
                }
            }).Play();
            clientCanvas.SetActive(true);
        }
    }
}
