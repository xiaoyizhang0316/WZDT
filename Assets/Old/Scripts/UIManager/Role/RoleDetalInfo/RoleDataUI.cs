using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static CreateTradeManager;
using static JYFS;

public class RoleDataUI : MonoBehaviour
{
    /// <summary>
    /// 产能条
    /// </summary>
    public Image capacity;

    /// <summary>
    /// 效率条
    /// </summary>
    public Image efficiency;

    /// <summary>
    /// 觉得品牌
    /// </summary>
    public Image brand;

    /// <summary>
    /// 质量
    /// </summary>
    public Image quality;
    /// <summary>
    /// 角色风险
    /// </summary>
    public Text risk;


    /// <summary>
    /// 角色搜寻
    /// </summary>
    public Text search;

    public Text bargain;

    /// <summary>
    /// 角色交付
    /// </summary>
    public Text delivery;

    /// <summary>
    /// 产能条
    /// </summary>
    public Image capacityAdd;

    /// <summary>
    /// 效率条
    /// </summary>
    public Image efficiencyAdd;

    /// <summary>
    /// 觉得品牌
    /// </summary>
    public Image brandAdd;

    /// <summary>
    /// 质量
    /// </summary>
    public Image qualityAdd;


    public void Init(TradeRoleAttribute original,TradeRoleAttributeChange change)
    {
        InitOriginal(original);
        InitChange(change);
        InitTC(original, change);
    }

    public void InitOriginal(TradeRoleAttribute original)
    {
        capacity.GetComponent<RectTransform>().sizeDelta = new Vector2(original.capacity / 150f * 301f, capacity.GetComponent<RectTransform>().sizeDelta.y);
        brand.GetComponent<RectTransform>().sizeDelta = new Vector2(original.brand / 150f * 301f, capacity.GetComponent<RectTransform>().sizeDelta.y);
        quality.GetComponent<RectTransform>().sizeDelta = new Vector2(original.quality / 150f * 301f, capacity.GetComponent<RectTransform>().sizeDelta.y);
        efficiency.GetComponent<RectTransform>().sizeDelta = new Vector2(original.effeciency / 150f * 301f, capacity.GetComponent<RectTransform>().sizeDelta.y);
        capacityAdd.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, capacity.GetComponent<RectTransform>().sizeDelta.y);
        qualityAdd.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, capacity.GetComponent<RectTransform>().sizeDelta.y);
        brandAdd.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, capacity.GetComponent<RectTransform>().sizeDelta.y);
        efficiencyAdd.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, capacity.GetComponent<RectTransform>().sizeDelta.y);
        capacityAdd.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0f); 
        qualityAdd.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0f);
        brandAdd.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0f);
        efficiencyAdd.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0f);
    }

    public void InitChange(TradeRoleAttributeChange change)
    {
        capacityAdd.GetComponent<RectTransform>().sizeDelta = new Vector2(change.capacityAdd / 150f * 301f, capacity.GetComponent<RectTransform>().sizeDelta.y);
        qualityAdd.GetComponent<RectTransform>().sizeDelta = new Vector2(change.qualityAdd / 150f * 301f, capacity.GetComponent<RectTransform>().sizeDelta.y);
        brandAdd.GetComponent<RectTransform>().sizeDelta = new Vector2(change.brandAdd / 150f * 301f, capacity.GetComponent<RectTransform>().sizeDelta.y);
        efficiencyAdd.GetComponent<RectTransform>().sizeDelta = new Vector2(change.effeciencyAdd / 150f * 301f, capacity.GetComponent<RectTransform>().sizeDelta.y);
        if (change.capacityAdd > 0)
            capacityAdd.transform.DOLocalRotate(new Vector3(0f,-180f,0f), 0f);
        if (change.qualityAdd > 0)
            qualityAdd.transform.DOLocalRotate(new Vector3(0f, -180f, 0f), 0f);
        if (change.brandAdd > 0)
            brandAdd.transform.DOLocalRotate(new Vector3(0f, -180f, 0f), 0f);
        if (change.effeciencyAdd > 0)
            efficiencyAdd.transform.DOLocalRotate(new Vector3(0f, -180f, 0f), 0f);
    }

    public void InitTC(TradeRoleAttribute original, TradeRoleAttributeChange change)
    {
        risk.text = original.risk.ToString() + GetTCNumberString(change.riskAdd);
        search.text = original.search.ToString() + GetTCNumberString(change.searchAdd);
        bargain.text = original.bargain.ToString() + GetTCNumberString(change.bargainAdd);
        delivery.text = original.delivery.ToString() + GetTCNumberString(change.deliveryAdd);
        
    }

    public string GetTCNumberString(int number)
    {
        if (number == 0)
            return "";

        else if (number > 0)
            return "+" + number.ToString();
        else
            return number.ToString();
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
