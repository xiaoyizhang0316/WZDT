using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_0_Step_3 : BaseGuideStep
{
    public GameObject hand;

    public BaseMapRole role;
    public GameObject dustDirtyPoofSoft;
    //public GameObject text;

    private void Start()
    {
        StartCoroutine(OwnStep());
    }

    public override IEnumerator StepEnd()
    {
        Debug.Log("结束教学 " + currentStepIndex);
        yield break;
    }

    public override IEnumerator StepStart()
    {
        Debug.Log("开始教学 " + currentStepIndex);
        afterEntry = HandMove;
        
        yield return new WaitForSeconds(0.2f);

        ShowInfos();
    }

    IEnumerator OwnStep()
    {
        yield return new WaitForSeconds(0.5f);
        role.transform.DOMoveY(0.3f, 1f).OnComplete(() => {
            GameObject go = Instantiate(dustDirtyPoofSoft, role.transform);
            Destroy(go, 1f);
            role.transform.DOScale(new Vector3(1.3f, 0.8f, 1.3f), 0.2f).OnComplete(() => {
                role.transform.DOScale(1f, 0.15f);
            });
        });
    }

    void HandMove()
    {
        hand.SetActive(true);
    }

    void ShowInfos()
    {
        for(int i=0; i<contentText.Count; i++)
        {
            contentText[i].gameObject.SetActive(true);
        }
    }
}
