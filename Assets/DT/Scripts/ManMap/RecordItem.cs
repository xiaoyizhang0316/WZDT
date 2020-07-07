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

    public Sprite[] bgSprites;

    public bool isStar1;

    public bool isStar2;

    public bool isStar3;

    /// <summary>
	/// 初始化
	/// </summary>
	/// <param name="replay"></param>
    public void Init(ReplayList replay)
    {
        recordTime.text = TimeStamp.TimeStampToString(replay.recordTime);
        score.text = replay.score.ToString();
        Id = replay.recordID;
        playTime = replay.timeCount;
        playTimeCount.text = playTime.ToString();
        char[] temp = replay.stars.ToCharArray();
        if (replay.win)
        {
            star1.sprite = sprites[0];
            isStar1 = true;
            star2.sprite = temp[1].Equals('1') ? sprites[0] : sprites[1];
            star3.sprite = temp[2].Equals('1') ? sprites[0] : sprites[1];
        }
        else
        {
            isStar1 = false;
            star1.sprite = sprites[1];
            star2.sprite = sprites[1];
            star3.sprite = sprites[1];
        }
        isStar2 = temp[1].Equals('1');
        isStar3 = temp[2].Equals('1');
        GetComponent<Button>().onClick.AddListener(GetReplayById);
        InitBg();
    }

    /// <summary>
	/// 点击调用
	/// </summary>
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

    public void InitBg()
    {
        if (isStar3 && isStar2 && isStar1)
        {
            GetComponent<Image>().sprite = bgSprites[0];
        }
        else if (!isStar1)
        {
            GetComponent<Image>().sprite = bgSprites[2];
        }
        else
        {
            GetComponent<Image>().sprite = bgSprites[1];
        }
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
