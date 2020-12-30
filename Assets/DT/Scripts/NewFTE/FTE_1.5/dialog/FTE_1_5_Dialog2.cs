using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_5_Dialog2 : BaseGuideStep
{
    public GameObject openCG;
    public Transform MerchantRoleSign;
    public override IEnumerator StepStart()
    {
        openCG.SetActive(true);
        MerchantRoleSign.GetComponent<CreatRole_Button>().enabled = true;
        MerchantRoleSign.GetChild(2).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
    }

    public override bool ChenkEnd()
    {
        return !openCG.activeInHierarchy;
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(0.5f);
    }

}
