using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static GameEnum;

public class ConsumerItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Image consumerClass;

    public Text consumerName;

    public Text buyPower;

    public Text buyRange;

    public Image preference;

    public Image lastSatisfy;

    public ConsumeSign currentConsumer;

    public List<Sprite> consumerStatusList;

    public List<Sprite> consumerSatisfyList;

    public List<Sprite> consumerPreferenceList;

    public Image isSelect;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="consume"></param>
    public void Init(GameObject consume)
    {
        currentConsumer = consume.transform.GetComponent<ConsumeSign>();
        InitInfo();
        SetClass();
        SetBuyPower();
        SetSprite(consume);
    }

    /// <summary>
    /// text相关设置
    /// </summary>
    public void InitInfo()
    {
        consumerName.text = currentConsumer.consumeData.consumerName;
        buyRange.text = currentConsumer.consumeData.buyRange.ToString() + "'";
    }

    /// <summary>
    /// 图片相关设置
    /// </summary>
    /// <param name="go"></param>
    public void SetSprite(GameObject go)
    {
        if (go.activeInHierarchy)
        {
            GetComponent<Image>().sprite = consumerStatusList[1];
            Text[] temp = GetComponentsInChildren<Text>();
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i].color = Color.black;
            }
        }
        else
        {
            GetComponent<Image>().sprite = consumerStatusList[0];
            Text[] temp = GetComponentsInChildren<Text>();
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i].color = Color.white;
            }
        }
        if (currentConsumer.totalSatisfy < -100)
        {
            lastSatisfy.sprite = consumerSatisfyList[4];
        }
        else if (currentConsumer.totalSatisfy < -50)
        {
            lastSatisfy.sprite = consumerSatisfyList[3];
        }
        else if (currentConsumer.totalSatisfy < 50)
        {
            lastSatisfy.sprite = consumerSatisfyList[2];
        }
        else if (currentConsumer.totalSatisfy < 100)
        {
            lastSatisfy.sprite = consumerSatisfyList[1];
        }
        else
        {
            lastSatisfy.sprite = consumerSatisfyList[0];
        }
        if (currentConsumer.consumeData.preference == 0)
        {
            preference.sprite = consumerPreferenceList[0];
        }
        else
        {
            preference.sprite = consumerPreferenceList[1];
        }
    }

    /// <summary>
    /// 购买力相关设置
    /// </summary>
    public void SetBuyPower()
    {
        if (currentConsumer.consumeData.buyPower < 0.5f)
        {
            buyPower.text = "低";
        }
        else if (currentConsumer.consumeData.buyPower < 1.1f)
        {
            buyPower.text = "中";
        }
        else
        {
            buyPower.text = "高";
        }
    }

    /// <summary>
    /// 阶级设置
    /// </summary>
    public void SetClass()
    {
        switch (currentConsumer.consumerType)
        {
            case ConsumerType.Oldpao_1:
            case ConsumerType.Oldpao_2:
            case ConsumerType.Oldpao_3:
                consumerClass.color = new Color();
                break;
            case ConsumerType.Bluecollar_1:
            case ConsumerType.Bluecollar_2:
            case ConsumerType.Bluecollar_3:
                consumerClass.color = Color.blue;
                break;
            case ConsumerType.Whitecollar_1:
            case ConsumerType.Whitecollar_2:
            case ConsumerType.Whitecollar_3:
                consumerClass.color = Color.white;
                break;
            case ConsumerType.Goldencollar_1:
            case ConsumerType.Goldencollar_2:
            case ConsumerType.Goldencollar_3:
                consumerClass.color = Color.yellow;
                break;
            case ConsumerType.Elite_1:
            case ConsumerType.Elite_2:
            case ConsumerType.Elite_3:
                consumerClass.color = Color.red;
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        float offset = Screen.height / 1080f;
        ConsumerPopUp.My.Init(currentConsumer, Input.mousePosition.y - (405f * offset));
        isSelect.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ConsumerPopUp.My.MenuHide();
        isSelect.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        isSelect.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
