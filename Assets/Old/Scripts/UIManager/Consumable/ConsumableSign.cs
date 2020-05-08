using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConsumableSign : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    /// <summary>
    /// 道具ID
    /// </summary>
    public int consumableId;

    /// <summary>
    /// 道具数量
    /// </summary>
    public int consumableNum;

    /// <summary>
    /// 数量对应UI
    /// </summary>
    public Text consumableNumber;

    /// <summary>
    /// 拖拽用gameobject
    /// </summary>
    public GameObject go;

    /// <summary>
    /// buff列表
    /// </summary>
    public List<BuffData> targetBuffList;

    /// <summary>
    /// 是否是拖拽临时生成
    /// </summary>
    public bool isCopy;

    private void Awake()
    {
        ConsumableListManager.My._signs.Add(this);
    }

    private void OnDestroy()
    {
        ConsumableListManager.My._signs.Remove(this);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="p"></param>
    public void Init(PlayerConsumable p)
    {
        consumableId = p.consumableId;
        consumableNum = p.number;
        string path = "Sprite/Consumable/" + consumableId.ToString();
        GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
        UpdateInfo();
    }

    /// <summary>
    /// 更新UI信息
    /// </summary>
    public void UpdateInfo()
    {
        consumableNum = PlayerData.My.GetPlayerConsumableById(consumableId).number;
        consumableNumber.text = consumableNum.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isCopy)
        {
            go = Instantiate(gameObject, transform.position, transform.rotation);
            go.GetComponentInChildren<Text>().gameObject.SetActive(false);
            go.GetComponent<ConsumableSign>().isCopy = true;
            go.transform.SetParent(ConsumableListManager.My.dragPos);
            Vector3 V = Input.mousePosition;
            Vector3 V2 = new Vector3(V.x, V.y - Screen.height / 2 + 20f);
            go.transform.localPosition = V2;
            go.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isCopy)
        {
            //float amount = 16 * Screen.height / 9f / Screen.width;
            //print(Screen.height.ToString() + "   " + Screen.width.ToString());
            //print(amount);
            //Vector3 temp = new Vector3(0f,Screen.height / 2,0f);
            //Vector3 V = Input.mousePosition;
            //print(Input.mousePosition - temp) ;
            //Vector3 V2 = new Vector3(V.x / (amount * 2f - 1f) , V.y - (Screen.height / (2 * amount - 1f)));
            //go.transform.localPosition = V2;
            //go.GetComponent<RectTransform>().anchoredPosition = V2;
            Vector3 pos = new Vector3();
            RectTransformUtility.ScreenPointToWorldPointInRectangle(go.GetComponent<RectTransform>(), eventData.position,
            Camera.main, out pos);
            //print(eventData.position);
            go.transform.position = pos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isCopy)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hit = Physics.RaycastAll(ray);
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].transform.tag.Equals("Role"))
                {
                    BaseMapRole role = hit[i].transform.GetComponentInParent<BaseMapRole>();
                    print("使用消耗品:" + consumableId.ToString());
                    InitBuff();
                    CastBuff(role);
                    PlayerData.My.UseConsumable(consumableId);
                    CheckDelete();
                    break;
                }
            }
            Destroy(go);
        }
    }

    /// <summary>
    /// 检测是否需要删除
    /// </summary>
    public void CheckDelete()
    {
        foreach (PlayerConsumable p in PlayerData.My.playerConsumables)
        {
            //print(p.consumableId);
            if (p.consumableId == consumableId)
            {
                UpdateInfo();
                return;
            }
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// 初始化消耗品附带的buff
    /// </summary>
    public void InitBuff()
    {
        targetBuffList = new List<BuffData>();
        ConsumableData data = GameDataMgr.My.GetConsumableDataByID(consumableId);
        foreach (int i in data.targetBuffList)
        {
            if (i != -1)
            {
                targetBuffList.Add(GameDataMgr.My.GetBuffDataByID(i));
            }
        }
    }

    /// <summary>
    /// 将buff释放给目标
    /// </summary>
    /// <param name="role"></param>
    public void CastBuff(BaseMapRole role)
    {
        for (int i = 0; i < targetBuffList.Count; i++)
        {
            BaseBuff buff = new BaseBuff();
            buff.Init(targetBuffList[i]);
            buff.SetRoleBuff(role, role, role);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isCopy)
        {
            ConsumableInfo.My.Init(consumableId,consumableNum, Input.mousePosition.y - (540f * Screen.height / 1080f));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isCopy)
        {
            ConsumableInfo.My.MenuHide();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


}
