using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BuffRangeItem : MonoBehaviour
{
    public Transform dashLine;

    public Transform duan;

    public Transform solidLine;

    float speed = 5f;

    private void Update()
    {
        dashLine.Rotate(Vector3.back * speed *Time.deltaTime);
        duan.Rotate(Vector3.back * speed*Time.deltaTime);
        solidLine.Rotate(Vector3.forward * speed*Time.deltaTime);
    }
}
