using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FTE_01_Step_3 : BaseGuideStep
{
    bool isOver = false;
    public GameObject hand;
    private void Start()
    {
        isOver = false;
    }
    public override IEnumerator StepEnd()
    {
        yield break;
    }

    public override IEnumerator StepStart()
    {
        transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => {
            RoleListManager.My.OutButton();
            Destroy(transform.GetChild(0).gameObject);
            //isOver = true;
            HideInfos();
            hand.SetActive(false);
            StartCoroutine(Over());
        });
        StartCoroutine(OwnStep());
        //hand.SetActive(true);
        yield break;
    }

    public override bool ChenkEnd()
    {
        return isOver;
    }

    IEnumerator OwnStep()
    {
        yield return new WaitForSeconds(1);
        hand.SetActive(true);
        ShowInfos();
    }

    IEnumerator Over()
    {
        yield return new WaitForSeconds(0.5f);
        isOver = true;
    }

    void ShowInfos()
    {
        for (int i = 0; i < contentText.Count; i++)
        {
            contentText[i].gameObject.SetActive(true);
        }
    }

    void HideInfos()
    {
        for (int i = 0; i < contentText.Count; i++)
        {
            contentText[i].gameObject.SetActive(false);
        }
    }
}
