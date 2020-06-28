using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NpcBulletSign : MonoBehaviour, IPointerDownHandler
{
    public ProductData currentProduct;

    public bool canSell;

    public void OnPointerDown(PointerEventData eventData)
    {
        NPCListInfo.My.npcBulletDetail.gameObject.SetActive(true);
        NPCListInfo.My.npcBulletDetail.InitUI(currentProduct, GetComponent<Image>().sprite, currentProduct.damage, currentProduct.loadingSpeed);
    }
}
