using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankWorker : MonoBehaviour
{
    public List<int> indexList = new List<int>();

    public void Rank()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.childCount - i; j++)
            {
                if (transform.GetChild(i).position.y > transform.GetChild(j).position.y)
                {
                    transform.GetChild(i).SetSiblingIndex(j);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Rank();
    }
}
