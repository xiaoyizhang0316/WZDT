using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUnlockRole : BaseGuideStep, ICanvasRaycastFilter
{
    public NPC targetNpc;

    public override IEnumerator StepEnd()
    {
        yield break;
    }

    public override IEnumerator StepStart()
    {
        yield break;
    }

    public override bool ChenkEnd()
    {
        return !targetNpc.isLock;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
