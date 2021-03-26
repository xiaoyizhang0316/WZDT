using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_0_Step_2 : BaseGuideStep
{
    public GameObject hand;
    public GameObject hand1;

    public List<GameObject> image;
    //public GameObject text;

    public Image propBg;

    public List<GameObject> highLight2DObjListOwn;
    public List<GameObject> ownObjCopy=new List<GameObject>();

    public override IEnumerator StepEnd()
    {
        //Debug.Log("结束教学 " + currentStepIndex);
        if (hand1 != null)
        {
            hand1.SetActive(false);
        }
        yield break;
    }

    public override IEnumerator StepStart()
    {
        //Debug.Log("开始教学 " + currentStepIndex);
        afterEntry = HandMove;
        if (hand1 != null)
        {
            hand1.SetActive(true);
        }
        yield return new WaitForSeconds(1);
        ShowAllHighlightUIOwn();
        ShowInfos();
    }

    void HandMove()
    {
        if(hand!=null)
            hand.SetActive(true);
    }

    void ShowInfos()
    {
        for(int i=0; i< contentText.Count; i++)
        {
            contentText[i].gameObject.SetActive(true);
        }

        for(int i=0; i<image.Count; i++)
        {
            image[i].SetActive(true);
        }
        ShowBg();
    }

    void ShowBg()
    {
        if (propBg != null)
        {
            propBg.color = Color.white;
        }
    }

    public void ShowAllHighlightUIOwn()
    {

        if (highLight2DObjListOwn.Count != 0)
        {
            for (int i = 0; i < highLight2DObjListOwn.Count; i++)
            {
                GameObject go = Instantiate(highLight2DObjListOwn[i], transform);
                go.transform.position = highLight2DObjListOwn[i].transform.position;
                go.transform.SetAsFirstSibling();
                go.gameObject.SetActive(true);
                ownObjCopy.Add(go);
            }
        }
    }
}
