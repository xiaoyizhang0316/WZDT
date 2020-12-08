using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BoxEffect : MonoBehaviour
{
    public Button box;
    // Start is called before the first frame update
    void Start()
    {
        if (box.interactable)
        {
             
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (box.interactable)
        {
            transform.localEulerAngles+=new Vector3(0,0,1);
        }
    }
}
