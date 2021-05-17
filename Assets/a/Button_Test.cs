using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Button_Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { 
      GetComponent<Button>().onClick.AddListener(() =>
      {
          Debug.Log("点击按钮");
      });
      GetComponent<Button>().onClick.AddListener(() =>
      {
          Debug.Log("点击");
      });
    }

    public void GameMainSignPathsGoAgent()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
