using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSign : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Text name;
    public Text Text_describe;
    public Image Image_shape;
    public Transform pop_1;
    public Transform pop_2;
    public RectTransform pinpai;
    public RectTransform zhiliang;
    public RectTransform channeng;
    public RectTransform xiaolv;
    public Text souxun;
    public Text yijia;
    public Text jiaofu;
    public Text fengxian;
    public Transform pop_1startPOS;
    public Transform pop_1EndPOS;
    public Transform pop_2startPOS;
    public Transform pop_2EndPOS;
    public Text Cost;

    public Image BG;
    /// <summary>
    /// 当创建销毁时候或者保存角色时候调整占用状态
    /// </summary>
    public bool   isOccupation;
    public int ID; 

    //public int isoccupy; 
    //public bool isEquiped;

    public GearData gearData;
 
    
    public  RectTransform   canvas;//得到canvas的ugui坐标
    private RectTransform imgRect;//得到图片的ugui坐标
    Vector2 offset = new Vector3();//用来得到鼠标和图片的差值


    private Vector2 startPos;


    private GameObject cam;
        private void Awake()
    {
        EquipListManager.My._signs.Add(this);
        imgRect = GetComponent<RectTransform>();//得到组件
        startPos = imgRect.position;
        cam = Camera.main.gameObject;//找到摄像机
         }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="id"></param>
    /// <param name="_isEquiped"></param>
    public void Init(int id, bool _isEquiped)
    {
        ID = id;
        SetOccupyStatus(_isEquiped);
        gearData = GameDataMgr.My.GetGearData(id);
        //name.text = gearData.name;
        
        //print(gearData.SpritePath);
        Image_shape.sprite = Resources.Load<Sprite>(gearData.SpritePath);
        pop_1.position = pop_1startPOS.position; 
        pop_2.position = pop_2startPOS.position; 
        pinpai.sizeDelta =new Vector2(gearData.brand/150f*133f,pinpai.sizeDelta.y);
        zhiliang.sizeDelta =new Vector2(gearData.quality/150f*133f,pinpai.sizeDelta.y);
        channeng.sizeDelta =new Vector2(gearData.capacity/150f*133f,pinpai.sizeDelta.y);
        xiaolv.sizeDelta =new Vector2(gearData.efficiency/150f*133f,pinpai.sizeDelta.y);
        souxun.text = gearData.searchAdd.ToString();
        yijia.text = gearData.bargainAdd.ToString();
        jiaofu.text = gearData.deliverAdd.ToString();
        fengxian.text = gearData.riskAdd.ToString();
        Cost.text = gearData.costMonth.ToString();
        
    }

    /// <summary>
    /// 改变装备的占用状态
    /// </summary>
    /// <param name="newStatus"></param>
    public void SetOccupyStatus(bool newStatus)
    {
        bool temp = isOccupation;
        isOccupation = newStatus;
        if (temp != isOccupation)
            CheckOccupyStatus();
    }

    /// <summary>
    /// 改变装备的图标显示
    /// </summary>
    public void CheckOccupyStatus()
    {
        if (isOccupation)
        {
            //GetComponent<Image>().raycastTarget = false;
            //Image_shape.GetComponent<Image>().raycastTarget = false;
            //GetComponent<Image>().DOFade(0.3f, 0.5f);
            BG.GetComponent<Image>().DOColor(new Color(0.6f,0.6f,0.6f,1), 0.5f);
            Image_shape.GetComponent<Image>().DOFade(0.3f, 0.5f);
            //GetComponent<LayoutElement>().layoutPriority = -10;
            transform.SetAsLastSibling();
        }
        else
        {
            //GetComponent<Image>().raycastTarget = true;
            //transform.Find("Image_SignBG").GetComponent<Image>().raycastTarget = true;
            //GetComponent<Image>().DOFade(1f, 0.5f);
            BG.GetComponent<Image>().DOColor(new Color(1f,1f,1f,1), 0.5f);
            Image_shape.GetComponent<Image>().DOFade(1 ,0.5f);
            //GetComponent<LayoutElement>().layoutPriority = 10;
            transform.SetAsFirstSibling();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas = UIManager.My.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private GameObject Equip;
    public void CreatEquipOBJ()
    {
        if (!isOccupation)
        {
            Equip = Instantiate(Resources.Load<GameObject>(GameDataMgr.My.GetGearData(ID).GearSpacePath), EquipListManager.My.equipPos);
            Vector3 V = Input.mousePosition;
            Vector3 V2 = new Vector3(V.x - Screen.width / 2, V.y - Screen.height / 2);
            Equip.transform.localPosition = V2;
            Equip.name = "EquipOBJ_" + ID;
            Equip.GetComponent<DragUI>().dragType = DragUI.DragType.equip;
        }
    } 
    
    public void OnDrag(PointerEventData eventData)
    { 
        if (Equip == null || isOccupation)
        { 
            return;
        }

     //  Vector3 V = Input.mousePosition;
     //  Vector3 V2 = new Vector3(V.x-Screen.width/2 ,V.y-Screen.height/2);
     //  Debug.Log("Screen.width"+Screen.width+"Screen.height"+Screen.height);
     // // Vector3 V2 = new Vector3(V.x-960,V.y-540);
     //  Equip.transform.localPosition = V2;
     Vector2 mouseDrage = Input.mousePosition;//当鼠标拖动时的屏幕坐标
     Vector2 uguiPos = new Vector2();//用来接收转换后的拖动坐标
     bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDrage, eventData.enterEventCamera, out uguiPos);
     if (isRect)
     {
         //设置图片的ugui坐标与鼠标的ugui坐标保持不变
         Equip.GetComponent<RectTransform>().anchoredPosition =  uguiPos;
     } 
        // Equip.transform.position =  Input.mousePosition;
    
    }

    
    public void OnEndDrag(PointerEventData eventData)
    {
        //isOccupation =    Equip.GetComponent<DragUI>().CheckAllRight(false);
        SetOccupyStatus(Equip.GetComponent<DragUI>().CheckAllRight(false));
       
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isOccupation)
        {
            CreatEquipOBJ();
            CreatRoleManager.My.CurrentTemplateManager.OpenMidTemplate(0.3f);
            
          //Vector2 mouseDown = eventData.position;//记录鼠标按下时的屏幕坐标
          //Vector2 mouseUguiPos = new Vector2();//定义一个接收返回的ugui坐标


          //bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDown, eventData.enterEventCamera, out mouseUguiPos);
          ////RectTransformUtility.ScreenPointToLocalPointInRectangle()：把屏幕坐标转化成ugui坐标
          ////canvas：坐标要转换到哪一个物体上，这里img父类是Canvas，我们就用Canvas
          ////eventData.enterEventCamera：这个事件是由哪个摄像机执行的
          ////out mouseUguiPos：返回转换后的ugui坐标
          ////isRect：方法返回一个bool值，判断鼠标按下的点是否在要转换的物体上
 

          //if (isRect)//如果在
          //{ //计算图片中心和鼠标点的差值


          //    offset = Equip.GetComponent<RectTransform>().anchoredPosition - mouseUguiPos;
          //}
//            cam.GetComponent<CameraMove>().enabled = false;


           
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pop_1.DOLocalMove(pop_1EndPOS.localPosition, 0.05f).SetEase(Ease.OutBack).SetUpdate(true);
        pop_2.DOLocalMove(pop_2EndPOS.localPosition, 0.05f).SetEase(Ease.OutBack).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
     var  t1= pop_1.DOLocalMove(Vector3.zero, 0.05f).SetUpdate(true).OnComplete(() =>
     {
         pop_1.transform.localPosition = Vector3.zero;  
     });
      var t2 =  pop_2.DOLocalMove(Vector3.zero, 0.05f).SetUpdate(true).OnComplete(() =>
      {
          pop_2.transform.localPosition =Vector3.zero;  
      });
        
    }
}
