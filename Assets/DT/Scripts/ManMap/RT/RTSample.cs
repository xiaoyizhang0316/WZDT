using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RTSample : MonoBehaviour
{
    public Image rank_img;
    public Text rank_text;
    public Text playerName;
    public Text bossLevel;
    public Text score;

    public List<Sprite> rank_pic;
    public GameObject end;
    public GameObject ing;
    public GameObject stop;
    public void Setup(PlayerRTScore prt, int rank)
    {
        rank_text.text = rank.ToString();
        if (rank <= 3)
        {
            rank_img.gameObject.SetActive(true);
            rank_img.sprite = rank_pic[rank - 1];
        }
        playerName.text = prt.playerName;
        bossLevel.text = prt.bossLevel.ToString();
        score.text = prt.score.ToString();
        if (NetworkMgr.My.stopMatch)
        {
            stop.SetActive(true);
        }
        else
        {
            if (prt.isGameEnd)
            {
                end.SetActive(true);
            }
            else
            {
                ing.SetActive(true);
            }
        }
    }
}
