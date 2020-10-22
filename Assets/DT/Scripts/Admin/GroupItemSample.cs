using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupItemSample : MonoBehaviour
{
    #region UI
    public Text names;
    public List<Text> levels;
    #endregion

    public void Setup(GroupItemInfo info)
    {
        names.text = info.itemName;
        for(int i=0; i<levels.Count; i++)
        {
            if (i < info.levels.Count)
            {

                if (info.isFloat)
                {
                
                    if (info.isPercent)
                    {
                        levels[i].text = (info.levels[i] * 100).ToString("F2") + "%";
                    }
                    else
                    {
                        //levels[i].text = (info.levels[i] / 60).ToString("F2") + "m";
                        levels[i].text = AdminManager.My.GetTimeString((int)info.levels[i]);
                    }
                }
                else
                {
                    levels[i].text = ((int)info.levels[i]).ToString();
                }
            }
            else
            {
                if (info.isFloat)
                {

                    if (info.isPercent)
                    {
                        levels[i].text = "0%";
                    }
                    else
                    {
                        levels[i].text = "0m";
                    }
                }
                else
                {
                    levels[i].text = "0";
                }
            }
        }
    }

    public void Setup(GroupPlayerLevelPlayCount gplpc, bool isTime=false)
    {
        names.text = gplpc.playerName;
        if (isTime)
        {
            //levels[0].text = ((float)gplpc.level1/60).ToString("F2")+"m";
            //levels[1].text = ((float)gplpc.level2 / 60).ToString("F2") + "m";
            //levels[2].text = ((float)gplpc.level3 / 60).ToString("F2") + "m";
            //levels[3].text = ((float)gplpc.level4 / 60).ToString("F2") + "m";
            //levels[4].text = ((float)gplpc.level5 / 60).ToString("F2") + "m";
            //levels[5].text = ((float)gplpc.level6 / 60).ToString("F2") + "m";
            //levels[6].text = ((float)gplpc.level7 / 60).ToString("F2") + "m";
            //levels[7].text = ((float)gplpc.level8 / 60).ToString("F2") + "m";
            //levels[8].text = ((float)gplpc.level9 / 60).ToString("F2") + "m";
            levels[0].text = AdminManager.My.GetTimeString(gplpc.level1);
            levels[1].text = AdminManager.My.GetTimeString(gplpc.level2);
            levels[2].text = AdminManager.My.GetTimeString(gplpc.level3);
            levels[3].text = AdminManager.My.GetTimeString(gplpc.level4);
            levels[4].text = AdminManager.My.GetTimeString(gplpc.level5);
            levels[5].text = AdminManager.My.GetTimeString(gplpc.level6);
            levels[6].text = AdminManager.My.GetTimeString(gplpc.level7);
            levels[7].text = AdminManager.My.GetTimeString(gplpc.level8);
            levels[8].text = AdminManager.My.GetTimeString(gplpc.level9);
        }
        else
        {

            levels[0].text = gplpc.level1.ToString();
            levels[1].text = gplpc.level2.ToString();
            levels[2].text = gplpc.level3.ToString();
            levels[3].text = gplpc.level4.ToString();
            levels[4].text = gplpc.level5.ToString();
            levels[5].text = gplpc.level6.ToString();
            levels[6].text = gplpc.level7.ToString();
            levels[7].text = gplpc.level8.ToString();
            levels[8].text = gplpc.level9.ToString();
        }
    }
}
