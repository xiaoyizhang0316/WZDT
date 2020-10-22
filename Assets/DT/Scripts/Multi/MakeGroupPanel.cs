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

    public void MakeGroup()
    {
        int portNum = int.Parse(port.text);
        PlayerData.My.transform.GetComponent<Server>().SetUpServer(portNum);
    }

    public void JoinGroup()
    {
        int portNum = int.Parse(port.text);
        PlayerData.My.transform.GetComponent<Client>().ConnectToServer(ip.text, portNum);
    }

    public void Show()
    {
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -559);
    }

    public void Hide()
    {
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(10000,10000);
    }

    private void Start()
    {
        ip.text = NetManager.My.GetIP(ADDRESSFAM.IPv4);
        port.text = "3101";
        makeGroupButton.onClick.AddListener(() =>
        {
            MakeGroup();
        });
        joinButton.onClick.AddListener(() => {
            JoinGroup();
        });
        Hide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Show();
        }
    }
}
