using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RoleEffect3 : MonoBehaviour
{
    public GameObject rotateOBJ;

    public BaseSkill skill;
    // Start is called before the first frame update
    void Start()
    {
        skill = GetComponentInParent<BaseSkill>();
        Debug.Log(rotateOBJ.transform.position.y);
    }

    private bool tweFinish = true;

    public void Func()
    {

    }
    private float number = 1f;

    // Update is called once per frame
    void Update()
    {

        if (skill.role.tradeList.Count > 0 && skill.IsOpen)
        {
            if (rotateOBJ.transform.position.y >= 0.36f)
                number = -1f;
            if (rotateOBJ.transform.position.y <= 0.30f)
                number = 1f;
            rotateOBJ.transform.position += new Vector3(0f, 0.001f, 0f) * number;
        }
    }
}
