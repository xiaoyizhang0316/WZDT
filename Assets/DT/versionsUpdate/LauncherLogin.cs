using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LauncherLogin : MonoBehaviour
{
    public InputField username_Input;
    public InputField password_Input;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username_Input.isFocused)
            {
                password_Input.ActivateInputField();
                password_Input.MoveTextEnd(true);
            }
        }

    }

    public void Login()
    {
        //wj
    }

    /// <summary>
    /// 保存账号密码
    /// </summary>
    public void SaveAccount()
    {
        
    }

    /// <summary>
    /// 更新
    /// </summary>
    public void UpdateGame()
    {
        
        
    }
}
