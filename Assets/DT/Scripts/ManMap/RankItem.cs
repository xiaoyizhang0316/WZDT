using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankItem : MonoBehaviour
{
    public Button replay_btn;
    public GameObject recordInfos;
    public GameObject rankInfos;
    public GameObject group;
    public GameObject global;
    public GameObject rankStarsAndScore;
    public GameObject bossLevelAndScore;
    public GameObject noRank;
    public GameObject teamTag;
    //public List<Sprite> rankSprites;
    public Image rankSprite;
    public Text rankText;
    #region record
    public Transform recordStars;
    public Text recordScore;
    public Text recordDate;
    public Text recordTimeCount;
    #endregion

    #region group rank
    public Text groupRankPlayerName;
    #endregion

    #region global rank
    public Text globalRankPlayerName;
    public Text globalRankGroupName;
    #endregion

    public Transform rankStars;
    public Text starsScore;
    public Text bossLevel;
    public Text bossLevelScore;

    private string recordID = "";
    int rank = 0;
    //Color _color = Color.white;;
    void Start()
    {
        replay_btn.onClick.AddListener(Review);
        //_color = Color.white;
    }

    void Review()
    {
        NetworkMgr.My.GetReplayDatas(recordID, (datas) =>
        {
            string str = "{ \"playerOperations\":" + datas.operations + "}";
            PlayerOperations operations = JsonUtility.FromJson<PlayerOperations>(str);
            string str1 = "{ \"dataStats\":" + datas.dataStats + "}";
            PlayerStatus status = JsonUtility.FromJson<PlayerStatus>(str1);
            Debug.Log("获取录像成功");
            ReviewPanel.My.MapInit(operations.playerOperations, status.dataStats, (status.dataStats.Count - 1) * 5);
        });
    }

    public void Setup(ReplayList rp)
    {
        GetComponent<Image>().color = Color.white;
        recordID = rp.recordID;
        noRank.SetActive(false);
        rankInfos.SetActive(false);
        recordInfos.SetActive(true);
        SetStars(recordStars, rp.stars);
        recordScore.text = rp.score.ToString();
        recordDate.text = TimeStamp.TimeStampToString(rp.recordTime);
        recordTimeCount.text = rp.realTime/60+":"+rp.realTime%60;
        if (rp.score == -1)
        {
            GetComponent<Image>().color = Color.gray;
        }
        else if (!rp.win)
        {
            GetComponent<Image>().color = Color.red;
            SetStars(recordStars, "000");
        }
        if (rp.isTeamwork)
        {
            teamTag.SetActive(true);
        }
        else
        {
            teamTag.SetActive(false);
        }
    }

    public void Setup(RankList rankList, bool isGroup)
    {
        GetComponent<Image>().color = Color.white;
        recordID = rankList.recordID;
        noRank.SetActive(false);
        recordInfos.SetActive(false);
        rankInfos.SetActive(true);
        
        if(rankList.sceneName == "FTE_9")
        {
            bossLevelAndScore.SetActive(true);
            rankStarsAndScore.SetActive(false);
            
            bossLevel.text = rankList.bossLevel.ToString();
            bossLevelScore.text = rankList.score.ToString();
            if (isGroup)
            {
                group.SetActive(true);
                global.SetActive(false);
                groupRankPlayerName.text = rankList.playerName;
                rank = rankList.rank - NetworkMgr.My.currentGroupPage * CommonParams.rankPageMaxNum;
                if (rank <= 0)
                {
                    rank = rankList.rank + NetworkMgr.My.currentGroupPage * CommonParams.rankPageMaxNum;
                }
                else
                {
                    rank = rankList.rank;
                }
            }
            else
            {
                group.SetActive(false);
                global.SetActive(true);
                globalRankPlayerName.text = rankList.playerName;
                globalRankGroupName.text = rankList.groupName;
                rank = rankList.rank - NetworkMgr.My.currentGlobalPage * CommonParams.rankPageMaxNum;
                if (rank <= 0)
                {
                    rank = rankList.rank + NetworkMgr.My.currentGlobalPage * CommonParams.rankPageMaxNum;
                }
                else
                {
                    rank = rankList.rank;
                }
            }
        }
        else
        {
            bossLevelAndScore.SetActive(false);
            rankStarsAndScore.SetActive(true);
            SetStars(rankStars, rankList.stars);
            starsScore.text = rankList.score.ToString();
            
            if (isGroup)
            {
                group.SetActive(true);
                global.SetActive(false);
                if (rankList.isTeamwork)
                {
                    teamTag.SetActive(true);
                    groupRankPlayerName.text = rankList.teamName;
                    //globalRankGroupName.text = rankList.groupName;
                }
                else
                {
                    teamTag.SetActive(false);
                    groupRankPlayerName.text = rankList.playerName;
                    //globalRankGroupName.text = rankList.groupName;
                }
                //groupRankPlayerName.text = rankList.playerName;
                rank = rankList.rank - NetworkMgr.My.currentGroupPage * CommonParams.rankPageMaxNum;
                if (rank <= 0)
                {
                    rank = rankList.rank + NetworkMgr.My.currentGroupPage * CommonParams.rankPageMaxNum;
                }
                else
                {
                    rank = rankList.rank;
                }
            }
            else
            {
                group.SetActive(false);
                global.SetActive(true);
                if (rankList.isTeamwork)
                {
                    teamTag.SetActive(true);
                    globalRankPlayerName.text = rankList.teamName;
                    globalRankGroupName.text = rankList.groupName;
                }
                else {
                    teamTag.SetActive(false);
                    globalRankPlayerName.text = rankList.playerName;
                    globalRankGroupName.text = rankList.groupName;
                }

                rank = rankList.rank - NetworkMgr.My.currentGlobalPage * CommonParams.rankPageMaxNum;
                if (rank <= 0)
                {
                    rank = rankList.rank + NetworkMgr.My.currentGlobalPage * CommonParams.rankPageMaxNum;
                }
                else
                {
                    rank = rankList.rank;
                }
            }
        }
        rankText.text = rank.ToString();
        if (rank <= 3)
        {
            rankSprite.sprite = RankPanel.My.rankSprites[rank - 1];
            
            rankSprite.gameObject.SetActive(true);
        }
        else
        {
            rankSprite.gameObject.SetActive(false);
        }

    }

    public void SetMyRank(string tip="未上榜")
    {
        recordInfos.SetActive(false);
        rankInfos.SetActive(false);
        replay_btn.gameObject.SetActive(false);
        noRank.GetComponent<Text>().text = tip;
        noRank.SetActive(true);
    }

    public void SetMyRank(RankList rankList, bool isGroup)
    {
        recordID = rankList.recordID;
        noRank.SetActive(false);
        recordInfos.SetActive(false);
        rankInfos.SetActive(true);

        if (rankList.sceneName == "FTE_9")
        {
            bossLevelAndScore.SetActive(true);
            rankStarsAndScore.SetActive(false);

            bossLevel.text = rankList.bossLevel.ToString();
            bossLevelScore.text = rankList.score.ToString();
            if (isGroup)
            {
                group.SetActive(true);
                global.SetActive(false);
                groupRankPlayerName.text = NetworkMgr.My.playerDatas.playerName;// rankList.playerName;
                
            }
            else
            {
                group.SetActive(false);
                global.SetActive(true);
                globalRankPlayerName.text = NetworkMgr.My.playerDatas.playerName;// rankList.playerName;
                globalRankGroupName.text = rankList.groupName;
                
            }
        }
        else
        {
            bossLevelAndScore.SetActive(false);
            rankStarsAndScore.SetActive(true);
            SetStars(rankStars, rankList.stars);
            starsScore.text = rankList.score.ToString();

            if (isGroup)
            {
                group.SetActive(true);
                global.SetActive(false);
                groupRankPlayerName.text = NetworkMgr.My.playerDatas.playerName;// rankList.playerName;

            }
            else
            {
                group.SetActive(false);
                global.SetActive(true);
                globalRankPlayerName.text = NetworkMgr.My.playerDatas.playerName;// rankList.playerName;
                globalRankGroupName.text = rankList.groupName;
                
            }
        }
        rank = rankList.rank;
        rankText.text = rank.ToString();
        if (rank <= 3)
        {
            rankSprite.sprite = RankPanel.My.rankSprites[rank - 1];

            rankSprite.gameObject.SetActive(true);
        }
        else
        {
            rankSprite.gameObject.SetActive(false);
        }
    }

    private void SetStars(Transform stars, string star)
    {
        if (star[0] == '0')
        {
            stars.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            stars.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }

        if (star[1] == '0')
        {
            stars.GetChild(1).GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            stars.GetChild(1).GetChild(0).gameObject.SetActive(true);
        }

        if (star[2] == '0')
        {
            stars.GetChild(2).GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            stars.GetChild(2).GetChild(0).gameObject.SetActive(true);
        }
    }
}
