using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseGuide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (AnsweringPanel.My.isComplete)
        {
            gameObject.SetActive(false);
        }

        else
        {
          
        }

        GetComponent<Button>().onClick.AddListener(() =>
        {
            GuideManager.My.CloseFTE();
            gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
