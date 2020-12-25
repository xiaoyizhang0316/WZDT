using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FTE_2_5_Goal5 : BaseGuideStep
{
    public GameObject bornPoint1;
    public GameObject bornPoint2;
    public GameObject bornPoint3;

    public GameObject endPanel;

    private List<BaseMapRole> dealers;
    public override IEnumerator StepStart()
    {
        FTE_2_5_Manager.My.isClearGoods = false;
        NewCanvasUI.My.GameNormal();
        bornPoint1.GetComponent<Building>().BornEnemyForFTE_2_5(302);
        bornPoint2.GetComponent<Building>().BornEnemyForFTE_2_5(301);
        bornPoint3.GetComponent<Building>().BornEnemyForFTE_2_5(-1);
        InvokeRepeating("CheckGoal", 1f, 0.1f);
        InvokeRepeating("CheckBuffOut", 3, 30);
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        endPanel.GetComponent<Button>().onClick.AddListener(() =>
        {
            NetworkMgr.My.UpdatePlayerFTE("2.5", ()=>SceneManager.LoadScene("Map"));
        });
        yield return new WaitForSeconds(1f);
        endPanel.SetActive(true);
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish&&missiondatas.data[1].isFinish&&missiondatas.data[2].isFinish&&missiondatas.data[3].isFinish;
    }

    void CheckGoal()
    {
        
    }

    void CheckBuffOut()
    {
        if (dealers == null)
        {
            dealers = new List<BaseMapRole>();
        }
        dealers.Clear();
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType ==
                GameEnum.RoleType.Dealer)
            {
                dealers.Add(PlayerData.My.MapRole[i]);
            }
        }

        for (int i = 0; i < dealers.Count; i++)
        {
            if (dealers[i] != null)
            {
                for (int j = 0; j < dealers[i].warehouse.Count; j++)
                {
                    if (dealers[i].warehouse[j].buffList.Count > 2)
                    {
                        HttpManager.My.ShowTip("有口味效果被顶掉！");
                        break;
                    }
                }
            }
        }
    }
}
