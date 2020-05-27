using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using IOIntensiveFramework.MonoSingleton;
using static GameEnum;

public class MapManager : MonoSingleton<MapManager>
{

    public List<MapSign>_mapSigns = new List<MapSign>();

    public List<GameObject> mapTypeList;

    private float interval;

    private Vector3 medium = Vector3.zero;

    private Vector3 high = new Vector3(0f, 0.6f, 0f);

    public bool generatePath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// 检测地块是否能放置建筑
    /// </summary>
    /// <param name="xList"></param>
    /// <param name="yList"></param>
    /// <returns></returns>
    public bool CheckLandAvailable(List<int> xList,List<int> yList)
    {
        int height = 100000;
        for (int i = 0; i < xList.Count; i++)
        {
            MapSign temp = GetMapSignByXY(xList[i], yList[i]);
            if (temp == null)
            {
                return false;
            }
            else
            {
                Debug.Log(temp.isCanPlace);
                if (!temp.isCanPlace)
                    return false;
                if (height != temp.height && height != 100000)
                    return false;
                else
                    height = temp.height;
            }
        }
        return true;
    }

    public MapSign GetMapSignByXY(int x,int y)
    {
        foreach(MapSign m in _mapSigns)
        {
            if (m.x == x && m.y == y)
                return m;
        }
        return null;
    }

    /// <summary>
    /// 设置地块占用
    /// </summary>
    /// <param name="xList"></param>
    /// <param name="yList"></param>
    public void SetLand(List<int> xList, List<int> yList)
    {
        for (int i = 0; i < xList.Count; i++)
        {
            MapSign temp = GetMapSignByXY(xList[i], yList[i]);
            if (temp != null)
            {
                temp.isCanPlace = false;
            }
        }
    }

    /// <summary>
    /// 解除地块占用
    /// </summary>
    /// <param name="xList"></param>
    /// <param name="yList"></param>
    public void ReleaseLand(List<int> xList, List<int> yList)
    {
        for (int i = 0; i < xList.Count; i++)
        {
            MapSign temp = GetMapSignByXY(xList[i], yList[i]);
            if (temp != null)
            {
                temp.isCanPlace = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        interval += Time.deltaTime;
        if (interval >= 0.1f)
        {
            //平草地
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
                        go.transform.position = new Vector3(hit[i].transform.position.x, go.transform.position.y, hit[i].transform.position.z);
                        int tempX = hit[i].transform.GetComponent<MapSign>().x;
                        int tempY = hit[i].transform.GetComponent<MapSign>().y;
                        go.GetComponent<MapSign>().x = tempX;
                        go.GetComponent<MapSign>().y = tempY;
                        Destroy(hit[i].transform.gameObject);
                        break;
                    }
                }
                interval = 0f;
            }
            //高草地
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
                        go.transform.position = new Vector3(hit[i].transform.position.x, go.transform.position.y, hit[i].transform.position.z);
                        int tempX = hit[i].transform.GetComponent<MapSign>().x;
                        int tempY = hit[i].transform.GetComponent<MapSign>().y;
                        go.GetComponent<MapSign>().x = tempX;
                        go.GetComponent<MapSign>().y = tempY;
                        Destroy(hit[i].transform.gameObject);
                        break;
                    }
                }
                interval = 0f;
            }
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
                        go.transform.position = new Vector3(hit[i].transform.position.x, go.transform.position.y, hit[i].transform.position.z);
                        int tempX = hit[i].transform.GetComponent<MapSign>().x;
                        int tempY = hit[i].transform.GetComponent<MapSign>().y;
                        go.GetComponent<MapSign>().x = tempX;
                        go.GetComponent<MapSign>().y = tempY;
                        Destroy(hit[i].transform.gameObject);
                        break;
                    }
                }
                interval = 0f;
            }
            if (Input.GetKey(KeyCode.Alpha4))
            {
                print("press 3");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hit = Physics.RaycastAll(ray);
                for (int i = 0; i < hit.Length; i++)
                {
                    print(hit[i].transform);
                    if (hit[i].transform.tag.Equals("MapLand"))
                    {
                        GameObject go = Instantiate(mapTypeList[3], transform);
                        go.transform.position = new Vector3(hit[i].transform.position.x, go.transform.position.y, hit[i].transform.position.z);
                        int tempX = hit[i].transform.GetComponent<MapSign>().x;
                        int tempY = hit[i].transform.GetComponent<MapSign>().y;
                        go.GetComponent<MapSign>().x = tempX;
                        go.GetComponent<MapSign>().y = tempY;
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
                        hit[i].transform.localPosition += new Vector3(0f, 0.3f, 0f);
                        hit[i].transform.GetComponent<MapSign>().height += 1;
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
                        hit[i].transform.localPosition += new Vector3(0f, -0.3f, 0f);
                        hit[i].transform.GetComponent<MapSign>().height -= 1;
                    }
                    break;
                }
                interval = 0f;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hit = Physics.RaycastAll(ray);
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].transform.tag.Equals("MapLand"))
                    {
                        hit[i].transform.GetComponent<MeshRenderer>().enabled = true;
                    }
                    break;
                }
                interval = 0f;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hit = Physics.RaycastAll(ray);
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].transform.tag.Equals("MapLand"))
                    {
                        hit[i].transform.GetComponent<MeshRenderer>().enabled = false;
                    }
                    break;
                }
                interval = 0f;
            }
        }
    }
}
