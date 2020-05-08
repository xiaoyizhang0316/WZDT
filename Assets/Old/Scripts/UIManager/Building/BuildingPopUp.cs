using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;

public class BuildingPopUp : MonoSingleton<BuildingPopUp>
{
    public Building currentBuilding;

    public Text consumerNumber;

    public Text buildingQuality;

    public Image sweetCursor;

    public Image crispCursor;

    public Image consumerLevel;

    //public float x;

    //public float y;

    //public float y2;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="building"></param>
    public void Init(Building building)
    {
        currentBuilding = building;
        InitInfo();
        SetCursor();
        MenuShow();
        SetClass();
    }

    /// <summary>
    /// 初始化文字信息
    /// </summary>
    public void InitInfo()
    {
        //print(currentBuilding.consumerGoList.Count);
        int number = currentBuilding.GetActiveConsuemr();
        consumerNumber.text = number.ToString() + "      " + currentBuilding.consumerGoList.Count.ToString();
        buildingQuality.text = currentBuilding.buildingQualityNeed.ToString();
    }

    /// <summary>
    /// 设置酸甜度指针信息
    /// </summary>
    public void SetCursor()
    {
        sweetCursor.transform.localPosition = new Vector3(18f + (currentBuilding.buildingSweet * 16), sweetCursor.transform.localPosition.y, 0f);
        crispCursor.transform.localPosition = new Vector3(18f + (currentBuilding.buildingCrisp * 16), crispCursor.transform.localPosition.y, 0f);
    }

    /// <summary>
    /// 菜单显示
    /// </summary>
    public void MenuShow()
    {
        //print(Screen.width);
        //print(Screen.height);
        //print(16 * Screen.height / 9f / Screen.width);
        float amount = 16 * Screen.height / 9f / Screen.width;
        float offset = Screen.height / 1080f;
        transform.localPosition = new Vector3(-770f / amount, -500f , 0);
    }

    /// <summary>
    /// 菜单隐藏
    /// </summary>
    public void MenuHide()
    {
        float offset = Screen.height / 1080f;
        float amount = 16 * Screen.height / 9f / Screen.width;
        transform.localPosition = new Vector3(-770f / amount, -700f, 0);
    }

    /// <summary>
    /// 阶级设置
    /// </summary>
    public void SetClass()
    {
        switch (currentBuilding.consumerGoList[0].GetComponent<ConsumeSign>().consumerType)
        {
            case ConsumerType.Oldpao_1:
            case ConsumerType.Oldpao_2:
            case ConsumerType.Oldpao_3:
                consumerLevel.color = Color.white;
                break;
            case ConsumerType.Bluecollar_1:
            case ConsumerType.Bluecollar_2:
            case ConsumerType.Bluecollar_3:
                consumerLevel.color = Color.blue;
                break;
            case ConsumerType.Whitecollar_1:
            case ConsumerType.Whitecollar_2:
            case ConsumerType.Whitecollar_3:
                consumerLevel.color = Color.green;
                break;
            case ConsumerType.Goldencollar_1:
            case ConsumerType.Goldencollar_2:
            case ConsumerType.Goldencollar_3:
                consumerLevel.color = Color.yellow;
                break;
            case ConsumerType.Elite_1:
            case ConsumerType.Elite_2:
            case ConsumerType.Elite_3:
                consumerLevel.color = Color.red;
                break;
        }
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
