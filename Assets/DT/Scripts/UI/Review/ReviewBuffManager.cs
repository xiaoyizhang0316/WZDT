using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviewBuffManager : IOIntensiveFramework.MonoSingleton.MonoSingleton<ReviewBuffManager>
{
    public Transform content;

    public Button close;

    public GameObject buffprb;
    public GameObject buffInfo;

    public Text roleName;

    public GameObject buffContent;

    public Text buffText;
    // Start is called before the first frame update
    void Start()
    {
        buffInfo.SetActive(false);
        buffContent.SetActive(false);
        close.onClick.AddListener(() =>
        {
            buffInfo.SetActive(false);
            close.gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitRoleBuff(ReviewRoleSign sign)
    {
        buffInfo.SetActive(true);
        close.gameObject.SetActive(true);

        buffInfo.transform.position = sign.transform.position;
        roleName.text = sign.role.roleName;

        for (int i = 0; i <content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
            
        }

        for (int i = 0; i <sign.role.buffList.Count; i++)
        {
           GameObject buff =  Instantiate(buffprb, content);
           buff.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/buff/"+sign.role.buffList[i]);
        }
    }

    public void ShowBuffContent(string content)
    {
        buffContent .SetActive(true);
        buffText.text = content;
    }

    public void CloseBuffContent()
    {
        buffContent.SetActive(false);
    }
}
