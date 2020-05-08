using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class DetalBarInfo : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Text showText;

    public Image BG;
    public string seedString;
    public string peasantString;
    public string dealerString;
    public string merchantString;
    // Start is called before the first frame update
    void Start()
    {
        showText.gameObject.SetActive(false);
        if(BG!=null)
        BG.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        showText.gameObject.SetActive(true);
        if(BG!=null)
        BG.gameObject.SetActive(true);

        if (UIManager.My.currentMapRole.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
        {
            showText.text = seedString;
            
        }
        if (UIManager.My.currentMapRole.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Peasant)
        {
            showText.text = peasantString;
            
        }
        if (UIManager.My.currentMapRole.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Dealer)
        {
            showText.text =dealerString;
            
        }
        if (UIManager.My.currentMapRole.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Merchant)
        {
            showText.text = merchantString;
            
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        showText.gameObject.SetActive(false);
        if(BG!=null)
        BG.gameObject.SetActive(false);

    }
}
