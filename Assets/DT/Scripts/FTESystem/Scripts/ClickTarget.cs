using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickTarget : MonoBehaviour
{
    public float waitTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.LogError("m down");
        if (!gameObject.activeInHierarchy)
        {
            Debug.LogError("not active");
            return;
        }
        GetComponent<Image>().raycastTarget = false;

        FTEManager.My.NextStep(waitTime);
        FTEManager.My.isOnFTE = false;
    }

    private void OnMouseUp()
    {
        Debug.LogError("m up");
        if (!gameObject.activeInHierarchy)
        {
            Debug.LogError("not active");
            return;
        }
        FTEManager.My.isOnFTE = true;
    }
}
