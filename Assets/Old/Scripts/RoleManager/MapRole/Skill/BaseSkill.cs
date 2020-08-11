using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    public BaseMapRole role;

    public bool IsOpen;

    public bool isPlay;

    public bool isAvaliable = true;

    public bool isAnimPlaying = false;

    /// <summary>
    /// 需要多棱镜发现的buff
    /// </summary>
    public List<int> goodSpecialBuffList;
    /// <summary>
    /// 需要多棱镜发现的buff实体
    /// </summary>
    public List<BaseBuff> goodBaseBuffs = new List<BaseBuff>();

    /// <summary>
    /// 不需要多棱镜发现的buff
    /// </summary>
    public List<int> badSpecialBuffList;
    /// <summary>
    /// 不需要多棱镜发现的buff实体
    /// </summary>
    public List<BaseBuff> badBaseBuffs = new List<BaseBuff>();

    /// <summary>
    /// 技能描述
    /// </summary>
    public string skillDesc;
    /// <summary>
    /// 附带buff列表
    /// </summary>
    public List<int> buffList;

    public List<GameObject> animationPart = new List<GameObject>();

    // Start is called before the first frame update
    public void Start()
    {
        role = GetComponent<BaseMapRole>();
        if (IsOpen)
        {
            UnleashSkills();
        }
        for (int i = 0; i < goodSpecialBuffList.Count; i++)
        {
            BaseBuff buff = new BaseBuff();
            BuffData data = GameDataMgr.My.GetBuffDataByID(goodSpecialBuffList[i]);
            buff.Init(data);
            buff.targetRole = role;
            buff.castRole = role;
            buff.buffRole = role;
            goodBaseBuffs.Add(buff);
        }
        for (int i = 0; i < badSpecialBuffList.Count; i++)
        {
            BaseBuff buff = new BaseBuff();
            BuffData data = GameDataMgr.My.GetBuffDataByID(badSpecialBuffList[i]);
            buff.Init(data);
            buff.targetRole = role;
            buff.castRole = role;
            buff.buffRole = role;
            badBaseBuffs.Add(buff);
        }
    }

    /// <summary>
    /// 释放技能
    /// </summary>
    public abstract void Skill();

    public virtual void UnleashSkills()
    {
        isPlay = true;
        float d = 1f / (role.baseRoleData.efficiency * 0.05f);
        transform.DORotate(transform.eulerAngles, d).OnComplete(() =>
        {
            Skill();
            if (IsOpen)
            {
                UnleashSkills();
            }
        });
    }

    /// <summary>
    /// 添加增益Buff
    /// </summary>
    public void AddRoleBuff(TradeData tradeData)
    {
        if (!IsOpen)
        {
            return;
        }

        for (int i = 0; i < buffList.Count; i++)
        {
            var buff = GameDataMgr.My.GetBuffDataByID(buffList[i]);
            BaseBuff baseb = new BaseBuff();
            baseb.Init(buff);
            baseb.SetRoleBuff(PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole)), PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole)), PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole)));
        }
        if (role.isNpc)
        {
            if (role.npcScript.isCanSeeEquip)
            {
                for (int i = 0; i < role.npcScript.NPCBuffList.Count; i++)
                {
                    var buff = GameDataMgr.My.GetBuffDataByID(role.npcScript.NPCBuffList[i]);
                    BaseBuff baseb = new BaseBuff();
                    baseb.Init(buff);
                    baseb.SetRoleBuff(PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole)), PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole)), PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole)));
                }
            }
        }
    }

    /// <summary>
    /// 移除增益buff
    /// </summary>
    /// <param name="tradeData"></param>
    public void DeteleRoleBuff(TradeData tradeData)
    {
        BaseMapRole targetRole = PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole));
        foreach (int i in buffList)
        {
            targetRole.RemoveBuffById(i);
        }
        if (role.isNpc)
        {
            if (role.npcScript.isCanSeeEquip)
            {
                foreach (int i in role.npcScript.NPCBuffList)
                {
                    targetRole.RemoveBuffById(i);
                }
            }
        }
    }

    /// <summary>
    /// 重启释放技能
    /// </summary>
    public void ReUnleashSkills()
    {
        IsOpen = true;
        Debug.Log("重启技能" + role.baseRoleData.ID);
        if (role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Dealer)
        {
            if (!isPlay)
                UnleashSkills();
        }
        else
            UnleashSkills();
    }

    /// <summary>
    ///  取消技能
    /// </summary>
    public void CancelSkill()
    {
       
        IsOpen = false;
    }

    //public class Solution
    //{
    //    public void GameOfLife(int[][] board)
    //    {
    //        for (int i = 0; i < board.Length - 1; i++)
    //        {
    //            for (int j = 0; j < board[0].Length - 1; j++)
    //            {
    //                if (IsLife(board,i,j))
    //                {
    //                    if (board[i][j] == 0)
    //                    {
    //                        board[i][j] = 2;
    //                    }
    //                }
    //                else
    //                {
    //                    if (board[i][j] == 1)
    //                    {
    //                        board[i][j] = 3;
    //                    }
    //                }
    //            }
    //        }
    //        for (int i = 0; i < board.Length - 1; i++)
    //        {
    //            for (int j = 0; j < board[0].Length - 1; j++)
    //            {
    //                if (board[i][j] == 3)
    //                {
    //                    //board[][]
    //                }
    //            }
    //        }

    //    }

    //    public bool IsLife(int[][] board,int x,int y)
    //    {
    //        int count = 0;
    //        for (int i = Mathf.Max(x-1,0); i < Mathf.Min(x+1,board.Length); i++)
    //        {
    //            for (int j = Mathf.Max(y-1,0); j < Mathf.Min(y + 1, board[0].Length); j++)
    //            {
    //                if (board[i][j] % 2 == 1 && i != x && j != y)
    //                    count++;
    //            }
    //        }
    //        if (board[x][y] % 2 == 0)
    //        {
    //            if (count == 3)
    //            {
    //                return true;
    //            }
    //            else
    //            {
    //                return false;
    //            }
    //        }
    //        else
    //        {
    //            if (count < 2 || count > 3)
    //            {
    //                return false;
    //            }
    //            else
    //            {
    //                return true;
    //            }
    //        }
    //    }
    //}
}
