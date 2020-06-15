using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnToMap : MonoBehaviour
{
    public Button confirm;
    public Button cancel;
    // Start is called before the first frame update
    void Start()
    {
        confirm.onClick.AddListener(Confirm);
        cancel.onClick.AddListener(Cancel);
    }


    void Confirm()
    {
        SceneManager.LoadScene("Map");
    }

    void Cancel()
    {
        gameObject.SetActive(false);
    }
    
}
