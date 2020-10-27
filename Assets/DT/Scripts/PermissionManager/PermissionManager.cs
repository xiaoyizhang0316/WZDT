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

    public BaseMapRole lastSelectRole;

    public int currentSelectPanel = 0;

    private Transform buttonParent;

    public List<Sprite> sprites;

    // Start is called before the first frame update
    void Awake()
    {
        InitUI();
    }

    public void Start()
    {
        buttonParent = Button_financial.transform.parent;
        Button_consumer.transform.SetParent(WaveCount.My.transform);
        Button_consumer.onClick.AddListener(() =>
        {
            WaveCount.My.transform.SetAsLastSibling();
            Button_consumer.transform.SetParent(WaveCount.My.transform);
            Button_consumer.GetComponent<Image>().sprite = sprites[0];
            Button_financial.transform.SetParent(buttonParent);
            Button_financial.GetComponent<Image>().sprite = sprites[1];
            Button_RoleInfo.transform.SetParent(buttonParent);
            Button_RoleInfo.GetComponent<Image>().sprite = sprites[1];
            currentSelectPanel = 0;
        });

        Button_financial.onClick.AddListener(() =>
        {
            DataStatPanel.My.ShowStat();
            DataStatPanel.My.transform.SetAsLastSibling();
            Button_consumer.transform.SetParent(buttonParent);
            Button_consumer.GetComponent<Image>().sprite = sprites[1];
            Button_financial.transform.SetParent(DataStatPanel.My.transform);
            Button_financial.GetComponent<Image>().sprite = sprites[0];
            Button_RoleInfo.transform.SetParent(buttonParent);
            Button_RoleInfo.GetComponent<Image>().sprite = sprites[1];
            currentSelectPanel = 1;
        });
        Button_RoleInfo.onClick.AddListener(() =>
        {
            InitRolePanel();
        });
    }

    public void InitRolePanel()
    {
        Button_consumer.transform.SetParent(buttonParent);
        Button_consumer.GetComponent<Image>().sprite = sprites[1];
        Button_RoleInfo.GetComponent<Image>().sprite = sprites[0];
        Button_financial.transform.SetParent(buttonParent);
        Button_financial.GetComponent<Image>().sprite = sprites[1];
        if (lastSelectRole == null)
        {
            Transform nullRole = NPCListInfo.My.transform.parent.Find("nullRole");
            nullRole.SetAsLastSibling();
            Button_RoleInfo.transform.SetParent(nullRole);
        }
        else if (lastSelectRole.isNpc)
        {
            if (lastSelectRole.npcScript.isCanSee)
            {
                NPCListInfo.My.transform.SetAsLastSibling();
                Button_RoleInfo.transform.SetParent(NPCListInfo.My.transform);
            }
            else
            {
                Transform unknownRole = NPCListInfo.My.transform.parent.Find("unknownRole");
                unknownRole.SetAsLastSibling();
                Button_RoleInfo.transform.SetParent(unknownRole);
            }
        }
        else
        {
            RoleUpdateInfo.My.transform.SetAsLastSibling();
            Button_RoleInfo.transform.SetParent(RoleUpdateInfo.My.transform);
        }
        currentSelectPanel = 2;
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
            Camera.main.DOOrthoSize(BaseLevelController.My.orthoSize, 2f).Play();
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
