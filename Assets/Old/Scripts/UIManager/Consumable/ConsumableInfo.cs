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
    public Tweener tweener1;

    /// <summary>
    /// 消耗品初始化
    /// </summary>
    /// <param name="consumableId"></param>
    /// <param name="num"></param>
    /// <param name="y"></param>
    public void Init(int consumableId, int num,Vector3 y)
    {
        ConsumableData data = GameDataMgr.My.GetConsumableDataByID(consumableId);
         Debug.Log(consumableId);
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
    public void MenuShow(Vector3 pos)
    {
        Stop(pos.y);
         transform.position = pos;
        tweener = transform.DOLocalMoveY(transform.localPosition.y+30, 0.3f).SetUpdate(true).Play();
        tweener1 = transform.DOScale(1, 0.3f).SetUpdate(true).Play();
        
        
    }

    /// <summary>
    /// 菜单隐藏
    /// </summary>
    public void MenuHide()
    {
        tweener.Kill();
        tweener1.Kill();
        tweener = transform.DOLocalMoveY(transform.localPosition.y-30, 0.3f).SetUpdate(true).Play();
        tweener1 = transform.DOScale(0, 0.3f).SetUpdate(true).Play();

    }

    public void Stop(float y)
    {
        tweener.Kill();
        tweener1.Kill();
        transform.localPosition = new Vector3(-1132f, -3999, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        MenuHide();
    }
}
