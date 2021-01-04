using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_7 : BaseGuideStep
{
    public BaseMapRole role;

    public GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        role.warehouse.Clear();
        role.OnMoved += ChangeColor;

        yield return new WaitForSeconds(1f);
    }

    public override IEnumerator StepEnd()
    {
        yield break;
    }

    private bool isshow ;

    public override bool ChenkEnd()
    {
        if (NewCanvasUI.My.Panel_AssemblyRole.activeSelf&&!isshow)
        {
            ShowEffect();
            isshow = true;
        }

        for (int j = 0; j < role.warehouse.Count; j++)
        {
            if (role.warehouse[j].damage < 240)
            {
                role.warehouse.Remove(role.warehouse[j]);
            }
        }


        missiondatas.data[0].currentNum = role.warehouse.Count;
        if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
        {
            missiondatas.data[0].isFinish = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShowEffect()
    {
        EquipListManager.My._signs[0].effect.DOFade(0, 0.6f).OnComplete(() =>
        {
            EquipListManager.My._signs[0].effect.DOFade(1, 0.6f).Play();
        }).SetLoops(10).Play();

        EquipListManager.My._signs[1].effect.DOFade(0, 0.6f).OnComplete(() =>
        {
            EquipListManager.My._signs[1].effect.DOFade(1, 0.6f).Play();
        }).SetLoops(10).Play();

        EquipListManager.My._signs[2].effect.DOFade(0, 0.6f).OnComplete(() =>
        {
            EquipListManager.My._signs[2].effect.DOFade(1, 0.6f).Play();
        }).SetLoops(10).Play();

        EquipListManager.My._signs[3].effect.DOFade(0, 0.6f).OnComplete(() =>
        {
            EquipListManager.My._signs[3].effect.DOFade(1, 0.6f).Play();
        }).SetLoops(10).Play();

        effect.transform.GetComponent<Text>().DOFade(0, 0.6f).OnComplete(() =>
        {
            effect.transform.GetComponent<Text>().DOFade(1, 0.6f).Play();
        }).SetLoops(10).Play();
    }

    public void ChangeColor(ProductData data)
    {
        if (data.damage > 240)
        {
            FTE_0_5Manager.My.ChangeColor(FTE_0_5Manager.My.seerJC1_ran, FTE_0_5Manager.My.sg);
        }

        else
        {
            FTE_0_5Manager.My.ChangeColor(FTE_0_5Manager.My.seerJC1_ran, FTE_0_5Manager.My.sr);
        }
    }
}