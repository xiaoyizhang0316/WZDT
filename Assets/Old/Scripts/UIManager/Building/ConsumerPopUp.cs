using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

public class ConsumerPopUp : MonoSingleton<ConsumerPopUp>
{
    public ConsumeSign currentConsumer;

    public List<Sprite> consumerSatisfyList;

    public Tweener tweener;

    public Tweener tweener2;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="consume"></param>
    /// <param name="y"></param>
    public void Init(ConsumeSign consume,float y)
    {
        currentConsumer = consume;
        InitInfo();
        SetCursor();
        SetSprite();
        MenuShow(y);
    }

    /// <summary>
    /// 初始化文字显示
    /// </summary>
    public void InitInfo()
    {

    }

    /// <summary>
    /// 设置酸甜度指针位置
    /// </summary>
    public void SetCursor()
    {

    }

    /// <summary>
    /// 设置消费者满意度表情
    /// </summary>
    public void SetSprite()
    {

    }

    /// <summary>
    /// 菜单显示
    /// </summary>
    /// <param name="y"></param>
    public void MenuShow(float y)
    {
        Stop(y);
        tweener = transform.DOLocalMoveX(220f, 0f).SetUpdate(true);
        GetComponent<Canvas>().sortingOrder = 0;
    }

    /// <summary>
    /// 菜单隐藏
    /// </summary>
    public void MenuHide()
    {
        tweener = transform.DOLocalMoveX(0f, 0f).SetUpdate(true);
        GetComponent<Canvas>().sortingOrder = -1;
    }

    public void Stop(float y)
    {
        tweener.Kill();
        transform.localPosition = new Vector3(0f, y, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        MenuHide();
    }
}
