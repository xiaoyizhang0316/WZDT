using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo.Demos;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;

public class SoftFTE_Video : SoftFTE_Base
{

    public VCR vcr;

    public Text roleVideoText;

    public override void Init(RoleType type)
    {
        SoftFTEItem item = SoftFTEData.My.GetSoftItemByType(type);
        if (item != null)
        {
            roleVideoText.text = item.roleVideoDesc;
        }
        vcr._videoFiles[0] = type.ToString() + ".mp4";
        vcr._videoFiles[1] = type.ToString() + ".mp4";
        base.Init(type);
        nextButton.onClick.AddListener(() =>
        {
            vcr.PlayingPlayer.CloseVideo();
            vcr.isFinishWatching = false;
        });
    }

    public override void Play()
    {
        base.Play();
        vcr.OnOpenVideoFile();
    }

    public override IEnumerator Check()
    {
        while(!vcr.isFinishWatching)
        {
            yield return null;
        }
        nextButton.interactable = true;
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
