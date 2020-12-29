using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FTE_2_5_DialogNewAdd : FTE_DialogDoBase
{
    public Button updateButton;
    public override void DoStart()
    {
        updateButton.interactable = false;
        updateButton.GetComponent<UpdateRole>().enabled = false;
    }

    public override void DoEnd()
    {
        
    }
}
