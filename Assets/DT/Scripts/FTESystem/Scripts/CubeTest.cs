using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CubeTest : MonoBehaviour
{
    public GameObject mask;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp()
    {
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return;//为真，则点击在UI上
        //}
        if (mask.activeInHierarchy)
        {
            if (!target.activeInHierarchy)
            {
                return;
            }
        }
        //Debug.Log("click");
    }
}
