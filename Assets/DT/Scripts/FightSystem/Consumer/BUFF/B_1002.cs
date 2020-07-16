using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_1002 : MonoBehaviour
{
    private ConsumeSign targetConsume;

    private bool isOpen;

    public GameObject effectPrb;

    //private void OnDestroy()
    //{
    //    ConsumeSign[] list = FindObjectsOfType<ConsumeSign>();
    //    for (int i = 0; i < list.Length; i++)
    //    {
    //        if (Vector3.Distance(transform.position, list[i].transform.position) <= 3)
    //        {
    //            list[i].ChangeHealth(-200);
    //        }
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        targetConsume = GetComponentInParent<ConsumeSign>();
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetConsume.currentHealth >= targetConsume.consumeData.maxHealth && !isOpen)
        {
            ConsumeSign[] list = FindObjectsOfType<ConsumeSign>();
            for (int i = 0; i < list.Length; i++)
            {
                if (Vector3.Distance(transform.position, list[i].transform.position) <= 3)
                {
                    list[i].ChangeHealth(-200);
                }
            }
            isOpen = true;
            GameObject go = Instantiate(effectPrb, targetConsume.transform);
            go.transform.localPosition = new Vector3(0f,0.1f,0f);
        }
    }
}
