using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorkerSign : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
{
    public Image Image_shape;
   
    /// <summary>
    /// 效果值
    /// </summary>
    public  Text effect;
    
    /// <summary>
    /// 效率值
    /// </summary>
    public  Text efficiency;
    
    /// <summary>
    /// 范围值
    /// </summary>
    public  Text range;
    
    /// <summary>
    /// 风险抗力
    /// </summary>
    public  Text riskResistance;
    
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
    public Text techAdd;
    public Image shapeImageBG;
    public Sprite lv;
    public Sprite lan;
    public Sprite hui;
    /// <summary>
    /// 当创建销毁时候或者保存角色时候调整占用状态
    /// </summary>
    public bool isOccupation;
    public int ID; 

    public WorkerData workerData;

    private void Awake()
    {
        WorkerListManager.My._signs.Add(this);
    }

    /// <summary>
    /// UI单个图标初始化
    /// </summary>
    /// <param name="id"></param>
    public void Init(int id, bool Occupation)
    {
        ID = id;
        SetOccupyStatus(Occupation);
        workerData = GameDataMgr.My.GetWorkerData(id);
        effect.text = workerData.effect.ToString();
        efficiency.text = workerData.efficiency.ToString();
        range.text = workerData.range.ToString();
        riskResistance.text = workerData.riskResistance.ToString();
        tradeCost.text = workerData.tradeCost.ToString();
        cost.text = workerData.cost.ToString();
        bulletCapacity.text = workerData.bulletCapacity.ToString(); 
        //S name.text = workerData.name;
        //print(workerData.SpritePath);
        if (workerData.ProductOrder == 1)
        {
            shapeImageBG.sprite = hui;
        }
        if (workerData.ProductOrder == 2)
        {
            shapeImageBG.sprite = lv;
        }
        if (workerData.ProductOrder == 3)
        {
            shapeImageBG.sprite = lan ;
        }
        Image_shape.sprite = Resources.Load<Sprite>(workerData.SpritePath);

        techAdd.text = workerData.techAdd.ToString();
    }

    /// <summary>
    /// 改变工人的占用状态
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
    /// 改变工人的图标显示
    /// </summary>
    public void CheckOccupyStatus()
    {
        if (isOccupation)
        {
            //GetComponent<Image>().raycastTarget = false;
            //Image_shape.GetComponent<Image>().raycastTarget = false;
            BG.DOColor (new Color(0.6f,0.6f,0.6f),  0.5f).Play();
            Image_shape.GetComponent<Image>().DOFade(0.3f, 0.5f).Play();
            //GetComponent<LayoutElement>().layoutPriority = -10;
            transform.SetAsLastSibling();
        }
        else
        {
            //GetComponent<Image>().raycastTarget = true;
            //Image_shape.GetComponent<Image>().raycastTarget = true;
           // GetComponent<Image>().DOFade(1f, 0.5f);
            Image_shape.GetComponent<Image>().DOFade(1f, 0.5f).Play();
            BG.GetComponent<Image>().DOColor(new Color(1f,1f,1f,1), 0.5f).Play();

            //GetComponent<LayoutElement>().layoutPriority = 10;
            transform.SetAsFirstSibling();
        }
    }

    // Start is called before the first frame update
    private GameObject worker;

    public void CreatWorkerOBJ()
    {
        worker = Instantiate(Resources.Load<GameObject>(GameDataMgr.My.GetWorkerData(ID).WorkerSpacePath),
            WorkerListManager.My.workerPos);
        Vector3 V = Input.mousePosition;
        Vector3 V2 = new Vector3(V.x - Screen.width / 2, V.y - Screen.height / 2);
        worker.transform.localPosition = V2;
        worker.name = "WorkerOBJ_" + ID;
        worker.GetComponent<DragUI>().dragType = DragUI.DragType.people;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isOccupation)
        {
            CreatWorkerOBJ();
            CreatRoleManager.My.CurrentTemplateManager.OpenTopTemplate(0.3f);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        time += Time.deltaTime;
        if (worker == null || isOccupation)
        {
            return;
        }

      //  Vector3 V = Input.mousePosition;
      //  Vector3 V2 = new Vector3(V.x - Screen.width / 2, V.y - Screen.height / 2);
      //  worker.transform.localPosition = V2
      Vector2 mouseDrage = Input.mousePosition;//当鼠标拖动时的屏幕坐标
      Vector2 uguiPos = new Vector2();//用来接收转换后的拖动坐标
      bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(      CreatRoleManager.My.currentCanvas.GetComponent<RectTransform>() , mouseDrage, eventData.enterEventCamera, out uguiPos);
      if (isRect)
      {
          //设置图片的ugui坐标与鼠标的ugui坐标保持不变
          worker.GetComponent<RectTransform>().anchoredPosition =  uguiPos;
      }

    }
    private float time = 0; 
    public void OnEndDrag(PointerEventData eventData)
    {    if (time < 0.3f)
        {
            Destroy(worker);
        }
        else
        {
            if (worker == null || isOccupation)
            {
                return;
            }

            SetOccupyStatus(worker.GetComponent<DragUI>().CheckAllRight(false));
            AudioManager.My.PlaySelectType(GameEnum.AudioClipType.PutEquip);
        }

        //isOccupation = worker.GetComponent<DragUI>().CheckAllRight(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        time = 0;
        if (!isOccupation)
        {
            CreatWorkerOBJ();
            AudioManager.My.PlaySelectType(GameEnum.AudioClipType.GrabEquip);
            CreatRoleManager.My.CurrentTemplateManager.OpenTopTemplate(0.3f);
        }
    }
}