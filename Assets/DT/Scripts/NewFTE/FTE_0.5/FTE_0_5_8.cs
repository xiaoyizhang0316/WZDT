using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_8 : BaseGuideStep
{
    public BaseMapRole role1;

    public BaseMapRole role2;

    public BaseMapRole role0;
    public List<GameObject> land;
    public List<GameObject> Seedtesting;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        PlayerData.My.DeleteRole(role0.baseRoleData.ID);
        yield return new WaitForSeconds(1f);
        for (int i = 0; i <land.Count; i++)
        {
            land[i].transform.DOLocalMoveY(-5, 1f).Play();
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i <land.Count; i++)
        {
            land[i].transform.DOLocalMoveY(0, 1f).Play();
       
        }

        for (int i = 0; i < Seedtesting.Count; i++)
        {
            Seedtesting[i].SetActive(true);
            Seedtesting[i].transform.DOLocalMoveY(0.3f, 1f).Play();

        }
    }

    public override IEnumerator StepEnd()
    {
        yield break;
    }


    public override bool ChenkEnd()
    {
        for (int j = 0; j < role1.warehouse.Count; j++)
        {
            if (role1.warehouse[j].damage < 50)
            {
                role1.warehouse.Remove(role1.warehouse[j]);
            }
        }
        for (int j = 0; j <  role2.warehouse.Count; j++)
        {
            if (role2.warehouse[j].damage < 50)
            {
                role2.warehouse.Remove(role2.warehouse[j]);
            }
        }

        missiondatas.data[0].currentNum = role1.warehouse.Count;
        missiondatas.data[1].currentNum = role2.warehouse.Count;

        if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
        {
            missiondatas.data[0].isFinish = true;
           
        }

        if (missiondatas.data[1].currentNum >= missiondatas.data[1].maxNum)
        {
            missiondatas.data[1].isFinish = true;
           
        }

        if (missiondatas.data[0].isFinish && missiondatas.data[1].isFinish)
        {
            return true;
        }

        return false;
    }
}