using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelDetailInfo : MonoBehaviour
{
    public Text num;
    public Text playerName;
    public Text time;
    public Text score;

    public void Setup(GroupLevelPassDetail glpd, int num)
    {
        this.num.text = num.ToString();
        playerName.text = glpd.playerName;
        time.text = glpd.time < 10 ? "0" : TimeStamp.TimeStampToString(glpd.time);
        score.text = glpd.score.ToString();
    }

    public void Setup(GroupPlayer gp, int num, bool isTry = false)
    {
        this.num.text = num.ToString();
        playerName.text = gp.playerName;
        if (isTry)
        {
            time.text = "未通过";
        }
        else
        {

            time.text = "未尝试";
        }

        score.text = "";
    }
}
