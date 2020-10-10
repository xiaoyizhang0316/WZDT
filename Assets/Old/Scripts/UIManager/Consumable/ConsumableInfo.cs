using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;



public class ConsumableInfo : MonoSingleton<ConsumableInfo>
{

    public Image consumableSprite;

    public Text consumableName;

    public Text consumableDesc;

    public Text consumableNum;

    public Tweener tweener;

    /// <summary>
    /// 消耗品初始化
    /// </summary>
    /// <param name="consumableId"></param>
    /// <param name="num"></param>
    /// <param name="y"></param>
    public void Init(int consumableId, int num,float y)
    {
        ConsumableData data = GameDataMgr.My.GetConsumableDataByID(consumableId);
        consumableSprite.sprite = Resources.Load<Sprite>("Sprite/Consumable/" + consumableId.ToString());
        consumableName.text = data.consumableName;
        consumableDesc.text = data.consumableDesc;
        consumableNum.text = num.ToString();
        MenuShow(y);
    }

    /// <summary>
    /// 菜单显示
    /// </summary>
    /// <param name="y"></param>
    public void MenuShow(float y)
    {
        Stop(y);
        //transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
        tweener = transform.DOLocalMoveX(-703f, 0f).SetUpdate(true);
    }

    /// <summary>
    /// 菜单隐藏
    /// </summary>
    public void MenuHide()
    {
        tweener.Kill();
        tweener = transform.DOLocalMoveX(-1132f, 0f).SetUpdate(true);
    }

    public void Stop(float y)
    {
        tweener.Kill();
        transform.localPosition = new Vector3(-1132f, y, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        MenuHide();
    }
}
