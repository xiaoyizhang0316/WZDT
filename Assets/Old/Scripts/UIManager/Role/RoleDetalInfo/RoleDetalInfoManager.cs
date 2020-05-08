using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

public class RoleDetalInfoManager : MonoBehaviour
{
    public BaseMapRole currentMapRole;

    public Transform wareHouseTF;
    public Transform shopTF;

    public GameObject goodsPrb;

    public Sprite Seed;
    public Sprite DecayMelon;

    public Sprite melon;

    private int currentInputCount = 0;

    private int currentWareHouseCount = 0;

    private int currentShopCount = 0;
    public Button exit;

    public Toggle sellType_seed;
    public Toggle sellType_melom;
    public Toggle sellType_rottenMelon;


    /// <summary>
    /// 产能条
    /// </summary>
    public Image capacity;

    /// <summary>
    /// 效率条
    /// </summary>
    public Image efficiency;

    /// <summary>
    /// 觉得品牌
    /// </summary>
    public Image brand;

    /// <summary>
    /// 质量
    /// </summary>
    public Image quality;

    /// <summary>
    /// 角色风险
    /// </summary>
    public Text risk;


    /// <summary>
    /// 角色搜寻
    /// </summary>
    public Text search;

    public Text bargain;

    /// <summary>
    /// 角色交付
    /// </summary>
    public Text delivery;

    /// <summary>
    /// 模板成本
    /// </summary>
    public Text templateCost;

    /// <summary>
    /// 装备成本
    /// </summary>
    public Text equipmentCost;

    /// <summary>
    /// 工人成本
    /// </summary>
    public Text workersCost;

    /// <summary>
    /// 地价成本
    /// </summary>
    public Text landCost;

    /// <summary>
    /// 当前行动列表创建TF
    /// </summary>
    public Transform currentActivityTF;

    /// <summary>
    /// 当前交易列表创建TF
    /// </summary>
    public Transform currentDealTF;

    public GameObject prbActivity;

    public GameObject prbDetal;

    /// <summary>
    /// //总收入
    /// </summary>
    public Text totalIncome;
    /// <summary>
    /// 月收入
    /// </summary>
    public Text monthlyIncome;
    /// <summary>
    /// 总满意度
    /// </summary>
    public Text totalSatisfaction;
    /// <summary>
    /// 月满意度
    /// </summary>
    public Text monthlySatisfaction;
    /// <summary>
    /// 总成本
    /// </summary>
    public Text totalCost;
    /// <summary>
    /// 租金成本
    /// </summary>
    public Text rentCost;
    /// <summary>
    /// 运营成本
    /// </summary>
    public Text operatingCosts;
    /// <summary>
    /// 业务成本
    /// </summary>
    public Text businessCost;
    /// <summary>
    /// 交易成本
    /// </summary>
    public Text transactionCost;



    public Sprite seedSprite;
    public Sprite peasantSprite;
    public Sprite MerchantSprite;
    public Sprite DealerSprite;
    public Image IconImage;

    public Button exhibiGoods_Button;
    public GameObject exhibiGoods_Panel;

    public Button confirm;

    public GameObject jiaoyiPRb;

    public Transform skillTF;

    public Transform buffTF;
    // Start is called before the first frame update

    /// <summary>
    /// 需要更新的角色信息
    /// </summary>
    public void InitRoleDetalUpdate()
    {
        if (currentMapRole != null)
        {
            totalIncome.text = currentMapRole.totalProfit.ToString();
            monthlyIncome.text = currentMapRole.monthlyProfit.ToString();
            totalSatisfaction.text = currentMapRole.totalSatisfy.ToString();
            monthlySatisfaction.text = currentMapRole.monthlySatisfy.ToString();
            totalCost.text = currentMapRole.totalCost.ToString();
            rentCost.text = currentMapRole.rentCost.ToString();
            operatingCosts.text = currentMapRole.operationCost.ToString();
            businessCost.text = currentMapRole.activityCost.ToString();
            transactionCost.text = currentMapRole.tradeCost.ToString();
        }

    }
    public void OpenPanel()
    {

        exhibiGoods_Panel.SetActive(true);
    }
    public void ClosePanel()
    {

        exhibiGoods_Panel.SetActive(false);
    }
    /// <summary>
    /// 初始化用户属性UI
    /// </summary>
    public void InitRoleDetalData()
    {
        if (UIManager.My.currentMapRole.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
            IconImage.sprite = seedSprite;
        if (UIManager.My.currentMapRole.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Dealer)
            IconImage.sprite = DealerSprite;
        if (UIManager.My.currentMapRole.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Merchant)
            IconImage.sprite = MerchantSprite;
        if (UIManager.My.currentMapRole.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Peasant)
            IconImage.sprite = peasantSprite;

        capacity.GetComponent<RectTransform>().sizeDelta = new Vector2((float)UIManager.My.currentMapRole.baseRoleData.capacity / 150f * 483f, capacity.GetComponent<RectTransform>().sizeDelta.y);
        efficiency.GetComponent<RectTransform>().sizeDelta = new Vector2((float)UIManager.My.currentMapRole.baseRoleData.efficiency / 150f * 483f, efficiency.GetComponent<RectTransform>().sizeDelta.y);
        brand.GetComponent<RectTransform>().sizeDelta = new Vector2((float)UIManager.My.currentMapRole.baseRoleData.brand / 150f * 483f, brand.GetComponent<RectTransform>().sizeDelta.y);
        quality.GetComponent<RectTransform>().sizeDelta = new Vector2((float)UIManager.My.currentMapRole.baseRoleData.quality / 150f * 483f, quality.GetComponent<RectTransform>().sizeDelta.y);
        risk.text = UIManager.My.currentMapRole.baseRoleData.risk.ToString();
        search.text = UIManager.My.currentMapRole.baseRoleData.search.ToString();
        delivery.text = UIManager.My.currentMapRole.baseRoleData.delivery.ToString();
        bargain.text = UIManager.My.currentMapRole.baseRoleData.bargain.ToString();
        // templateCost.text = UIManager.My.currentMapRole.baseRoleData.baseRoleData.baseCostMonth.ToString();
        // equipmentCost.text = UIManager.My.currentMapRole.baseRoleData.equipCost.ToString();
        // workersCost.text = UIManager.My.currentMapRole.baseRoleData.workerCost.ToString();
        //  landCost.text = UIManager.My.currentMapRole.baseRoleData.landCost.ToString();

        InitActivity();
        InitBuy();
        InitSkill();
        InitBuff();
    }

    void Start()
    {
        exit.onClick.AddListener(() =>
        {
            UIManager.My.ExitRoleDetalInfo();

        });
        sellType_seed.onValueChanged.AddListener((a) =>
        {
            UIManager.My.currentMapRole.isCanSellSeed = sellType_seed.isOn;
        });
        sellType_melom.onValueChanged.AddListener((a) =>
        {
            UIManager.My.currentMapRole.isCanSellMelon = sellType_melom.isOn;
        });
        sellType_rottenMelon.onValueChanged.AddListener((a) =>
        {
            UIManager.My.currentMapRole.isCanRottenMelon = sellType_rottenMelon.isOn;
        });
        //Debug.Log("   D 打开 " + confirm);

        exhibiGoods_Panel.SetActive(false);
        confirm.onClick.AddListener(() =>
        {
            if (UIManager.My.currentMapRole.autoLoading)
            {
                UIManager.My.currentMapRole.autoLoading = false;
                confirm.transform.GetChild(0).GetComponent<Text>().text = "关闭";
                //保证当前存在自动上货
                UIManager.My.currentMapRole. GetComponent<AutomaticLoading>().Close();
            }
            else
            {
                UIManager.My.currentMapRole.autoLoading = true;
                UIManager.My.currentMapRole.GetComponent<AutomaticLoading>().Open(); 
                confirm.transform.GetChild(0).GetComponent<Text>().text = "开启";
                UIManager.My.currentMapRole. GetComponent<AutomaticLoading>().ReleaseSkills(UIManager.My.currentMapRole);
                UIManager.My.currentMapRole. GetComponent<AutomaticLoading>().ShowImage(UIManager.My.currentMapRole);
            }
            exhibiGoods_Panel.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        InitRoleDetalUpdate();
    }

    public void CleanList()
    {
        for (int i = 0; i < wareHouseTF.childCount; i++)
        {
            Destroy(wareHouseTF.GetChild(i).gameObject, 0.00001f);
        }

        for (int i = 0; i < shopTF.childCount; i++)
        {
            Destroy(shopTF.GetChild(i).gameObject, 0.00001f);
        }
    }

    public void InitRoleDetalInfo()
    {
        CleanList();
        this.currentMapRole = UIManager.My.currentMapRole;

        currentInputCount = 0;

        currentWareHouseCount = 0;

        currentShopCount = 0;
        DetectionInput();

    }

    /// <summary>
    /// 添加商品  在生产产品时添加
    /// </summary>
    /// <param name="productData"></param>
    /// <param name="isInput">0 ----仓库   1--- 输入口   2----商店</param>
    public void AddProductToWarehouse(ProductData productData, int isInput)
    {
        GameObject prbong = Instantiate(goodsPrb, wareHouseTF);

        if (productData.productType == GameEnum.ProductType.Melon)
        {
            prbong.transform.GetChild(0).GetComponent<Image>().sprite = melon;
        }

        if (productData.productType == GameEnum.ProductType.Seed)
        {
            prbong.transform.GetChild(0).GetComponent<Image>().sprite = Seed;
        }
        if (productData.productType == GameEnum.ProductType.DecayMelon)
        {
            prbong.transform.GetChild(0).GetComponent<Image>().sprite = DecayMelon;
        }
        prbong.GetComponent<ProductSign>().currentProduct = productData;
        if (isInput == 1)
        {
            prbong.GetComponent<ProductSign>().canSell = false;
            currentInputCount++;
        }
        else if (isInput == 0)
        {
            prbong.GetComponent<ProductSign>().canSell = true;

            currentWareHouseCount++;
        }
        else
        {
            prbong.GetComponent<ProductSign>().canSell = true;
            prbong.transform.SetParent(shopTF);
            currentShopCount++;
        }
    }

    /// <summary>
    /// 检测输入口当前数量是否与当前角色输入口数量一致 如果不一致则创建新增加的
    /// </summary>
    /// <returns></returns>
    public void DetectionInput()
    {
     //  if (currentInputCount < currentMapRole.Input.Count)
     //  {
     //      AddProductToWarehouse(currentMapRole.Input[currentInputCount], 1);
     //      DetectionInput();
     //  }

        if (currentWareHouseCount < currentMapRole.warehouse.Count)
        {
            AddProductToWarehouse(currentMapRole.warehouse[currentWareHouseCount], 0);
            DetectionInput();
        }

        if (currentShopCount < currentMapRole.shop.Count)
        {
            AddProductToWarehouse(currentMapRole.shop[currentShopCount], 3);
            DetectionInput();
        }
    }

    /// <summary>
    /// 当点击按钮后物品会移动到商店中
    /// </summary>
    /// <param name="productSign"></param>
    public void MoveToShop(ProductSign productSign)
    {
        //Debug.Log(productSign.currentProduct.ID);
        currentMapRole.ShiftProductWarehouseToShop(productSign.currentProduct);
        //Debug.Log("当前商店" + currentMapRole.shop.Count);
        //Debug.Log("当前仓库" + currentMapRole.warehouse.Count);
        currentWareHouseCount--;
    }

    public void InitSkill()
    {

        for (int i = 0; i < skillTF.childCount; i++)
        {
            Destroy(skillTF.GetChild(i).gameObject);
        }
        //Debug.Log("交易记录");
        //Debug.Log(UIManager.My.currentMapRole.tradeHistroy.Count);
        List<int> temp = new List<int>(UIManager.My.currentMapRole.tradeHistroy.Keys);
        for (int i = 0; i < UIManager.My.currentMapRole.tradeHistroy.Count; i++)
        {

            GameObject game = Instantiate(jiaoyiPRb, skillTF);
            //game.GetComponent<DetalDataSkillInfo>().InitUI(UIManager.My.currentMapRole.tradeHistroy[temp[i]].selectSkill, UIManager.My.currentMapRole.tradeHistroy[temp[i]].startRole, UIManager.My.currentMapRole.tradeHistroy[temp[i]].endRole,
            //    UIManager.My.currentMapRole.tradeHistroy[temp[i]].skillCost.ToString(), UIManager.My.currentMapRole.tradeHistroy[temp[i]].transactionCost.ToString()
            //    );
            game.GetComponent<DetalDataSkillInfo>().InitUI(UIManager.My.currentMapRole.tradeHistroy[temp[i]]);
        }
    }


    /// <summary>
    /// 创建活动
    /// </summary>
    public void InitActivity()
    {
        for (int i = 0; i < currentActivityTF.childCount; i++)
        {
            Destroy(currentActivityTF.GetChild(i).gameObject);
        }

        for (int i = 0; i < UIManager.My.currentMapRole.AllPassivitySkills.Count; i++)
        {
            if (UIManager.My.currentMapRole.AllPassivitySkills[i].isLock)
            {
                continue;
            }

            GameObject game = Instantiate(prbActivity, currentActivityTF);
            UIManager.My.currentMapRole.AllPassivitySkills[i].skillImage =
                game. GetComponent<SkillActivityUI>().SkillCD;
            UIManager.My.currentMapRole.AllPassivitySkills[i].skillButton =
                game. GetComponent<SkillActivityUI>().openButton;
Debug.Log(UIManager.My.currentMapRole.AllPassivitySkills[i].SkillName);
            game.GetComponent<SkillActivityUI>().contentUI.text = GameDataMgr.My.GetSkillDataByName(   UIManager.My.currentMapRole.AllPassivitySkills[i].SkillName).skillDesc;
            game.GetComponent<SkillActivityUI>().skillName.text =
                UIManager.My.currentMapRole.AllPassivitySkills[i].SkillName;
            game.GetComponent<SkillActivityUI>().cost.text = GameDataMgr.My
                .GetSkillDataByName(UIManager.My.currentMapRole.AllPassivitySkills[i].SkillName).cost.ToString();
            UIManager.My.currentMapRole.AllPassivitySkills[i].ShowImage(UIManager.My.currentMapRole);

            UIManager.My.currentMapRole.AllPassivitySkills[i].SwitchButton(UIManager.My.currentMapRole,game. GetComponent<SkillActivityUI>().openButton);
        }
    }

    /// <summary>
    /// 创建交易记录
    /// </summary>
    public void InitBuy()
    {
        for (int i = 0; i < currentDealTF.childCount; i++)
        {
            Destroy(currentDealTF.GetChild(i).gameObject);
        }

        //for (int i = 0; i < UIManager.My.currentMapRole.tradingHistory.Count; i++)
        //{
        //    GameObject game = Instantiate(prbDetal, currentDealTF);
        //    game.GetComponent<Text>().text = UIManager.My.currentMapRole.tradingHistory[i];
        //}
    }

    public void InitToggle()
    {
        sellType_seed.isOn = UIManager.My.currentMapRole.isCanSellSeed;
        sellType_melom.isOn = UIManager.My.currentMapRole.isCanSellMelon;
        sellType_rottenMelon.isOn = UIManager.My.currentMapRole.isCanRottenMelon;
    }

    public void InitBuff()
    {
        for (int i = 0; i < buffTF.childCount; i++)
        {
            Destroy(buffTF.GetChild(i).gameObject);
        }
        for (int i = 0; i < UIManager.My.currentMapRole.buffList.Count; i++)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/UI/RoleBuff/RoleBuffSign"),buffTF);
            go.GetComponent<RoleBuffSign>().Init(UIManager.My.currentMapRole.buffList[i]);
        }
    }

    public void InitAUTOUI()
    {
        exhibiGoods_Panel.SetActive(true);
        Debug.Log(UIManager.My.currentMapRole.autoLoading);
        if (UIManager.My.currentMapRole.autoLoading)
        {

            confirm.transform.GetChild(0).GetComponent<Text>().text = "关闭";
        }
        else
        {

            confirm.transform.GetChild(0).GetComponent<Text>().text = "开启";
        }
    }

}