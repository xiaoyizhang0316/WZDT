using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject map;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int j = 0; j< 40; j++)
        {
            Vector3 ad;
            if (j % 2==0)
            {
                ad = new Vector2( 0.85f,0);
            }
            else
            {
                ad = new Vector2( 0f,0);
            }

            for (int i = 0; i < 40; i++)
            {
                GameObject game= Instantiate(map); 
                game.transform.position += new Vector3(i*1.7f,0,j*1.462f)+ad; 
            }
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
