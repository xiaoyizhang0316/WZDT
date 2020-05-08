using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 
using static GameEnum;
using Image = UnityEngine.UI.Image;

public class ProductSign : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{

    public ProductData currentProduct;

    public bool canSell;



    /// <summary>
    /// GameObject销毁时
    /// </summary>
    private void OnDestroy()
    {
        CancelInvoke();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
     
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ProductDetalUI.My.panel.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ProductDetalUI.My.panel.SetActive(true);
        ProductDetalUI.My.InitUI( transform.GetChild(0).GetComponent<Image>().sprite ,currentProduct.Sweetness,currentProduct.Crisp,currentProduct.Quality.ToString(),currentProduct.Brand.ToString(),currentProduct.time-(Time.time-currentProduct.birthday));
        ProductDetalUI.My.panel.transform.position = gameObject.transform.position;
    }
}
