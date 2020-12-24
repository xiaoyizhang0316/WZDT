using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_2_5_Dialog2Do : FTE_DialogDoBase
{
    public GameObject package;
    public GameObject sale;
    public override void DoStart()
    {
        package.SetActive(true);
        sale.SetActive(true);
    }

    public override void DoEnd()
    {
        
    }
}
