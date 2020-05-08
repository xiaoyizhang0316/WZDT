using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour,IDragHandler
{
    public  float sensitivityAmt;

    public float x;

    public float y;

    public float Upbrod;
    public float DownBrod;
    public float RightBrod;
    public float LeftBrod;

    public bool upmove;
    public bool downmove;
    public bool leftmove;
    public bool rightmove;
    public float currentupDownmove; 
    public float currentleftrightmove;

    /// <summary>
    /// 滚轮缩放系数
    /// </summary>
    public int v = 3;
    public Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ( UIManager.My.NeedRayCastPanel())
        {
            return;
        }

        Camera.main.orthographicSize -=Input.GetAxis("Mouse ScrollWheel")* v ;
        if (Camera.main.orthographicSize <10  )
        {
            Camera.main.orthographicSize = 10;
        }

        if (Camera.main.orthographicSize > 40)
        {
            Camera.main.orthographicSize = 40;
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 p0 = Camera.main.transform.position;
            Vector3 p01 = p0 - Camera.main.transform.right * Input.GetAxisRaw("Mouse X") * sensitivityAmt * Time.timeScale;
            Vector3 p03 = p01 - Camera.main.transform.up * Input.GetAxisRaw("Mouse Y") * sensitivityAmt * Time.timeScale;
            Camera.main.transform.position = p03;
        }

//        Debug.Log(Camera.main.transform.position);
        /*if (Input.mousePosition.y>1075 )
        {
            upmove = true;
            if (currentupDownmove  >Upbrod)
            {
                upmove = false;
            }

        if(upmove)
            {
                
                Vector3 p0 = Camera.main.transform.position;
                currentupDownmove += 0.1f;
                Camera.main.transform.position +=new Vector3(0.72f,0,0.72f);

            }


        
        }
        if (Input.mousePosition.y<5 )
        {
            downmove = true;
         
            if (currentupDownmove<DownBrod)
            {
                downmove = false;
            } 
           if(downmove)
            { 
                Vector3 p0 = Camera.main.transform.position;
                currentupDownmove -= 0.1f;
                Camera.main.transform.position += new Vector3(-0.72f, 0, -0.72f); 
            }

            //  gameObject.transform.localPosition+=new Vector3(-sensitivityAmt,0,0);
          
        }
        if (Input.mousePosition.x<15)
        {
            leftmove = true;
            if ( currentleftrightmove<LeftBrod)
            {
                leftmove = false;

                //    Camera.main.transform.position  = LeftBrod.transform.position ;
            }
           if(leftmove)
            {
             
                Vector3 p0 = Camera.main.transform.position;
                currentleftrightmove -= 0.1f;
                Camera.main.transform.position +=new Vector3( -0.72f,0, 0.72f);

            }

           
        }
        if (Input.mousePosition.x>1915 )
        {
            rightmove = true;
           
            if (currentleftrightmove> RightBrod )
            {
                rightmove = false;

                //Camera.main.transform.position  = RightBrod.transform.position ;
            }
          if(rightmove)
            {
              
                Vector3 p0 = Camera.main.transform.position;
                currentleftrightmove += 0.1f;
                Camera.main.transform.position +=new Vector3( 0.72f,0, -0.72f);

            }

           
          
        }*/
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
}
