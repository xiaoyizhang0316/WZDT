using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FloatInfoManager : MonoSingleton<FloatInfoManager>
{
    public Transform moneyPos;

    public Transform techpos;

    public GameObject textPrb;



    /// <summary>
    /// 金钱变化时浮动文字信息
    /// </summary>
    /// <param name="number"></param>
    public void MoneyChange(int number)
    {
        if (CommonParams.fteList.Contains(SceneManager.GetActiveScene().name))
        {
            return;
        }
        if (number == 0)
            return;
        GameObject go = Instantiate(textPrb, transform);
        go.transform.localPosition = moneyPos.localPosition + new Vector3(-130f, -10f, 0f) + new Vector3(Random.Range(-10,10),Random.Range(-10,10),0f);
        if (number > 0)
        {
            go.GetComponent<Text>().text = "+" + number.ToString() + "$";
            go.GetComponent<Text>().color = Color.yellow;
        }
        else
        {
            go.GetComponent<Text>().text = number.ToString() + "$";
            go.GetComponent<Text>().color = Color.red;
        }
        go.transform.DOLocalMoveY(30f,1f).OnComplete(()=> {
            Destroy(go);
        }).Play();
    }

    /// <summary>
    /// 科技点变化时浮动信息
    /// </summary>
    /// <param name="number"></param>
    public void TechChange(int number)
    {
        if (number == 0)
            return;
        GameObject go = Instantiate(textPrb, transform);
        go.transform.localPosition = techpos.localPosition + new Vector3(-130f, -10f, 0f);
        if (number > 0)
        {
            go.GetComponent<Text>().text = "+" + number.ToString();
            go.GetComponent<Text>().color = Color.blue;
        }
        else
        {
            go.GetComponent<Text>().text = number.ToString();
            go.GetComponent<Text>().color = Color.red;
        }
        go.transform.DOLocalMoveY(-30f, 1f).OnComplete(() => {
            Destroy(go);
        }).Play();
    }
}
