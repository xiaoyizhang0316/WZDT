using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AutoFireTow : MonoBehaviour
{
    private ConsumeSign Asign;

    public ProductData data;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GetminDisConsumer",0,1);
        GetComponent<BulletLaunch>().fire .SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public bool GetminDisConsumer()
    {
        List<ConsumeSign> signs = FindObjectsOfType<ConsumeSign>().ToList();
        bool find = false;
        Asign = null;
        float dis = 2;
        for (int i = 0; i < signs.Count; i++)
        {
            if (  signs[i].isCanSelect)
            {
                if (Vector3.Distance( transform.position, signs[i].transform.position) < dis)
                {
                    dis = Vector3.Distance( transform.position, signs[i].transform.position);
                    Asign = signs[i];
                    find =  true;
                }
            }
        }
 
        return find;
    }

    public void Shoot()
    {
        if (Asign != null)
        {
            GetComponent<BulletLaunch>().fire .SetActive(true);
            GetComponent<BulletLaunch>().LanchNormal(data);
        }
        
    }
}
