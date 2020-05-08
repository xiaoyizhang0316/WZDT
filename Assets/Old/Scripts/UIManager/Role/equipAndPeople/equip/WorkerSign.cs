using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorkerSign : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Text name;
    public Text Text_describe;
    public Image Image_shape;
    public int ID;
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
    public Text cost;
    public Image BG;
    /// <summary>
    /// 当创建销毁时候或者保存角色时候调整占用状态
    /// </summary>
    public bool isOccupation;

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
        //S name.text = workerData.name;
        //print(workerData.SpritePath);
        Image_shape.sprite = Resources.Load<Sprite>(workerData.SpritePath);
        pop_1.position = pop_1startPOS.position;
        pop_2.position = pop_2startPOS.position;
        pinpai.sizeDelta = new Vector2(workerData.brand / 150f * 133f, pinpai.sizeDelta.y);
        zhiliang.sizeDelta = new Vector2(workerData.quality / 150f * 133f, pinpai.sizeDelta.y);
        channeng.sizeDelta = new Vector2(workerData.capacity / 150f * 133f, pinpai.sizeDelta.y);
        xiaolv.sizeDelta = new Vector2(workerData.efficiency / 150f * 133f, pinpai.sizeDelta.y);
        souxun.text = workerData.searchAdd.ToString();
        yijia.text = workerData.bargainAdd.ToString();
        jiaofu.text = workerData.deliverAdd.ToString();
        fengxian.text = workerData.riskAdd.ToString();
        cost.text = workerData.costMonth.ToString();
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
            BG.DOColor (new Color(0.6f,0.6f,0.6f),  0.5f);
            Image_shape.GetComponent<Image>().DOFade(0.3f, 0.5f);
            //GetComponent<LayoutElement>().layoutPriority = -10;
            transform.SetAsLastSibling();
        }
        else
        {
            //GetComponent<Image>().raycastTarget = true;
            //Image_shape.GetComponent<Image>().raycastTarget = true;
           // GetComponent<Image>().DOFade(1f, 0.5f);
            Image_shape.GetComponent<Image>().DOFade(1f, 0.5f);
            BG.GetComponent<Image>().DOColor(new Color(1f,1f,1f,1), 0.5f);

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
        worker.name = "workerOBJ_" + ID;
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
        if (worker == null || isOccupation)
        {
            return;
        }

      //  Vector3 V = Input.mousePosition;
      //  Vector3 V2 = new Vector3(V.x - Screen.width / 2, V.y - Screen.height / 2);
      //  worker.transform.localPosition = V2
      Vector2 mouseDrage = Input.mousePosition;//当鼠标拖动时的屏幕坐标
      Vector2 uguiPos = new Vector2();//用来接收转换后的拖动坐标
      bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(UIManager.My.GetComponent<RectTransform>(), mouseDrage, eventData.enterEventCamera, out uguiPos);
      if (isRect)
      {
          //设置图片的ugui坐标与鼠标的ugui坐标保持不变
          worker.GetComponent<RectTransform>().anchoredPosition =  uguiPos;
      }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (worker == null || isOccupation)
        {
            return;
        }
        SetOccupyStatus(worker.GetComponent<DragUI>().CheckAllRight(false));
        //isOccupation = worker.GetComponent<DragUI>().CheckAllRight(false);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isOccupation)
        {
            CreatWorkerOBJ();
            CreatRoleManager.My.CurrentTemplateManager.OpenTopTemplate(0.3f);
        }


    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pop_1.DOLocalMove(pop_1EndPOS.localPosition, 0.1f).SetEase(Ease.OutBack).SetUpdate(true);
        pop_2.DOLocalMove(pop_2EndPOS.localPosition, 0.1f).SetEase(Ease.OutBack).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pop_1.DOLocalMove(pop_1startPOS.localPosition, 0.1f).SetUpdate(true);
        pop_2.DOLocalMove(pop_2startPOS.localPosition, 0.1f).SetUpdate(true);
    }
}