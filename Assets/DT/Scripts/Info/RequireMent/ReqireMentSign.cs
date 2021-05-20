using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReqireMentSign : MonoBehaviour
{



    public GameObject ison;
    public Text str;
    public Text effstr;
    public void Init(bool isuseTSJ,Requirement requirement)
    {
        if (isuseTSJ)
        {

        str.text = requirement.requirementData.requirementDesc;
        effstr.text = requirement.requirementData.effectDesc;
        if (requirement.isOpen)
        {
            ison.SetActive(true);
        }
        else
        {
            ison.SetActive(false);
            
        }
        }
        else
        {
            str.text = "???";
            effstr.text ="???";;
            if (requirement.isOpen)
            {
                ison.SetActive(true);
            }
            else
            {
                ison.SetActive(false);

            }
        }
    }
}
