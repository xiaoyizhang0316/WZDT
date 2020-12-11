using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameEnum;

public class BaikePanel : MonoSingleton<BaikePanel>
{

    public Transform baikeListTF;

    public GameObject baikeItemPrb;

    public void Init()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(29, 0);
        for (int i = 0; i < baikeListTF.childCount; i++)
        {
            Destroy(baikeListTF.GetChild(i).gameObject);
        }
        List<RoleType> typeList = NetworkMgr.My.roleFoundDic.Keys.ToList();
        for (int i = 0; i < typeList.Count; i++)
        {
            GameObject go = Instantiate(baikeItemPrb, baikeListTF);
            go.GetComponent<BaikeItem>().Init(typeList[i], NetworkMgr.My.roleFoundDic[typeList[i]] == 1);
        }
    }

    public void Hide()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(4000, 4000);
    }

    private void Start()
    {
        Hide();
    }
}
