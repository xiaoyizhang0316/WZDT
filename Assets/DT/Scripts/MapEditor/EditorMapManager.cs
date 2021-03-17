using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;

public class EditorMapManager : MapManager
{
    private float interval;

    private int consumerSpotCount = 0;

    public Transform PlayerStartPointTF;
    public GameObject PlayerStartPointUIPrb;

    public GameObject currentPort;

    public int count=0;
    public void CreatPrb(GameObject game)
    {
        count++;
       GameObject gameobj=  Instantiate(PlayerStartPointUIPrb, PlayerStartPointTF);
       gameobj.GetComponent<PlayStartSign>().port = game;
       gameobj.GetComponentInChildren<Text>().text = count.ToString();
    }

    private void Update()
    {
        interval += Time.deltaTime;
        if (interval >= 0.1f)
        {
             
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hit = Physics.RaycastAll(ray);
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].transform.tag.Equals("MapLand"))
                    {
                        consumerSpotCount++;
                        string path = "Prefabs/Common/ConsumerSpot" + consumerSpotCount.ToString();
                        Vector3 pos = hit[i].transform.position + new Vector3(0f, 0.3f, 0f);
                        GameObject go = Instantiate(Resources.Load<GameObject>(path));
                        go.transform.position = pos;
                    }
                    break;
                }
                interval = 0f;
            }
            //消费者终点
            else if (Input.GetKeyDown(KeyCode.E))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hit = Physics.RaycastAll(ray);
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].transform.tag.Equals("MapLand"))
                    {
                        string path = "Prefabs/Common/EndPoint";
                        Vector3 pos = hit[i].transform.position + new Vector3(0f, 0.3f, 0f);
                        GameObject go = Instantiate(Resources.Load<GameObject>(path));
                        go.transform.position = pos;
                    }
                    break;
                }
                interval = 0f;
            }
        }
    }
    
}
