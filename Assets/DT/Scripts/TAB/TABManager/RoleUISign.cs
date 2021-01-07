using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RoleUISign : MonoBehaviour
{

    public BaseMapRole mapRole;

    public Image icon;

    public Text cost;
    // Start is called before the first frame update
    void Start()
    {
        UI_Camera = Camera.main;
        image = transform.GetComponent<RectTransform>();
        ui_Canvas = NewCanvasUI.My.gameObject.GetComponent<Canvas>();
    }

    

      Camera UI_Camera; //UI相机
     RectTransform image; //UI元素
    public  GameObject obj; //3D物体

    Canvas ui_Canvas;

    // Update is called once per frame
    void Update()
    {
        if (obj == null)
        {
            return;
        }
        else
        {
            UpdateNamePosition();
            if (!mapRole.isNpc)
            {
                icon.gameObject.SetActive(true);
                cost.gameObject.SetActive(true);
                cost.text = mapRole.baseRoleData.cost.ToString();
                icon.transform.DOLocalMoveY(106, 0.3f).SetEase(Ease.OutCubic);
                cost.transform.DOLocalMoveY(-3, 0.3f).SetEase(Ease.OutCubic);
                
            }
            else
            {
                icon.gameObject.SetActive(false);
                cost.gameObject.SetActive(false);
            } 
        }

    }

    /// <summary>
    /// 更新image位置
    /// </summary>
    void UpdateNamePosition()
    {
        Vector2 mouseDown = Camera.main.WorldToScreenPoint(obj.transform.position);
        Vector2 mouseUGUIPos = new Vector2();
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(ui_Canvas.transform as RectTransform,
            mouseDown, UI_Camera, out mouseUGUIPos);
        if (isRect)
        {
            image.anchoredPosition = mouseUGUIPos;
        }
    }
}