using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FTE_2_5_Dialog7 : BaseGuideStep
{
    public GameObject openCG;
    public GameObject playerScore;
    public override IEnumerator StepStart()
    {
        openCG.SetActive(true);
        StageGoal.My.playerSatisfy = 0;
        StageGoal.My.playerSatisfyText.text = "0";
        playerScore.transform.DOScale(1, 0.5f).Play();
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
