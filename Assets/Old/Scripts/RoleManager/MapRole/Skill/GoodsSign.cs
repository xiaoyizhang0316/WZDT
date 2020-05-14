using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GoodsSign : MonoBehaviour
{

    public ProductData productData;

    public List<Vector3> path;

    public BaseMapRole role;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int count = 0;
    public void Move()
    {
       
        transform.DOMove(path[count],1).OnComplete(() =>
        {
            count++;
            if (count < path.Count)
            {
                Move();
                
            }
            else
            {
                role.AddPruductToWareHouse (productData);
                Destroy(this.gameObject,0.01f);
            }

        }).SetEase(Ease.Linear);
    }

}
