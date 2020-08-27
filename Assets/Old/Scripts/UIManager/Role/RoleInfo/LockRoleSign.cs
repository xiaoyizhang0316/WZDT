using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockRoleSign : MonoBehaviour
{ 
    public GameObject lockimage;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Button>().interactable)
        {
            lockimage.gameObject.SetActive(false);
        }
        else
        {
            lockimage.gameObject.SetActive(true);
            
        }
    }
}
