using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T7_Dialog3 : FTE_Dialog
{
    public GameObject startRole;
    public GameObject endRole;
    public GameObject first_btn;
    public GameObject last_btn;
    public override void BeforeDialog()
    {
        T7_Manager.My.peasant.GetComponent<BaseMapRole>().encourageLevel = -3;
        T7_Manager.My.peasant.GetComponent<BaseMapRole>().startEncourageLevel = -3;
        startRole.SetActive(true);
        endRole.SetActive(true);
        first_btn.GetComponent<Button>().enabled = true;
        last_btn.GetComponent<Button>().enabled = true;
        last_btn.GetComponent<Button>().interactable = true;
        first_btn.GetComponent<Button>().interactable = true;
        first_btn.transform.GetChild(0).GetComponent<Text>().text = "先结算";
        last_btn.transform.GetChild(0).GetComponent<Text>().text = "后结算";
    }
}
