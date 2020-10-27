<<<<<<< HEAD
﻿using System.Collections;
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

=======
﻿using System.Collections;
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

>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
    public string MakeGroup()
    {
        int portNum = int.Parse(port.text);
        return PlayerData.My.transform.GetComponent<Server>().SetUpServer(portNum);
<<<<<<< HEAD
    }

=======
    }

>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
    public string JoinGroup()
    {
        int portNum = int.Parse(port.text);
        return PlayerData.My.transform.GetComponent<Client>().ConnectToServer(ip.text, portNum);
<<<<<<< HEAD
    }

=======
    }

>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
    public void Show()
    {
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -559);
        errorInfo.gameObject.SetActive(false);
<<<<<<< HEAD
    }

=======
    }

>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
    public void Hide()
    {
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(10000,10000);
        errorInfo.gameObject.SetActive(false);
<<<<<<< HEAD
    }

=======
    }

>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
    public void SetErrorInfo(string str)
    {
        errorInfo.gameObject.SetActive(true);
        errorInfo.text = str;
<<<<<<< HEAD
    }

=======
    }

>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
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
<<<<<<< HEAD
    }

=======
    }

>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Show();
        }
<<<<<<< HEAD
    }
}
=======
    }
}
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
