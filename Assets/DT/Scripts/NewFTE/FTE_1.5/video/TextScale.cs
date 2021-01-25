using System;
using DG.Tweening;
using UnityEngine;

public class TextScale : MonoBehaviour
{
    private Vector3 origin_scale;
    private void Start()
    {
        origin_scale = transform.localScale;
    }

    private void OnMouseOver()
    {
        Debug.LogWarning("in");
        transform.DOScale(2, 0.2f).Play();
    }

    private void OnMouseExit()
    {
        Debug.LogWarning("out");

        transform.DOScale(origin_scale, 0.2f).Play();
    }
}
