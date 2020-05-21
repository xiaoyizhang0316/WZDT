using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using IOIntensiveFramework.MonoSingleton;
using static GameEnum;

public class MapManager : MonoSingleton<MapManager>
{

    public List<MapSign>_mapSigns = new List<MapSign>();

    public List<GameObject> mapTypeList;

    public List<Material> grassMaterials;

    public List<Material> landMaterials;

    public List<Material> roadMaterials;

    private float interval;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        interval += Time.deltaTime;
        if (interval >= 0.2f)
        {
            //草地
            if (Input.GetKey(KeyCode.Alpha1))
            {
                print("press 1");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hit = Physics.RaycastAll(ray);
                for (int i = 0; i < hit.Length; i++)
                {
                    print(hit[i].transform);
                    if (hit[i].transform.tag.Equals("MapLand"))
                    {
                        GameObject go = Instantiate(mapTypeList[0], transform);
                        go.transform.position = hit[i].transform.position;
                        int number = UnityEngine.Random.Range(0, grassMaterials.Count);
                        go.GetComponent<MeshRenderer>().material = grassMaterials[number];
                        Destroy(hit[i].transform.gameObject);
                        break;
                    }
                }
                interval = 0f;
            }
            //土地
            if (Input.GetKey(KeyCode.Alpha2))
            {
                print("press 2");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hit = Physics.RaycastAll(ray);
                for (int i = 0; i < hit.Length; i++)
                {
                    print(hit[i].transform);
                    if (hit[i].transform.tag.Equals("MapLand"))
                    {
                        GameObject go = Instantiate(mapTypeList[1], transform);
                        go.transform.position = hit[i].transform.position;
                        int number = UnityEngine.Random.Range(0, landMaterials.Count);
                        go.GetComponent<MeshRenderer>().material = landMaterials[number];
                        Destroy(hit[i].transform.gameObject);
                        break;
                    }
                }
                interval = 0f;
            }
            //路
            if (Input.GetKey(KeyCode.Alpha3))
            {
                print("press 3");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hit = Physics.RaycastAll(ray);
                for (int i = 0; i < hit.Length; i++)
                {
                    print(hit[i].transform);
                    if (hit[i].transform.tag.Equals("MapLand"))
                    {
                        GameObject go = Instantiate(mapTypeList[2], transform);
                        go.transform.position = hit[i].transform.position;
                        int number = UnityEngine.Random.Range(0, roadMaterials.Count);
                        go.GetComponent<MeshRenderer>().material = roadMaterials[number];
                        Destroy(hit[i].transform.gameObject);
                        break;
                    }
                }
                interval = 0f;
            }
            //地块升高
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                print("press up");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hit = Physics.RaycastAll(ray);
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].transform.tag.Equals("MapLand"))
                    {
                        hit[i].transform.localPosition += new Vector3(0f, 0.5f, 0f);
                    }
                    break;
                }
                interval = 0f;
            }
            //地块降低
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hit = Physics.RaycastAll(ray);
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].transform.tag.Equals("MapLand"))
                    {
                        hit[i].transform.localPosition += new Vector3(0f, -0.5f, 0f);
                    }
                    break;
                }
                interval = 0f;
            }
        }
    }
}
