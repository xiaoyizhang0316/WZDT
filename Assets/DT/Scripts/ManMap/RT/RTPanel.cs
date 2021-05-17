using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RTPanel : MonoBehaviour
{
    // 生成list的content
    public Transform content;
    // 要生成在列表里的预制体，显示详细信息
    public GameObject rtPrefab;
    // 开启竞赛排名按钮
    public Button rank_btn;
    // 关闭界面按钮
    public Button close_btn;
    // 正在更新
    bool isOnUpdate = false;
    // Start is called before the first frame update
    void Start()
    {
        rank_btn.onClick.AddListener(() =>
        {
            LevelInfoManager.My.Close();
            rank_btn.gameObject.SetActive(false);
            transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 0.02f);
            InvokeRepeating("UpdateGroupPlayerScore", 0.5f, 5);
        });
        close_btn.onClick.AddListener(Close);
    }
    
    /// <summary>
    /// 初始化界面
    /// </summary>
    public void InitRTPanel()
    {
        NetworkMgr.My.GetGroupScoreStatus();
        NetworkMgr.My.GetPlayerGroupInfo(() =>
        {
            if (LevelInfoManager.My.currentSceneName.Equals("FTE_9"))
            {
                if (NetworkMgr.My.playerDatas.levelID >= 12)
                {
                    if (NetworkMgr.My.playerGroupInfo.isOpenMatch)
                    {
                        isOnUpdate = true;
                        rank_btn.gameObject.SetActive(true);
                        /*transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(604, 38), 0.02f);
                        InvokeRepeating("UpdateGroupPlayerScore", 0.5f, 5);*/
                    }
                    else
                    {
                        rank_btn.gameObject.SetActive(false);
                    }
                }
            }
        });
    }

    /// <summary>
    /// 更新小组排名
    /// </summary>
    private void UpdateGroupPlayerScore()
    {
        NetworkMgr.My.GetGroupRTScore(()=> {
            if (NetworkMgr.My.stopMatch)
            {
                CancelInvoke();
                ClearContent();
                ShowRtList();
            }
            else
            {
                ClearContent();
                ShowRtList();
            }
        });
    }

    /// <summary>
    /// 显示列表
    /// </summary>
    void ShowRtList()
    {
        for(int i=0; i< NetworkMgr.My.playerRTScores.Count; i++)
        {
            InitRtItem(NetworkMgr.My.playerRTScores[i], i + 1);
        }
    }

    /// <summary>
    /// 初始化每个玩家的详细信息
    /// </summary>
    /// <param name="prt"></param>
    /// <param name="rank"></param>
    void InitRtItem(PlayerRTScore prt, int rank)
    {
        GameObject go = Instantiate(rtPrefab, content);
        RTSample rts = go.GetComponent<RTSample>();
        rts.Setup(prt, rank);
    }

    /// <summary>
    /// 清除列表界面
    /// </summary>
    void ClearContent()
    {
        foreach(Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// 关闭
    /// </summary>
    public void Close()
    {
        if (isOnUpdate)
        {
            transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 4000), 0.02f);
            CancelInvoke();
            ClearContent();
            isOnUpdate = false;
        }
        rank_btn.gameObject.SetActive(false);
    }
}
