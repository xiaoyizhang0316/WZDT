using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_1_5_Dialog5 : BaseGuideStep
{
    public GameObject openCG;
    public Transform mega;
    public List<GameObject> roleTechs;
    public override IEnumerator StepStart()
    {
        openCG.SetActive(true);
        StageGoal.My.playerTechPoint = 0;
        StageGoal.My.playerTechText.text = "0";
        mega.DOScale(Vector3.one, 0.5f).Play().OnPause(() =>
        {
            mega.DOScale(Vector3.one, 0.5f).Play();
        });
        for (int i = 0; i < roleTechs.Count; i++)
        {
            roleTechs[i].SetActive(true);
        }
        yield return new WaitForSeconds(0.5f);
    }

    public override bool ChenkEnd()
    {
        return !openCG.activeInHierarchy;
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(0.5f);
    }

}
