using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo.Demos;
using UnityEngine;

public class VideoStep : BaseGuideStep
{
    public VCR vcr;

    public override IEnumerator StepEnd()
    {
        Destroy(vcr.transform.parent.gameObject);
        yield break;
    }

    public override IEnumerator StepStart()
    {
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        if (vcr.isFinishWatching)
        {
            endButton.interactable = true;
        }
    }
}
