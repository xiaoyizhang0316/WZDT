using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RTPanel : MonoBehaviour
{
    public Transform content;
    public GameObject rtPrefab;
    public Button rank_btn;
    public Button close_btn;
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

    void ShowRtList()
    {
        for(int i=0; i< NetworkMgr.My.playerRTScores.Count; i++)
        {
            InitRtItem(NetworkMgr.My.playerRTScores[i], i + 1);
        }
    }

    void InitRtItem(PlayerRTScore prt, int rank)
    {
        GameObject go = Instantiate(rtPrefab, content);
        RTSample rts = go.GetComponent<RTSample>();
        rts.Setup(prt, rank);
    }

    void ClearContent()
    {
        foreach(Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }

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
