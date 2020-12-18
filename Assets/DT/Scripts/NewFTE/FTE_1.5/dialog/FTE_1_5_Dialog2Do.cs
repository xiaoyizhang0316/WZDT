using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_5_Dialog2Do : FTE_DialogDoBase
{
    public Transform merchant;
    public override void DoStart()
    {
        merchant.GetComponent<CreatRole_Button>().enabled = true;
        merchant.GetChild(2).gameObject.SetActive(false);
    }

    public override void DoEnd()
    {
        
    }
}
