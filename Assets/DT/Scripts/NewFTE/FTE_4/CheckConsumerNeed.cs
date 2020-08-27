using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckConsumerNeed : BaseGuideStep
{
    public Transform waveTF;

    public override IEnumerator StepEnd()
    {
        yield break;
    }

    public override IEnumerator StepStart()
    {
        GameObject go = Instantiate(waveTF.GetChild(3).gameObject, transform);
        go.transform.position = waveTF.GetChild(3).position;
        go.transform.SetAsFirstSibling();
        go.gameObject.SetActive(true);
        highLightCopyObj.Add(go);
        yield break;
    }

    public override bool ChenkEnd()
    {
        return waveTF.parent.transform.Find("WaveBg").childCount > 0;
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
