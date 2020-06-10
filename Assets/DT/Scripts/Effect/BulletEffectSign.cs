using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffectSign : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(List<int>buffIDs)
    {
        if (!buffIDs.Contains(int.Parse(name)))
        {
            if (name.Equals("000"))
            {
                return;
            }

            gameObject.SetActive(false);
        }
    }
}
