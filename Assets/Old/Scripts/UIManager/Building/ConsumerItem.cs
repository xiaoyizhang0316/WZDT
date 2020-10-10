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
        SetSprite(consume);
    }

    /// <summary>
    /// text相关设置
    /// </summary>
    public void InitInfo()
    {
        consumerName.text = currentConsumer.consumeData.consumerName;
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
}
