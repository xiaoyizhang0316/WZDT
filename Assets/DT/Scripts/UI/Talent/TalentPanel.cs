using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TalentPanel : MonoSingleton<TalentPanel>
{
    public List<TalentItem> dingWei = new List<TalentItem>();

    public List<TalentItem> yeWuXiTong = new List<TalentItem>();

    public List<TalentItem> guanJianZiYuanNengLi = new List<TalentItem>();

    public List<TalentItem> yingLiMoShi = new List<TalentItem>();

    public List<TalentItem> xianJinLiu = new List<TalentItem>();

    public List<TalentItem> qiYeJiaZhi = new List<TalentItem>();

    public Text restPoint;

    public int totalPoint;

    public int usedPoint;

    public TalentDesc talentDesc;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        usedPoint = 0;
        talentDesc.Close();
        for (int i = 0; i < dingWei.Count; i++)
        {
            dingWei[i].isSelect = PlayerData.My.dingWei[i];
            if (dingWei[i].isSelect)
            {
                usedPoint++;
            }
        }
        for (int i = 0; i < yeWuXiTong.Count; i++)
        {
            yeWuXiTong[i].isSelect = PlayerData.My.yeWuXiTong[i];
            if (yeWuXiTong[i].isSelect)
            {
                usedPoint++;
            }
        }
        for (int i = 0; i < guanJianZiYuanNengLi.Count; i++)
        {
            guanJianZiYuanNengLi[i].isSelect = PlayerData.My.guanJianZiYuanNengLi[i];
            if (guanJianZiYuanNengLi[i].isSelect)
            {
                usedPoint++;
            }
        }
        for (int i = 0; i < yingLiMoShi.Count; i++)
        {
            yingLiMoShi[i].isSelect = PlayerData.My.yingLiMoShi[i];
            if (yingLiMoShi[i].isSelect)
            {
                usedPoint++;
            }
        }
        for (int i = 0; i < xianJinLiu.Count; i++)
        {
            xianJinLiu[i].isSelect = PlayerData.My.xianJinLiu[i];
            if (xianJinLiu[i].isSelect)
            {
                usedPoint++;
            }
        }
        for (int i = 0; i < qiYeJiaZhi.Count; i++)
        {
            qiYeJiaZhi[i].isSelect = PlayerData.My.qiYeJiaZhi[i];
            if (qiYeJiaZhi[i].isSelect)
            {
                usedPoint++;
            }
        }
        if (NetworkMgr.My.levelProgressList.Count >= 4)
        {
            UnlockTalent(1);
            UnlockTalent(2);
        }
        else
        {
            LockTalent(1);
            LockTalent(2);
        }
        if (NetworkMgr.My.levelProgressList.Count >= 5)
        {
            UnlockTalent(3);
            UnlockTalent(4);
        }
        else
        {
            LockTalent(3);
            LockTalent(4);
        }
        if (NetworkMgr.My.levelProgressList.Count >= 6)
        {
            UnlockTalent(5);
            UnlockTalent(6);
        }
        else
        {
            LockTalent(5);
            LockTalent(6);
        }
        for (int i = 1; i < 7; i++)
        {
            CheckLabel(i);
        }
        CountTotalTalentPoint();
        UpdateTalentPoint();
    }

    /// <summary>
    /// 解锁整块天赋
    /// </summary>
    /// <param name="index"></param>
    public void UnlockTalent(int index)
    {
        switch (index)
        {
            case 1:
                {
                    dingWei[0].transform.parent.Find("lockImg").DOScale(0, 0).Play();
                    foreach (TalentItem item in dingWei)
                    {
                        item.CheckStatus();
                    }
                    break;
                }
            case 2:
                {
                    guanJianZiYuanNengLi[0].transform.parent.Find("lockImg").DOScale(0, 0).Play();
                    foreach (TalentItem item in guanJianZiYuanNengLi)
                    {
                        item.CheckStatus();
                    }
                    break;
                }
            case 3:
                {
                    yeWuXiTong[0].transform.parent.Find("lockImg").DOScale(0, 0).Play();
                    foreach (TalentItem item in yeWuXiTong)
                    {
                        item.CheckStatus();
                    }
                    break;
                }
            case 4:
                {
                    xianJinLiu[0].transform.parent.Find("lockImg").DOScale(0, 0).Play();
                    foreach (TalentItem item in xianJinLiu)
                    {
                        item.CheckStatus();
                    }
                    break;
                }
            case 5:
                {
                    yingLiMoShi[0].transform.parent.Find("lockImg").DOScale(0, 0).Play();
                    foreach (TalentItem item in yingLiMoShi)
                    {
                        item.CheckStatus();
                    }
                    break;
                }
            case 6:
                {
                    qiYeJiaZhi[0].transform.parent.Find("lockImg").DOScale(0, 0).Play();
                    foreach (TalentItem item in qiYeJiaZhi)
                    {
                        item.CheckStatus();
                    }
                    break;
                }
            default:
                break;
        }
    }

    /// <summary>
    /// 锁定整块天赋
    /// </summary>
    /// <param name="index"></param>
    public void LockTalent(int index)
    {
        switch (index)
        {
            case 1:
                {
                    dingWei[0].transform.parent.Find("lockImg").DOScale(1, 0).Play();
                    break;
                }
            case 2:
                {
                    guanJianZiYuanNengLi[0].transform.parent.Find("lockImg").DOScale(1, 0).Play();
                    break;
                }
            case 3:
                {
                    yeWuXiTong[0].transform.parent.Find("lockImg").DOScale(1, 0).Play();
                    break;
                }
            case 4:
                {
                    xianJinLiu[0].transform.parent.Find("lockImg").DOScale(1, 0).Play();
                    break;
                }
            case 5:
                {
                    yingLiMoShi[0].transform.parent.Find("lockImg").DOScale(1, 0).Play();
                    break;
                }
            case 6:
                {
                    qiYeJiaZhi[0].transform.parent.Find("lockImg").DOScale(1, 0).Play();
                    break;
                }
            default:
                break;
        }
    }

    /// <summary>
    /// 检测是否显示文字
    /// </summary>
    /// <param name="index"></param>
    public void CheckLabel(int index)
    {
        string str = "";
        switch (index)
        {
            case 1:
                {
                    if (PlayerData.My.isOneFinish[0])
                    {
                        str = "定位";
                    }
                    dingWei[0].transform.parent.Find("Label").GetComponent<Text>().text = str;
                    break;
                }
            case 2:
                {
                    if (PlayerData.My.isOneFinish[1])
                    {
                        str = "关键资源能力";
                    }
                    guanJianZiYuanNengLi[0].transform.parent.Find("Label").GetComponent<Text>().text = str;
                    break;
                }
            case 3:
                {
                    if (PlayerData.My.isOneFinish[2])
                    {
                        str = "业务系统";
                    }
                    yeWuXiTong[0].transform.parent.Find("Label").GetComponent<Text>().text = str;
                    break;
                }
            case 4:
                {
                    if (PlayerData.My.isOneFinish[3])
                    {
                        str = "现金流结构";
                    }
                    xianJinLiu[0].transform.parent.Find("Label").GetComponent<Text>().text = str;
                    break;
                }
            case 5:
                {
                    if (PlayerData.My.isOneFinish[4])
                    {
                        str = "盈利模式";
                    }
                    yingLiMoShi[0].transform.parent.Find("Label").GetComponent<Text>().text = str;
                    break;
                }
            case 6:
                {
                    if (PlayerData.My.isOneFinish[5])
                    {
                        str = "企业价值";
                    }
                    qiYeJiaZhi[0].transform.parent.Find("Label").GetComponent<Text>().text = str;
                    break;
                }
            default:
                break;
        }
    }

    /// <summary>
    /// 统计总共可用天赋点数
    /// </summary>
    public void CountTotalTalentPoint()
    {
        totalPoint = 0;
        for (int i = 0; i < NetworkMgr.My.levelProgressList.Count; i++)
        {
            if (i <= 3)
            {
                totalPoint += NetworkMgr.My.levelProgressList[i].starNum;
            }
            else
            {
                totalPoint += NetworkMgr.My.levelProgressList[i].starNum;
            }
        }
        usedPoint = 0;
        for (int i = 0; i < dingWei.Count; i++)
        {
            if (PlayerData.My.dingWei[i])
            {
                usedPoint++;
            }
        }
        for (int i = 0; i < yeWuXiTong.Count; i++)
        {
            if (PlayerData.My.yeWuXiTong[i])
            {
                usedPoint++;
            }
        }
        for (int i = 0; i < guanJianZiYuanNengLi.Count; i++)
        {
            if (PlayerData.My.guanJianZiYuanNengLi[i])
            {
                usedPoint++;
            }
        }
        for (int i = 0; i < yingLiMoShi.Count; i++)
        {
            if (PlayerData.My.yingLiMoShi[i])
            {
                usedPoint++;
            }
        }
        for (int i = 0; i < xianJinLiu.Count; i++)
        {
            if (PlayerData.My.xianJinLiu[i])
            {
                usedPoint++;
            }
        }
        for (int i = 0; i < qiYeJiaZhi.Count; i++)
        {
            if (PlayerData.My.qiYeJiaZhi[i])
            {
                usedPoint++;
            }
        }
    }

    /// <summary>
    /// 重置所有天赋
    /// </summary>
    public void ResetAllTalent()
    {
        for (int i = 0; i < dingWei.Count; i++)
        {
            dingWei[i].isSelect = false;
            dingWei[i].CheckStatus();
        }
        for (int i = 0; i < guanJianZiYuanNengLi.Count; i++)
        {
            guanJianZiYuanNengLi[i].isSelect = false;
            guanJianZiYuanNengLi[i].CheckStatus();
        }
        for (int i = 0; i < yeWuXiTong.Count; i++)
        {
            yeWuXiTong[i].isSelect = false;
            yeWuXiTong[i].CheckStatus();
        }
        for (int i = 0; i < xianJinLiu.Count; i++)
        {
            xianJinLiu[i].isSelect = false;
            xianJinLiu[i].CheckStatus();
        }
        for (int i = 0; i < yingLiMoShi.Count; i++)
        {
            yingLiMoShi[i].isSelect = false;
            yingLiMoShi[i].CheckStatus();
        }
        for (int i = 0; i < qiYeJiaZhi.Count; i++)
        {
            qiYeJiaZhi[i].isSelect = false;
            qiYeJiaZhi[i].CheckStatus();
        }
        usedPoint = 0;
        UpdateTalentPoint();
    }

    /// <summary>
    /// 保存并关闭面板
    /// </summary>
    public void SaveAndQuit()
    {
        for (int i = 0; i < dingWei.Count; i++)
        {
            PlayerData.My.dingWei[i] = dingWei[i].isSelect;
        }
        for (int i = 0; i < yeWuXiTong.Count; i++)
        {
            PlayerData.My.yeWuXiTong[i] = yeWuXiTong[i].isSelect;
        }
        for (int i = 0; i < guanJianZiYuanNengLi.Count; i++)
        {
            PlayerData.My.guanJianZiYuanNengLi[i] = guanJianZiYuanNengLi[i].isSelect;
        }
        for (int i = 0; i < yingLiMoShi.Count; i++)
        {
            PlayerData.My.yingLiMoShi[i] = yingLiMoShi[i].isSelect;
        }
        for (int i = 0; i < xianJinLiu.Count; i++)
        {
            PlayerData.My.xianJinLiu[i] = xianJinLiu[i].isSelect;
        }
        for (int i = 0; i < qiYeJiaZhi.Count; i++)
        {
            PlayerData.My.qiYeJiaZhi[i] = qiYeJiaZhi[i].isSelect;
        }
        NetworkMgr.My.UpdateTalent(
            ()=> {
            Close();
        },()=> {
            HttpManager.My.ShowTip("保存失败！");
        });
        //Close();
    }

    /// <summary>
    /// 关闭面板
    /// </summary>
    public void Close()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 3000f);
    }

    /// <summary>
    /// 面板打开同时初始化
    /// </summary>
    public void Open()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(1012f, -54.5f);
        MapGuideManager.My.GetComponent<MapObject>().isTalentPanelOpen = true;
        Init();
    }

    public void OpenTalentDesc(TalentItem item)
    {
        talentDesc.Init(item);
    }

    public void CloseTalentDesc()
    {
        talentDesc.Close();
    }

    /// <summary>
    /// 更新剩余天赋点数文字
    /// </summary>
    public void UpdateTalentPoint()
    {
        restPoint.text = (totalPoint - usedPoint).ToString() + "/" + totalPoint.ToString();
    }

    private bool isRunning = false;

    public void ShowNoTalentInfo()
    {
        if (isRunning)
        {
            return;
        }
        else
        {
            isRunning = true;
            restPoint.DOBlendableColor(Color.red, 0.5f).Play().SetLoops(3).OnComplete(()=> {
                isRunning = false;
                restPoint.color = Color.white;
            });
        }
    }

    public void Start()
    {
        for (int i = 0; i < dingWei.Count; i++)
        {
            dingWei[i].index = 1;
            dingWei[i].number = i;
        }
        for (int i = 0; i < guanJianZiYuanNengLi.Count; i++)
        {
            guanJianZiYuanNengLi[i].index = 2;
            guanJianZiYuanNengLi[i].number = i;
        }
        for (int i = 0; i < yeWuXiTong.Count; i++)
        {
            yeWuXiTong[i].index = 3;
            yeWuXiTong[i].number = i;
        }
        for (int i = 0; i < xianJinLiu.Count; i++)
        {
            xianJinLiu[i].index = 4;
            xianJinLiu[i].number = i;
        }
        for (int i = 0; i < yingLiMoShi.Count; i++)
        {
            yingLiMoShi[i].index = 5;
            yingLiMoShi[i].number = i;
        }
        for (int i = 0; i < qiYeJiaZhi.Count; i++)
        {
            qiYeJiaZhi[i].index = 6;
            qiYeJiaZhi[i].number = i;
        }
        Close();
    }
}
