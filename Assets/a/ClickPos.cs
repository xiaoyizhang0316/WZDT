using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPos : MonoBehaviour
{

    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject go = Instantiate(cube, transform);
                go.transform.position = new Vector3(i * 5 - 22.5f,1f,j*5 - 22.5f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;
        //     if (Physics.Raycast(inputRay, out hit))
        //     {
        //         Debug.Log(hit.point);
        //         cube.transform.localPosition = hit.point;
        //     }
        // }
    }
}
