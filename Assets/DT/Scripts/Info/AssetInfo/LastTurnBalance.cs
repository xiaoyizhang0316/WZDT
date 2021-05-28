using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LastTurnBalance : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text asset_balance_text;
    
    public Color addColor=new Color32((byte)147,(byte)204,(byte)36,(byte)255);
    public Color lostColor=new Color32((byte)208,(byte)86,(byte)55, (byte)255);
    public Color zeroColor=new Color32((byte)255,(byte)255,(byte)255, (byte)255);

    public string tipString="";
    
    // Start is called before the first frame update
    
    void Start()
    {
        if (asset_balance_text == null)
            asset_balance_text = GetComponent<Text>();
        asset_balance_text.color = Color.white;
    }

    public void SetAsset(int num)
    {
        if (num > 0)
        {
            asset_balance_text.color = addColor;
            asset_balance_text.text = "+" + num;
        }
        else
        {
            if (num == 0)
            {
                asset_balance_text.color = zeroColor;
                asset_balance_text.text = num.ToString();
            }
            else
            {
                asset_balance_text.color = lostColor;
                asset_balance_text.text = num.ToString();
            }
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        FloatWindow.My.Init(tipString);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FloatWindow.My.Hide();
    }
}
