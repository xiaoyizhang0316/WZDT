using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordItem : MonoBehaviour
{
    public Text recordTime;

    public Text score;

    public Text isWin;

    public Text playTimeCount;

    public string Id;

    public int playTime;

    public void Init(ReplayList replay)
    {
        recordTime.text = TimeStamp.TimeStampToString(replay.recordTime);
        score.text = replay.score.ToString();
        Id = replay.recordID;
        playTime = replay.timeCount;
        playTimeCount.text = playTime.ToString();
        if (replay.win)
        {
            isWin.text = "成功";
            isWin.color = Color.green;
        }
        else
        {
            isWin.text = "失败";
            isWin.color = Color.red;
        }
        GetComponent<Button>().onClick.AddListener(GetReplayById);
    }

    public void GetReplayById()
    {
        NetworkMgr.My.GetReplayDatas(Id, (datas) =>
        {
            string str = "{ \"playerOperations\":" + datas.operations + "}";
            PlayerOperations operations = JsonUtility.FromJson<PlayerOperations>(str);
            string str1 = "{ \"dataStats\":" + datas.dataStats + "}";
            PlayerStatus status = JsonUtility.FromJson<PlayerStatus>(str1);
            Debug.Log("获取录像成功");
            ReviewPanel.My.MapInit(operations.playerOperations, status.dataStats,playTime);
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
