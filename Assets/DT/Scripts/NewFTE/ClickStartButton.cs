using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ClickStartButton : BaseGuideStep
{
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
        if (DOTween.defaultAutoPlay == AutoPlay.All)
            return true;
        else
            return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
