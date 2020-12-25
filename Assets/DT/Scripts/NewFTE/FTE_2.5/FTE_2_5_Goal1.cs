using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class FTE_2_5_Goal1 : BaseGuideStep
{
    public GameObject checkBorder;
    public GameObject checkImage;
    public GameObject wave;
    public override IEnumerator StepStart()
    {
        /*PlayerData.My.playerGears.Clear();
        PlayerData.My.playerWorkers.Clear();*/
        wave.SetActive(true);
        FTE_2_5_Manager.My.GetComponent<RoleCreateLimit>().needLimit = true;
        InvokeRepeating("CheckGoal", 0.01f, 0.1f);
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(2f);
        WaveCount.My.closeBtn.SetActive(false);
        WaveCount.My.closeBtn.GetComponent<Button>().interactable = true;
        WaveCount.My.waveBg.gameObject.SetActive(false);
        Destroy(wave);
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish;
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false)
        {
            if (checkImage.activeInHierarchy)
            {
                checkBorder.SetActive(true);
                if (checkBorder.GetComponent<MouseOnThis>().isOn)
                {
                    checkBorder.SetActive(false);
                    WaveCount.My.closeBtn.GetComponent<Button>().interactable = false;
                    missiondatas.data[0].isFinish = true;
                }
            }
            else
            {
                checkBorder.SetActive(false);
            }
        }
    }
}
