using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DataEnum 
{
  装备_增加,
  使用透视镜,
  使用多棱镜,
  使用广角镜,
  消费者_口味击杀,
  消费者_点击进度条,
  角色_删除角色,
  角色_升级,
  角色_查看未解锁,
  角色_最终角色数量,
  角色_清仓,
  角色_查看自己属性,
  角色_查看NPC属性,
  角色_放置种子商,
  角色_放置农民,
  角色_放置贸易商,
  角色_放置零售商,
  交易_发起外部交易,
  交易_发起的内部交易,
  交易_五秒内查看交易的次数,
  交易_建交易,
  交易_删交易,
  交易_改交易,
  交易_优化率,
  看子弹属性,
  浪费的瓜,
  时间_暂停次数,
  时间_暂停时长,
  统计付钱时间占比,
  赤字次数,
}
[SerializeField]
public class DataUpload
{
  /// <summary>
  /// 装备_增加
  /// </summary>
  public int equip_add;

  /// <summary>
  /// 使用透视镜
  /// </summary>
  public int  use_tsj;

  /// <summary>
  /// 使用多棱镜
  /// </summary>
  public int use_dlj;

  /// <summary>
  /// 使用广角镜
  /// </summary>
  public int use_gjj;

  /// <summary>
  /// 消费者_口味击杀
  /// </summary>
  public int consumer_tasteskill;

  /// <summary>
  /// 消费者_点击进度条
  /// </summary>
  public int consumer_ClicksProgressBar;

  /// <summary>
  /// 角色_删除角色
  /// </summary>
  public int role_deleteRole;

  /// <summary>
  /// 角色_升级
  /// </summary>
  public int role_update;

  /// <summary>
  /// 角色_查看未解锁
  /// </summary>
  public int role_checkUnlockRole;
  
  /// <summary>
  /// 角色_最终角色数量
  /// </summary>
  public int role_roleNum;

  /// <summary>
  /// 角色_清仓
  /// </summary>
  public int role_clearWarehouse;

  /// <summary>
  /// 角色_查看自己属性
  /// </summary>
  public int role_checkselfData;

  /// <summary>
  /// 角色_查看Npc属性
  /// </summary>
  public int role_checkNpcData;

  /// <summary>
  /// 角色_放置角色比例
  /// </summary>
  public string  role_ReleaseRolePercentage;

  /// <summary>
  /// 交易_发起外部交易
  /// </summary>
  public int trad_DealNpcOrigination;

  /// <summary>
  /// 交易_发起的内部交易
  /// </summary>
  public int trad_DealSelfOrigination;

  /// <summary>
  /// 交易_五秒内查看交易的次数
  /// </summary>
  public int trad_tradesFiveSeconds;

  /// <summary>
  /// 交易_建交易
  /// </summary>
  public int trad_build;

  /// <summary>
  /// 交易_删交易
  /// </summary>
  public int trad_delete;

  /// <summary>
  /// 交易_改交易
  /// </summary>
  public int trad_change;

  /// <summary>
  /// 交易_优化率
  /// </summary>
  public float trad_optimize;

  /// <summary>
  /// 看子弹属性
  /// </summary>
  public int checkbullet;

  /// <summary>
  /// 浪费的瓜
  /// </summary>
  public int wastefulbullet;

  /// <summary>
  /// 时间_暂停次数
  /// </summary>
  public int time_PauseTimes;

  /// <summary>
  /// 时间_暂停时长
  /// </summary>
  public int time_PauseTime;

  /// <summary>
  /// 统计付钱时间占比
  /// </summary>
  public int percentageTime;

  /// <summary>
  /// 赤字次数
  /// </summary>
  public int deficitNumber;

}
