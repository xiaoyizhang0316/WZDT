using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class CreateBaseMap : MonoBehaviour
{
    public int X;

    public int Y;

    public GameObject land;

    public float yBuffer;

    public Toggle destory;
    public Toggle gt;
    public Toggle ht;
    public Toggle rt;
    public Toggle nt;


    public GameObject grass;

    public GameObject highGrass;

    public GameObject road;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                GameObject tf = Instantiate(land, transform);
                if (j % 2 == 1)
                {
                    tf.transform.position = new Vector3(i * 1.725f + yBuffer, 0, j * 1.5f);
                }
                else
                {
                    tf.transform.position = new Vector3(i * 1.725f, 0, j * 1.5f);
                }
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            if (Input.GetMouseButton(0) )
            {
                if (destory.isOn)
                {
                    hit.transform.GetComponent<MeshRenderer>().enabled = false;
                }
                else
                {
                    hit.transform.GetComponent<MeshRenderer>().enabled = true;
                }

                if (gt.isOn)
                {
             
                    Vector3  v3= hit.transform.position;
                    Destroy(hit.transform.gameObject);
                    GameObject tf = Instantiate(grass, transform);
                    tf.transform.position =new Vector3(v3.x,0,v3.z);


                }
                if (ht.isOn)
                {
                     
                    
                    Vector3  v3= hit.transform.position;
                    Destroy(hit.transform.gameObject);
                    GameObject tf = Instantiate(highGrass, transform);
                    
                    tf.transform.position =new Vector3(v3.x,0,v3.z);

                }
                if (rt.isOn)
                {
               
                    Vector3  v3= hit.transform.position;
                    Destroy(hit.transform.gameObject);
                    GameObject tf = Instantiate(road, transform);
                    tf.transform.position =new Vector3(v3.x,0,v3.z);

                }
                if (nt.isOn)
                {
                   
                    Vector3  v3= hit.transform.position;
                    Destroy(hit.transform.gameObject);
                    GameObject tf = Instantiate(land, transform);
                    tf.transform.position =new Vector3(v3.x,0,v3.z);


                }
            }
        }
    }
} 