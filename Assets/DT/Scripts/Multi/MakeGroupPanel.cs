using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeGroupPanel : MonoSingleton<MakeGroupPanel>
{
    public Button makeGroupButton;

    public Button joinButton;

    public InputField ip;

    public InputField port;

    public InputField groupName;

    public Text errorInfo;

    public string MakeGroup()
    {
        int portNum = int.Parse(port.text);
        return PlayerData.My.transform.GetComponent<Server>().SetUpServer(portNum);
    }

    public string JoinGroup()
    {
        int portNum = int.Parse(port.text);
        return PlayerData.My.transform.GetComponent<Client>().ConnectToServer(ip.text, portNum);
    }

    public void Show()
    {
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -559);
        errorInfo.gameObject.SetActive(false);
    }

    public void Hide()
    {
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(10000,10000);
        errorInfo.gameObject.SetActive(false);
    }

    public void SetErrorInfo(string str)
    {
        errorInfo.gameObject.SetActive(true);
        errorInfo.text = str;
    }

    private void Start()
    {
        ip.text = NetManager.My.GetIP(ADDRESSFAM.IPv4);
        port.text = "3101";
        makeGroupButton.onClick.AddListener(() =>
        {
            if (groupName.text.Trim().Length == 0)
            {
                string str1 = "队伍名不能为空！";
                SetErrorInfo(str1);
                return; 
            }
            string str = MakeGroup();
            if (str.Equals("true"))
            {
                Hide();
            }
            else
            {
                SetErrorInfo(str);
            }
        });
        joinButton.onClick.AddListener(() => {
            string str = JoinGroup();
            if (str.Equals("true"))
            {
                Hide();
            }
            else
            {
                SetErrorInfo(str);
            }
        });
        Hide();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                Show();
            }
        }
    }
}
