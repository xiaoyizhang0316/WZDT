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
        BaseMapRole startRole = PlayerData.My.GetMapRoleById(double.Parse(tradeData.startRole));
        BaseMapRole endRole = PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole));
        start = startRole.tradePoint;
        end = endRole.tradePoint;
        transform.position = (startRole.tradePoint.position + endRole.tradePoint.position) / 2f + new Vector3(0f, 0.3f, 0f);
        CheckPos();
        GetComponentInChildren<SpriteRenderer>().DOFade(0.2f, 0.2f).Play().timeScale = 1f / DOTween.timeScale;
    }

    /// <summary>
    /// 检测图标的位置有没有冲突
    /// </summary>
    public void CheckPos()
    {
        Physics.Raycast(transform.position + new Vector3(-7f, 10f, -7f), new Vector3(0.7f, -1f, 0.7f), out RaycastHit hit);
        if (hit.transform.CompareTag("CreateTradeButton") || hit.transform.CompareTag("MapRole"))
        {
            RePos();
        }
    }

    /// <summary>
    /// 重新排位置
    /// </summary>
    public void RePos()
    {
        float per = UnityEngine.Random.Range(0.2f, 0.8f);
        transform.position = start.position + (end.position - start.position) * per;
        CheckPos();
    }

    /// <summary>
    /// 鼠标点击弹出交易配置窗口
    /// </summary>
    public void OnMouseUp()
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

    /// <summary>
    /// 鼠标进入显现
    /// </summary>
    public void OnMouseEnter()
    {
        GetComponentInChildren<SpriteRenderer>().DOFade(1f, 0.8f).Play().timeScale = 1f / DOTween.timeScale;
    }

    /// <summary>
    /// 鼠标移除淡出效果
    /// </summary>
    public void OnMouseExit()
    {
        GetComponentInChildren<SpriteRenderer>().DOFade(0.2f, 0.8f).Play().timeScale = 1f / DOTween.timeScale;
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
