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

        //todo
      //  capacity.GetComponent<RectTransform>().sizeDelta = new Vector2((float)UIManager.My.currentMapRole.baseRoleData.capacity / 150f * 483f, capacity.GetComponent<RectTransform>().sizeDelta.y);
      //  efficiency.GetComponent<RectTransform>().sizeDelta = new Vector2((float)UIManager.My.currentMapRole.baseRoleData.efficiency / 150f * 483f, efficiency.GetComponent<RectTransform>().sizeDelta.y);
      //  brand.GetComponent<RectTransform>().sizeDelta = new Vector2((float)UIManager.My.currentMapRole.baseRoleData.brand / 150f * 483f, brand.GetComponent<RectTransform>().sizeDelta.y);
      //  quality.GetComponent<RectTransform>().sizeDelta = new Vector2((float)UIManager.My.currentMapRole.baseRoleData.quality / 150f * 483f, quality.GetComponent<RectTransform>().sizeDelta.y);
        //todo
        // risk.text = UIManager.My.currentMapRole.baseRoleData.risk.ToString();
       // search.text = UIManager.My.currentMapRole.baseRoleData.search.ToString();
       // delivery.text = UIManager.My.currentMapRole.baseRoleData.delivery.ToString();
       // bargain.text = UIManager.My.currentMapRole.baseRoleData.bargain.ToString();
       
 
    }

    void Start()
    {
        exit.onClick.AddListener(() =>
        {
            UIManager.My.ExitRoleDetalInfo();

        });
      
        //Debug.Log("   D 打开 " + confirm);

        exhibiGoods_Panel.SetActive(false);
        confirm.onClick.AddListener(() =>
        {
           
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
     //   GameObject prbong = Instantiate(goodsPrb, wareHouseTF);
//
     //   if (productData.productType == GameEnum.ProductType.Melon)
     //   {
     //       prbong.transform.GetChild(0).GetComponent<Image>().sprite = melon;
     //   }
//
     //   if (productData.productType == GameEnum.ProductType.Seed)
     //   {
     //       prbong.transform.GetChild(0).GetComponent<Image>().sprite = Seed;
     //   }
     //   if (productData.productType == GameEnum.ProductType.DecayMelon)
     //   {
     //       prbong.transform.GetChild(0).GetComponent<Image>().sprite = DecayMelon;
     //   }
     //   prbong.GetComponent<ProductSign>().currentProduct = productData;
     //   if (isInput == 1)
     //   {
     //       prbong.GetComponent<ProductSign>().canSell = false;
     //       currentInputCount++;
     //   }
     //   else if (isInput == 0)
     //   {
     //       prbong.GetComponent<ProductSign>().canSell = true;
//
     //       currentWareHouseCount++;
     //   }
     //   else
     //   {
     //       prbong.GetComponent<ProductSign>().canSell = true;
     //       prbong.transform.SetParent(shopTF);
     //       currentShopCount++;
     //   }
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
}