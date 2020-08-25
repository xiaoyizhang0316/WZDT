using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo.Demos;
using UnityEngine;

public class VideoStep : BaseGuideStep
{
    public VCR vcr;

    public override IEnumerator StepEnd()
    {
        yield break;
    }

    public override IEnumerator StepStart()
    {
        yield break;
    }

    // Start is called before the first frame update
    void Start()
    {
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
