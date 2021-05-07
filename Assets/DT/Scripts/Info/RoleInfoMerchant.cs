using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DT.Fight.Bullet;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoleInfoMerchant : BaseRoleInfoAdd
{
    public Text effect;
    public Text MoveSpeed;

    public Text efficiency;

    public Text TradDown;

    public Text tradCost;
    public Text risk;
    public Text montyCost;

    public Text technology;

    public GameObject efficiencyBar;
    public GameObject effectyBar;

    public Transform buffTf;
    // Start is called before the first frame update
    void Start()
    {
        
        //InvokeRepeating("UpdateBar",0.1f,0.2f);
    }

    public override void Init()
    {
        float efficiencyNum = (CreatRoleManager.My.finalEfficiency);
        float add = 1f;
        float encourage = PlayerData.My.GetMapRoleById(CreatRoleManager.My.CurrentRole.ID).encourageLevel + CreatRoleManager.My.finalEncourageAdd;
        MoveSpeed.color = Color.white;
        if (encourage > 0)
        {
            add += encourage * 0.05f;
            MoveSpeed.color = Color.green;
        }
        else if (encourage < 0)
        {
            add += encourage * 0.1f;
            MoveSpeed.color = Color.red;
        }
        efficiencyNum *= add;
        effect.text = CreatRoleManager.My.finalEffect.ToString();
        MoveSpeed.text = efficiencyNum.ToString("F2")+"%";
        efficiency.text = CreatRoleManager.My.finalEfficiency.ToString();
        TradDown.text = (CreatRoleManager.My.finalEffect *0.3f + 24f).ToString()+"%";
        tradCost.text  =  CreatRoleManager.My.finalTradeCost.ToString();
        risk .text =  CreatRoleManager.My.finalRiskResistance.ToString();
        if (StageGoal.My.currentType == GameEnum.StageType.Normal && !CommonParams.fteList.Contains(SceneManager.GetActiveScene().name))
        {
            montyCost.text = (CreatRoleManager.My.finalCost * 2).ToString();
        }
        else
        {
            montyCost.text = (CreatRoleManager.My.finalCost).ToString();
        }
        technology.text = CreatRoleManager.My.finalTechAdd.ToString();
        ShowLastpruduct(CreatRoleManager.My.CurrentRole);

    }

    public override void UpdateBar()
    {
        efficiencyBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2(CreatRoleManager.My.finalEfficiency / 120f * 150f,
                efficiencyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f).Play();
        effectyBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2(CreatRoleManager.My.finalEffect / 120f * 150f,
                effectyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f).Play();
    }
    
    
    private List<ProductData> _datas  = new List<ProductData>();
 public void ShowLastpruduct(Role role)
    {
        BaseMapRole baseMapRole =    PlayerData.My.GetMapRoleById(role.ID);
        for (int i = 0; i <CreatRoleManager.My.goodsTF.childCount; i++)
        {
            Destroy(CreatRoleManager.My.goodsTF.GetChild(i).gameObject);
        }
        _datas.Clear();
        for (int i = 0; i <baseMapRole.warehouse.Count; i++)
        {
            baseMapRole.warehouse[i].RepeatBulletCount = 0;
        }

        for (int i = 0; i <  baseMapRole.warehouse.Count; i++)
        {
            int  isSameCount = 0;
            int count = 0;
            for (int j = 0; j <_datas.Count; j++)
            {
            
                if (_datas[j].CheckSame(baseMapRole.warehouse[i]))
                {
                    isSameCount++;
                    _datas[j].RepeatBulletCount++;
                    count++;
                }
            }

            if (isSameCount>0)
            {
  
                continue;
            }
            _datas.Add(baseMapRole.warehouse[i]);
            
        }

        for (int i = 0; i < _datas.Count; i++)
        {
            

         //   baseMapRole.warehouse[i].RepeatBulletCount = isSameCount;
            GameObject Pruductgame =  Instantiate(CreatRoleManager.My.goodsPrb, CreatRoleManager.My.goodsTF);
            Pruductgame.GetComponent<ProductSign>().currentProduct =
                _datas[i];

            Pruductgame.GetComponent<ProductSign>().conut.text =_datas[i].RepeatBulletCount.ToString();
      
       
          
        
            switch (baseMapRole.warehouse[i].bulletType )
            {
                case BulletType.Bomb:
                    Pruductgame.GetComponent<Image>().sprite = RoleUpdateInfo.My.AOE;
                    break;
                case BulletType.NormalPP:
                    Pruductgame.GetComponent<Image>().sprite = RoleUpdateInfo.My.normallpp;
                    break;

                case BulletType.Lightning:
                    Pruductgame.GetComponent<Image>().sprite = RoleUpdateInfo.My.lightning;
                    break;

                case BulletType.summon:
                    Pruductgame.GetComponent<Image>().sprite = RoleUpdateInfo.My.tow;
                    break;

            }
            if (Pruductgame.GetComponent<ProductSign>().currentProduct.wasteBuffList.Count > 0)
            {
                Pruductgame.GetComponent<Image>().color = new Color(1, 0.6f, 0.6f, 1);
            }
        
         //  if (PlayerData.My.client != null)
         //  {
         //      Pruductgame.GetComponentInChildren<Text>().text = baseMapRole.warehouse[i].RepeatBulletCount.ToString();
         //  }
         //  else
         //  {
         //      Pruductgame.GetComponentInChildren<Text>().gameObject.SetActive(false);
         //  }
        }
    }
    public override void UpdateBuff()
    {
        List<int> equipId = CreatRoleManager.My.EquipList.Keys.ToList();
        List<int> tempBuffList = new List<int>();
        for (int i = 0; i < equipId.Count; i++)
        {
            GearData data = GameDataMgr.My.GetGearData(equipId[i]);
            if (data.buffList[0] != -1)
            {
                tempBuffList.Add(data.buffList[0]);
            }
        }
        BaseMapRole role = PlayerData.My.GetMapRoleById(CreatRoleManager.My.CurrentRole.ID);
        tempBuffList.AddRange(role.tasteBuffList);
        for (int i = 0; i < 4; i++)
        {
            try
            {
                buffTf.GetChild(i).gameObject.SetActive(true);
                buffTf.GetChild(i).GetComponent<WaveBuffSign>().Init(tempBuffList[i]);
            }
            catch (System.Exception e)
            {
                buffTf.GetChild(i).gameObject.SetActive(false);
            }
        }

    }
}
