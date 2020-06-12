using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static GameEnum;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TradeIcon : MonoBehaviour
{
    public Transform start;

    public Transform end;

    public int tradeId;

    public void SetTrasform(Transform s, Transform e)
    {
        start = s;
        end = e;

    }

    /// <summary>
    /// 为指定的交易结构生成交易图标
    /// </summary>
    /// <param name="szfs"></param>
    /// <param name="cashflow"></param>
    /// <param name="isfree"></param>
    /// <param name="skilltype"></param>
    public void Init(TradeData tradeData)
    {
        tradeId = tradeData.ID;
        BaseMapRole start = PlayerData.My.GetMapRoleById(double.Parse(tradeData.startRole));
        BaseMapRole end = PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole));
        transform.position = (start.tradePoint.position + end.tradePoint.position) / 2f + new Vector3(0f,0.3f,0f);
        GetComponentInChildren<SpriteRenderer>().DOFade(0.2f, 0.2f);
    }

    public void OnMouseDown()
    {
        //UIManager.My.Panel_CreateTrade.SetActive(true);
        if (!NewCanvasUI.My.NeedRayCastPanel() && !EventSystem.current.IsPointerOverGameObject())
        {
            if (int.Parse(SceneManager.GetActiveScene().name.Split('_')[1]) > 3)
            {
                NewCanvasUI.My.Panel_TradeSetting.SetActive(true);
                CreateTradeManager.My.Open(TradeManager.My.tradeList[tradeId].gameObject);
            }
            else
            {
                NewCanvasUI.My.Panel_Delete.SetActive(true);
                string str = "确定要删除此交易吗？";
                DeleteUIManager.My.Init(str, () => { TradeManager.My.DeleteTrade(tradeId); });
            }
        }
    }

    public void OnMouseEnter()
    {
        GetComponentInChildren<SpriteRenderer>().DOFade(1f, 0.8f);
    }

    public void OnMouseExit()
    {
        GetComponentInChildren<SpriteRenderer>().DOFade(0.2f,0.8f);
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
