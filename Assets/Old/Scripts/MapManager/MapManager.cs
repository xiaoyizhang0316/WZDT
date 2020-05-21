using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using IOIntensiveFramework.MonoSingleton;
using static GameEnum;

public class MapManager : MonoSingleton<MapManager>
{

    public List<MapSign>_mapSigns = new List<MapSign>();

    public List<GameObject> mapTypeList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //草地
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            print("press 1");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hit = Physics.RaycastAll(ray);
            for (int i = 0; i < hit.Length; i++)
            {
                print(hit[i].transform);
                if (hit[i].transform.tag.Equals("MapLand"))
                {
                    GameObject go =  Instantiate(mapTypeList[0], transform);
                    go.transform.position = hit[i].transform.position;
                    Destroy(hit[i].transform.gameObject);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            print("press 1");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hit = Physics.RaycastAll(ray);
            for (int i = 0; i < hit.Length; i++)
            {
                print(hit[i].transform);
                if (hit[i].transform.tag.Equals("MapLand"))
                {
                    GameObject go = Instantiate(mapTypeList[1], transform);
                    go.transform.position = hit[i].transform.position;
                    Destroy(hit[i].transform.gameObject);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            print("press 1");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hit = Physics.RaycastAll(ray);
            for (int i = 0; i < hit.Length; i++)
            {
                print(hit[i].transform);
                if (hit[i].transform.tag.Equals("MapLand"))
                {
                    GameObject go = Instantiate(mapTypeList[2], transform);
                    go.transform.position = hit[i].transform.position;
                    Destroy(hit[i].transform.gameObject);
                }
            }
        }
    }
}
