using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoleUISign : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public BaseMapRole mapRole;

    public Image icon;

    public Text cost;

    public Text effect;

    public Text efficiency;

    public List<Image> buff;

 
    // Start is called before the first frame update
    void Start()
    {
        UI_Camera = Camera.main;
        image = transform.GetComponent<RectTransform>();
        ui_Canvas = NewCanvasUI.My.gameObject.GetComponent<Canvas>();
        transform.localScale = new Vector3(0.8f,0.8f);
    }


    Camera UI_Camera; //UI相机
    RectTransform image; //UI元素
    public GameObject obj; //3D物体

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
                effect.text = mapRole.baseRoleData.effect.ToString();
                efficiency.text = mapRole.baseRoleData.efficiency.ToString();

                for (int i = 0; i < mapRole.GetEquipBuffList().Count; i++)
                {
                    if (TabManager.My.buffsprite.ContainsKey(mapRole.GetEquipBuffList()[i]))
                    {
                        buff[i].sprite =TabManager.My. buffsprite[mapRole.GetEquipBuffList()[i]];
                    }
                    else
                    {
                        TabManager.My. buffsprite.Add(mapRole.GetEquipBuffList()[i],
                                Resources.Load<Sprite>("Sprite/Buff/" + mapRole.GetEquipBuffList()[i]));
                        buff[i].sprite =TabManager.My.buffsprite[mapRole.GetEquipBuffList()[i]];
                    }
                }

                //icon.transform.DOLocalMoveY(106, 0.3f).SetEase(Ease.OutCubic);
                //cost.transform.DOLocalMoveY(-3, 0.3f).SetEase(Ease.OutCubic);
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        transform.DOScale(1.5f, 0.5f).Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(0.8f, 0.5f).Play();
        
    }
}