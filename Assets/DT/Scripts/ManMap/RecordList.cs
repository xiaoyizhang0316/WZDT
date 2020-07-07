using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordList : MonoBehaviour
{
    public Transform contentTF;

    public GameObject itemPrb;

    public void Init(List<ReplayList> operations)
    {
        Clear();
        for (int i = operations.Count - 1; i >= 0; i--)
        {
            GameObject go = Instantiate(itemPrb, contentTF);
            go.GetComponent<RecordItem>().Init(operations[i]);
        }
    }

    public void Clear()
    {
        int number = contentTF.childCount;
        for (int i = 0; i < number; i++)
        {
            DestroyImmediate(contentTF.GetChild(0).gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelInfoManager.My.listScript = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
