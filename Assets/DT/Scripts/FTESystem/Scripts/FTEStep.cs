using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTEStep : MonoBehaviour
{
    public RectTransform target;
    public float scale=1;
    public float scaleTime = 0.5f;
    public float moveTime = 0.5f;
    public GuideType guideType = GuideType.Rect;
    public TranslateType translateType = TranslateType.Direct;
    public RectTransform targetPos;

    private FTEController controller;

    public void ExcuteStep(FTEController guideController, Canvas canvas)
    {
        gameObject.SetActive(true);
        controller = guideController;
        guideController.Guide(canvas, target, guideType, scale, scaleTime, translateType, moveTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPos != null)
        {
            targetPos.localPosition = controller.Center;
        }
    }
}
