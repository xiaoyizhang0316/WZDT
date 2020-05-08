using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeSettingPopUp : MonoBehaviour
{
    public Text popUpTitle;

    public Text popUpText;

    public Vector3 offset;

    public void Init(string title,Vector3 pos)
    {
        popUpTitle.text = title;
        transform.localPosition = pos + new Vector3(-960f,-400f,0f);
        SetPopUpText();
        //popUpText.text = text;
    }

    public void SetPopUpText()
    {
        switch (popUpTitle.text)
        {
            case "固定":
                popUpText.text = "付钱方支付固定数值的金钱给收钱方";
                break;
            case "剩余":
                popUpText.text = "付钱方获得收入时，保留固定数值的金钱，然后将剩余数目全部给收钱方";
                break;
            case "分成":
                popUpText.text = "付钱方获得收入时，将该收入一定比例的金钱付给收钱方";
                break;
            case "先钱":
                popUpText.text = "付钱方在交易开始时付钱给收钱方";
                break;
            case "后钱":
                popUpText.text = "付钱方在交易结束时付钱给收钱方";
                break;
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
