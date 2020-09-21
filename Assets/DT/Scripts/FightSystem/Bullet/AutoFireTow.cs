using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class AutoFireTow : MonoBehaviour
{
    public  ConsumeSign Asign;

    public Transform launchShooter;
    public ProductData data;

    public BulletLaunch lunch;
    public float shootTime;

    public float destroyTime;

    public ConsumeSign target;

    public GameObject boom;
    public bool isupdate = false;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GetminDisConsumer",0,1);
       // InvokeRepeating("Shoot",1,shootTime);
//        GetComponent<BulletLaunch>().fire .SetActive(false);
        DestotyOBJ();
    }

    // Update is called once per frame
    private Vector3 pos = new Vector3();
    void Update()
    {
        if (!isupdate)
        {
            return;
        }

        if (target != null)
        {
            //Debug.Log("buweikong");
            transform.position =target.transform.position;
            pos = target.transform.position;
        }

        else
        {
            transform.position =pos;

        }
    } 
    
    public bool GetminDisConsumer()
    {
        List<ConsumeSign> signs = FindObjectsOfType<ConsumeSign>().ToList();
        bool find = false;
        pos = transform.position;
        Asign = null;
        float dis = 2;
        for (int i = 0; i < signs.Count; i++)
        {
            if (  signs[i].isCanSelect)
            {
                if (Vector3.Distance( transform.position, signs[i].transform.position) < dis&&Vector3.Distance( transform.position, signs[i].transform.position)>0.1f)
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
        transform.DOScale(transform.localScale, shootTime).OnComplete(() =>
        {
            if (Asign != null)
            {
                //GetComponent<BulletLaunch>().fire .SetActive(true);
            
                GetComponent<BulletLaunch>().LanchLeaser(data, Asign, launchShooter,lunch,transform);
            }
            else
            {
                //  GetComponent<BulletLaunch>().fire .SetActive(false);
            }

            Shoot();
        });
    
    }

    public void DestotyOBJ()
    {
        transform.DOScale(0.5f, destroyTime-1).OnComplete(() =>
        {
            
            GameObject gameObject =  Instantiate(boom,transform);
            gameObject.transform.localPosition = Vector3.zero;
            CancelInvoke("Shoot");
            GetComponent<BoomTrigger>().GetConsumerListTow();
        });
        transform.DOScale(0.5f, destroyTime).OnComplete(() =>
        { 
            Destroy(gameObject);
        });
    }
}
