using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordItem : MonoBehaviour
{
    public Text recordTime;

    public Text score;

    public Text playTimeCount;

    public Image star1;

    public Image star2;

    public Image star3;

    public string Id;

    public int playTime;

    public Sprite[] sprites;

    public void Init(ReplayList replay)
    {
        recordTime.text = TimeStamp.TimeStampToString(replay.recordTime);
        score.text = replay.score.ToString();
        Id = replay.recordID;
        playTime = replay.timeCount;
        playTimeCount.text = playTime.ToString();
        char[] temp = replay.stars.ToCharArray();
        if (replay.win)
            star1.sprite = temp[0].Equals('1') ? sprites[0] : sprites[1];
        else
            star1.sprite = sprites[1];
        star2.sprite = temp[1].Equals('1') ? sprites[0] : sprites[1];
        star3.sprite = temp[2].Equals('1') ? sprites[0] : sprites[1];
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
