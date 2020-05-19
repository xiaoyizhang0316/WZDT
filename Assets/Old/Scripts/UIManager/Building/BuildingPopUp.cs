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
        MenuShow();
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
