using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckClosePanel : BaseGuideStep, ICanvasRaycastFilter
{
    public Transform targetPanel;

    public Transform buffContent;

    public Transform buffCopy;

    public override IEnumerator StepEnd()
    {
        yield break;
    }

    public override IEnumerator StepStart()
    {
        GameObject goCopy = Instantiate(buffContent.gameObject, transform);
        goCopy.transform.position = buffContent.position;
        goCopy.transform.SetAsFirstSibling();
        goCopy.gameObject.SetActive(true);
        highLightCopyObj.Add(goCopy);
        buffCopy = goCopy.transform;
        yield break;
    }

    public override bool ChenkEnd()
    {
        return !targetPanel.gameObject.activeInHierarchy;
    }

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        if (highLight2DObjList.Count == 0)
            return true;
        for (int i = 0; i < highLight2DObjList.Count; i++)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(highLight2DObjList[i].GetComponent<RectTransform>(), sp, eventCamera))
            {
                return false;
            }
        }
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buffCopy != null)
        {
            buffCopy.GetComponentInChildren<Text>().text = buffContent.GetComponentInChildren<Text>().text;
            buffCopy.gameObject.SetActive(buffContent.gameObject.activeInHierarchy);
        }

    }
}
