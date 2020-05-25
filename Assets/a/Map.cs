using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject map;

    public List<Material> materials;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int j = 0; j< 50; j++)
        {
            Vector3 ad;
            if (j % 2==0)
            {
                ad = new Vector2( 1.75f,0);
            }
            else
            {
                ad = new Vector2( -1.75f,0);
            }

            for (int i = 0; i < 50; i++)
            {
                GameObject game= Instantiate(map);
                game.transform.position += new Vector3(i*7,0,j*6)+ad;
                int number = UnityEngine.Random.Range(0, materials.Count);
                game.GetComponent<MeshRenderer>().material = materials[number];
            }
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
