using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerSpawnRange : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if(other.tag.Equals("Building") && GetComponentInParent<RolePosSign>().isRelease)
        {
            Building targetBuilding = other.GetComponent<Building>();
            GetComponentInParent<BaseMapRole>().AddBuilding(targetBuilding.buildingId, targetBuilding);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
