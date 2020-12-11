using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EquipSign : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerEnterHandler
{

    public Image Image_shape;

    public Image Image_buff;

    /// <summary>
    /// 效果值
    /// </summary>
    public Text effect;

    /// <summary>
    /// 效率值
    /// </summary>
    public Text efficiency;

    /// <summary>
    /// 范围值
    /// </summary>
    public Text range;

    /// <summary>
    /// 风险抗力
    /// </summary>
    public Text riskResistance;

    /// <summary>
    /// 交易成本
    /// </summary>
    public Text tradeCost;

    /// <summary>
    /// 成本
    /// </summary>
    public Text cost;

    /// <summary>
    /// 弹药容量
    /// </summary>
    public Text bulletCapacity;

    public Image BG;
    public Image shapeImageBG;
    /// <summary>
    /// 当创建销毁时候或者保存角色时候调整占用状态
    /// </summary>
    public bool isOccupation;
    public int ID;


    public Sprite lv;
    public Sprite lan;
    public Sprite hui;
    //public int isoccupy; 
    //public bool isEquiped;

    public GearData gearData;

    public RectTransform canvas;//得到canvas的ugui坐标
    private RectTransform imgRect;//得到图片的ugui坐标
    Vector2 offset = new Vector3();//用来得到鼠标和图片的差值

    private Vector2 startPos;

    private GameObject cam;


    public GameObject LevelUI;
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
        if (int.Parse(SceneManager.GetActiveScene().name.Split('_')[1])  >3)
        {
            LevelUI.SetActive(true);
        }
        else
        {
            LevelUI.SetActive(false);
            
        }

        ID = id;
        SetOccupyStatus(_isEquiped);
        gearData = GameDataMgr.My.GetGearData(id);
        //name.text = gearData.name;
        effect.text = gearData.effect.ToString();
        efficiency.text = gearData.efficiency.ToString();
        range.text = gearData.range.ToString();
        riskResistance.text = gearData.riskResistance.ToString();
        tradeCost.text = gearData.tradeCost.ToString();
        cost.text = gearData.cost.ToString();
        //bulletCapacity.text = gearData.bulletCapacity.ToString(); 
        //print(gearData.SpritePath);
        if (gearData.ProductOrder == 1)
        {
            shapeImageBG.sprite = hui;
        }
        if (gearData.ProductOrder == 2)
        {
            shapeImageBG.sprite = lv;
        }
        if (gearData.ProductOrder == 3)
        {
            shapeImageBG.sprite = lan ;
        }
        Image_shape.sprite = Resources.Load<Sprite>(gearData.SpritePath);
        if (gearData.buffList[0] != -1)
        {
            //Image_buff.sprite = Resources.Load<Sprite>("Sprite/Buff/" + gearData.buffList[0].ToString());
            GetComponentInChildren<WaveBuffSign>().Init(gearData.buffList[0]);
        }
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
            BG.GetComponent<Image>().DOColor(new Color(0.6f, 0.6f, 0.6f, 1), 0.5f).Play();
            Image_shape.GetComponent<Image>().DOFade(0.3f, 0.5f).Play();
            //GetComponent<LayoutElement>().layoutPriority = -10;
            transform.SetAsLastSibling();
        }
        else
        {
            //GetComponent<Image>().raycastTarget = true;
            //transform.Find("Image_SignBG").GetComponent<Image>().raycastTarget = true;
            //GetComponent<Image>().DOFade(1f, 0.5f);
            BG.GetComponent<Image>().DOColor(new Color(1f, 1f, 1f, 1), 0.5f).Play();
            Image_shape.GetComponent<Image>().DOFade(1, 0.5f).Play();
            //GetComponent<LayoutElement>().layoutPriority = 10;
            transform.SetAsFirstSibling();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas = CreatRoleManager.My.currentCanvas.GetComponent<RectTransform>();
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
        if (Input.GetMouseButton(1))
            return;
        time += Time.deltaTime;
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
            Equip.GetComponent<RectTransform>().anchoredPosition = uguiPos;
        }
        // Equip.transform.position =  Input.mousePosition;

    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (time < 0.3f)
        {
            Destroy(Equip);
        }
        else
        {
            SetOccupyStatus(Equip.GetComponent<DragUI>().CheckAllRight(false));
            AudioManager.My.PlaySelectType(GameEnum.AudioClipType.PutEquip);
        }

        //isOccupation =    Equip.GetComponent<DragUI>().CheckAllRight(false);
     
    }

    private float time = 0; 
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(1))
            return;
        time = 0;
       
        if (!isOccupation)
        {
            CreatEquipOBJ();
            CreatRoleManager.My.CurrentTemplateManager.OpenMidTemplate(0.3f);
            AudioManager.My.PlaySelectType(GameEnum.AudioClipType.GrabEquip);
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
        
    }
}
