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
        MenuShow();
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
}
