using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GameEnum;
using Image = UnityEngine.UI.Image;

public class ProductSign : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{

    public ProductData currentProduct;

    public bool canSell;


    
    public Text conut;

    public Image Image;
    /// <summary>
    /// GameObject销毁时
    /// </summary>
    private void OnDestroy()
    {
     
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
     
    }

    public void OnPointerUp(PointerEventData eventData)
    {
       // ProductDetalUI.My.panel.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
      ProductDetalUI.My.panel.SetActive(true);
      ProductDetalUI.My.InitUI( currentProduct,  transform.GetComponent<Image>().sprite ,currentProduct.damage,currentProduct.loadingSpeed);
     //  ProductDetalUI.My.panel.transform.position = gameObject.transform.position;
    }
}
