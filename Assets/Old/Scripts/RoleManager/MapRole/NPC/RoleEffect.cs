using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleEffect : MonoBehaviour
{
    public GameObject rotateOBJ;

    public BaseSkill skill;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (skill.role.tradeList.Count > 0 &&skill. IsOpen)
        { 
            rotateOBJ.transform.Rotate(Vector3.up,Space.World);
        }
    }
}
