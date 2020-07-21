using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RoleEffect2 : MonoBehaviour
{
    public GameObject rotateOBJ;

    public BaseSkill skill;
    // Start is called before the first frame update
    void Start()
    {
        skill = GetComponentInParent<BaseSkill>();
    }

    private bool tweFinish = true;

    public void Func()
    {
        //tweFinish = false;
        //rotateOBJ.transform.DORotate(new Vector3(-60f,0f,0f),0.5f,RotateMode.FastBeyond360).OnComplete(()=> {
        //    rotateOBJ.transform.DORotate(new Vector3(-90f, 0f, 0f), 0.5f, RotateMode.FastBeyond360).OnComplete(()=> {
        //        rotateOBJ.transform.DORotate(new Vector3(-120f, 0f, 0f), 0.5f, RotateMode.FastBeyond360).OnComplete(() => {
        //            rotateOBJ.transform.DORotate(new Vector3(-90f, 0f, 0f), 0.5f, RotateMode.FastBeyond360).OnComplete(() => {
        //                tweFinish = true;
        //            });
        //        });
        //    });
        //});
    }
    private int index = 1;

    private int count = 0;

    // Update is called once per frame
    void Update()
    {

        if (skill.role.tradeList.Count > 0 && skill.IsOpen)
        {
            rotateOBJ.transform.Rotate(Vector3.left * index / 2, Space.World);
            count++;
            if (count == 120)
            {
                index = 0 - index;
                count = 0;
            }
        }
    }
}
