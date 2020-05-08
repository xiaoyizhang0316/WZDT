using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInfo : MonoSingleton<BuildingInfo>
{
    public Building currentBuilding;

    public Text buildingName;

    public Text consumerNumber;

    public Text buildingQuality;

    public GameObject consumerItemPrb;

    public Transform consumerListTF;

    public Image sweetCursor;

    public Image crispCursor;

    public Image buildingSatisfy;

    public List<Sprite> satisfyList;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="building"></param>
    public void Init(Building building)
    {
        currentBuilding = building;
        InitInfo();
        SetCursor();
        InitConsumerList();
        SetBuildingSatisfy();
        MenuShow();
    }

    /// <summary>
    /// 初始化文字信息
    /// </summary>
    public void InitInfo()
    {
        int number = currentBuilding.GetActiveConsuemr();
        buildingQuality.text = currentBuilding.buildingQualityNeed.ToString();
        consumerNumber.text = number.ToString() + "        " + currentBuilding.consumerGoList.Count.ToString();
    }

    /// <summary>
    /// 设置酸甜度cursor箭头位置
    /// </summary>
    public void SetCursor()
    {
        sweetCursor.transform.localPosition = new Vector3(currentBuilding.buildingSweet * 19, sweetCursor.transform.localPosition.y, 0f);
        crispCursor.transform.localPosition = new Vector3(currentBuilding.buildingCrisp * 19, crispCursor.transform.localPosition.y, 0f);
    }

    /// <summary>
    /// 设置大楼满意度笑脸
    /// </summary>
    public void SetBuildingSatisfy()
    {
        float result = currentBuilding.GetTotalSatisfy();
        if (result < -100)
        {
            buildingSatisfy.sprite = satisfyList[4];
        }
        else if (result < -50)
        {
            buildingSatisfy.sprite = satisfyList[3];
        }
        else if (result < 50)
        {
            buildingSatisfy.sprite = satisfyList[2];
        }
        else if (result < 100)
        {
            buildingSatisfy.sprite = satisfyList[1];
        }
        else
        {
            buildingSatisfy.sprite = satisfyList[0];
        }

    }

    /// <summary>
    /// 初始化楼内消费者名单
    /// </summary>
    public void InitConsumerList()
    {
        ClearList();
        for (int i = 0; i < currentBuilding.consumerGoList.Count; i++)
        {
            GameObject go = Instantiate(consumerItemPrb, consumerListTF);
            go.GetComponent<ConsumerItem>().Init(currentBuilding.consumerGoList[i]);
        }
    }

    /// <summary>
    /// 清空菜单
    /// </summary>
    public void ClearList()
    {
        int number = consumerListTF.childCount;
        for (int i = 0; i < number; i++)
        {
            Destroy(consumerListTF.GetChild(0).gameObject);
        }
    }

    /// <summary>
    /// 菜单显示
    /// </summary>
    public void MenuShow()
    {
        transform.localPosition = new Vector3(-652f, -123f, 0f);
    }

    /// <summary>
    /// 菜单隐藏
    /// </summary>
    public void MenuHide()
    {
        transform.localPosition = new Vector3(-634f, -1275f, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        MenuHide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
