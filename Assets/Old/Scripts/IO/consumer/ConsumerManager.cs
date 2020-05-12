using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;

public class ConsumerManager : MonoSingleton<ConsumerManager>
{
    /// <summary>
    /// 所有消费者的list
    /// </summary>
    public List<ConsumeSign> consumeList = new List<ConsumeSign>();

    /// <summary>
    /// 所有消费者生成点的list
    /// </summary>
    public List<GameObject> consumerSpawner = new List<GameObject>();

    /// <summary>
    /// 消费者消失点
    /// </summary>
    public List<GameObject> consumerOutPlace = new List<GameObject>();

    public GameObject gameObject;
    /// <summary>
    /// 生成消费者
    /// </summary>
    /// <param name="num"></param>
    public void SpawnConsume()
    {
        int totalBrand = 0;
        int count = 0;
        foreach (BaseMapRole b in PlayerData.My.MapRole)
        {
            if (b.shop.Count > 0)
            {
             //   totalBrand += b.baseRoleData.brand;
                count++;
            }
        }
        int num;
        if (count == 0)
        {
            num = 10;
        }
        else
        {
            num = (int)(totalBrand / count * 0.8f);
            if (num > 30)
            {
                num = 30;
            }

        }

        for (int i = 0; i < num; i++)
        {
            SpawnNewConsume();
        }
    }


    ///// <summary>
    ///// 生成指定数量的消费者
    ///// </summary>
    ///// <param name="num"></param>
    //public void SpawnConsume(int num)
    //{
    //    for (int i = 0;i < num; i++)
    //    {
    //        SpawnNewConsume();
    //    }
    //}

    /// <summary>
    /// 生成单个消费者
    /// </summary>
    public void SpawnNewConsume()
    {
        int index = Random.Range(0, consumerSpawner.Count);
        int sex = Random.Range(0, 2);
        string path;
        if (sex == 0)
            path = "Prefabs/Consumer/Male/male_";
        else
            path = "Prefabs/Consumer/Female/female_";
        int num = Random.Range(1, 11);
        path += num.ToString();
        //print(path);
        //GameObject go = Instantiate(Resources.Load<GameObject>(path), consumerSpawner[index].transform.position, consumerSpawner[index].transform.rotation, consumerSpawner[index].transform);
        //go.GetComponent<ConsumeSign>().Init(sex);
    }

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnConsume",10f,6f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
