using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckConsumerBuff : BaseGuideStep
{
    public Transform targetConsumePanel;

    public Transform buffContent;

    public Transform buffCopy;

    public override IEnumerator StepEnd()
    {
        yield break;
    }

    public override IEnumerator StepStart()
    {
        GameObject go = Instantiate(targetConsumePanel.GetChild(0).gameObject, transform);
        go.transform.position = targetConsumePanel.GetChild(0).position;
        go.transform.SetAsFirstSibling();
        go.gameObject.SetActive(true);
        highLightCopyObj.Add(go);
        GameObject goCopy = Instantiate(buffContent.gameObject, transform);
        goCopy.transform.position = buffContent.position;
        goCopy.transform.SetAsFirstSibling();
        goCopy.gameObject.SetActive(true);
        highLightCopyObj.Add(goCopy);
        buffCopy = goCopy.transform;
        Destroy(buffCopy.GetComponent<FloatWindow>());
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        if (buffCopy != null)
        {
            buffCopy.GetComponentInChildren<Text>().text = buffContent.GetComponentInChildren<Text>().text;
            buffCopy.position = buffContent.position;
        }

    }
}
