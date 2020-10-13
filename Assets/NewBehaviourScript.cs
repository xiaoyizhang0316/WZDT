using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NewBehaviourScript : NetworkBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = Transform.FindObjectOfType<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ClientRpc]
    void RpcOnValeChange( int a)
    {
        Debug.Log("123");
        text.text =a.ToString();
    }

    public void OnGUI()
    {
        if(GUILayout.Button("1"))
        RpcOnValeChange(10);
    }
}
