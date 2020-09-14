using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_26 : BaseGuideStep
{

    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    
    public override IEnumerator StepStart()
    {
     
        yield return new WaitForSeconds(0.2f); 
    }

    public override IEnumerator StepEnd()
    {
        //panel.SetActive(false);
        yield break;
    }

    public override bool ChenkEnd()
    {
        return !panel.activeInHierarchy;
    }
}
