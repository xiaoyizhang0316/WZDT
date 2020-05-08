using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RoleInfoItem : MonoBehaviour
{
    /// <summary>
    /// 显示的角色属性名称
    /// </summary>
    public string attriName;

    private int needNum;

    /// <summary>
    /// 为角色属性赋上对应的数字
    /// </summary>
    /// <param name="num"></param>
    public void SetText(int num)
    {
        GetComponent<Text>().text = attriName + "  " + num.ToString();
    }

    /// <summary>
    /// 为角色的属性条赋值
    /// </summary>
    /// <param name="num"></param>
    public void SetSlide(int num)
    {
        Tweener tweener = GetComponent<Slider>().DOValue(num, 0.7f);
        tweener.SetUpdate(true);
        //GetComponent<Slider>().value = num;
    }

    /// <summary>
    /// 为角色的属性条赋值
    /// </summary>
    /// <param name="num"></param>
    public void SetSlide(int num,int need)
    {
        CheckColor(num);
        needNum = need;
        Tweener tweener = GetComponent<Slider>().DOValue(num, 0.7f);
        tweener.SetUpdate(true);
        //GetComponent<Slider>().value = num;
    }

    public void SetPosition(int num)
    {
        float x = (num - 75) * 350f / 150f;
        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
    }

    public void CheckColor(int num)
    {
        if (name.Contains("装备"))
        {
            if (num < needNum)
            {
                transform.Find("Fill Area/Fill").GetComponent<Image>().color = new Color(1f, 0f, 0f, 0.5f);
            }
            else
            {
                transform.Find("Fill Area/Fill").GetComponent<Image>().color = new Color(0f, 1f, 0f, 0.5f);
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        try
        {
            GetComponent<Slider>().maxValue = 100;
        }
        catch (System.Exception e)
        {

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
