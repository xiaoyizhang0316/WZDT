using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameEnum;

public class BaseLevelController : MonoSingleton<BaseLevelController>
{
    /// <summary>
    /// 1星条件描述
    /// </summary>
    public string starOneCondition;

    /// <summary>
    /// 2星条件描述
    /// </summary>
    public string starTwoCondition;

    /// <summary>
    /// 3星条件描述
    /// </summary>
    public string starThreeCondition;

    /// <summary>
    /// 1星状态
    /// </summary>
    public bool starOneStatus = false;

    /// <summary>
    /// 2星状态
    /// </summary>
    public bool starTwoStatus = false;

    /// <summary>
    /// 3星状态
    /// </summary>
    public bool starThreeStatus = false;

    public int targetNumber = 0;

    public int putRoleNumber = 0;

    public Vector3 newCameraPos;

    public Vector3 newCameraRot;

    public float orthoSize;

    public GameObject emojiPrb;

    public string winkey1;

    public string winkey2;

    public string winkey3;

    public bool tradeCostMoney;

    public List<StageSpecialType> stageSpecialTypes = new List<StageSpecialType>();

    /// <summary>
    /// 统计击杀数量
    /// </summary>
    /// <param name="sign"></param>
    public virtual void CountKillNumber(ConsumeSign sign)
    {
        targetNumber++;
    }

    /// <summary>
    /// 统计放置的角色数量
    /// </summary>
    /// <param name="role"></param>
    public virtual void CountPutRole(Role role)
    {
        putRoleNumber++;
    }

    /// <summary>
    /// 1星条件检测
    /// </summary>
    /// <returns></returns>
    public virtual void CheckStarOne()
    {
        starOneStatus = StageGoal.My.playerHealth > 0;
        starOneCondition = "满意度大于0";
    }

    /// <summary>
    /// 2星条件检测
    /// </summary>
    /// <returns></returns>
    public virtual void CheckStarTwo()
    {
        starTwoStatus = true;
    }

    /// <summary>
    /// 更新界面信息
    /// </summary>
    public virtual void UpdateInfo()
    {
        StageGoal.My.starOne.sprite = StageGoal.My.starSprites[starOneStatus ? 0 : 1];
        StageGoal.My.starTwo.sprite = StageGoal.My.starSprites[starTwoStatus ? 0 : 1];
        StageGoal.My.starThree.sprite = StageGoal.My.starSprites[starThreeStatus ? 0 : 1];
        StageGoal.My.starOneText.text = starOneCondition;
        StageGoal.My.starTwoText.text = starTwoCondition;
        StageGoal.My.starThreeText.text = starThreeCondition;
    }

    /// <summary>
    /// 3星条件检测
    /// </summary>
    /// <returns></returns>
    public virtual void CheckStarThree()
    {
        starThreeStatus = true;
    }

    public void GenerateEmoji(Vector3 pos)
    {
        GameObject go = Instantiate(emojiPrb);
        go.transform.position = pos;
        go.transform.LookAt(Camera.main.transform);
        go.transform.Translate(Vector3.forward * 10f);
        Destroy(go, 1f);
    }

    
    // Start is called before the first frame update
    public virtual void Start()
    {
        //Debug.LogWarning("base level controller start");
        DOTween.PauseAll();
        DOTween.defaultAutoPlay = AutoPlay.None;
        if (StageGoal.My.currentType == GameEnum.StageType.Normal && !CommonParams.fteList.Contains(SceneManager.GetActiveScene().name))
        {
            NewCanvasUI.My.ToggleSpeedButton(false);
        }
        InvokeRepeating("CheckStarTwo", 0f, 1f);
        InvokeRepeating("CheckStarThree", 0f, 1f);
        InvokeRepeating("CheckStarOne", 0f, 1f);
        InvokeRepeating("UpdateInfo", 0.1f, 1f);
        if (!PlayerData.My.isSOLO && !PlayerData.My.isServer)
        {
            string str = "OnGameReady|1";
            PlayerData.My.client.SendToServerMsg(str);
        }
        if (!PlayerData.My.isSOLO)
        {
            PlayerData.My.isLocalReady = true;
            PlayerData.My.CheckGameStart();
        }
        else
        {
            if (StageGoal.My.currentType != GameEnum.StageType.Normal && PlayerPrefs.GetInt("isUseGuide") == 0)
            {
                NewCanvasUI.My.GameNormal();
            }
        }
        //else if (PlayerPrefs.GetInt("isUseGuide") == 0)
        //{
        //    NewCanvasUI.My.GameNormal();
        //}
    }

    public void CheckCheat()
    {
        if (PlayerData.My.cheatIndex1 || PlayerData.My.cheatIndex2 || PlayerData.My.cheatIndex3)
        {
            starThreeStatus = false;
            CancelInvoke("CheckStarThree");
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out RaycastHit hit);
                if (hit.transform != null)
                {
                    //GenerateEmoji(hit.point);
                    if(!PlayerData.My.isSOLO)
                    {
                        string str = "Emoji|" + hit.point.x + "," + hit.point.y + "," + hit.point.z;
                        if (PlayerData.My.isServer)
                        {
                            PlayerData.My.server.SendToClientMsg(str);
                        }
                        else
                        {
                            PlayerData.My.client.SendToServerMsg(str);
                        }
                    }
                }
            }
        }
    }
}
