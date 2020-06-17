using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class KeyCodeManager : MonoBehaviour
{
    public GameObject createPanel;
    public GameObject getPanel;

    public Transform createContent;
    public Transform getContent;

    public Button createPanelButton;
    public Button getPanelButton;
    public Button createButton;
    public Button getButton;
    public Button createCopyAll;
    public Button getCopyAll;
    public Button createReturn;
    public Button getReturn;

    public Dropdown createDp;
    public Dropdown getDp;

    public Transform kcTransform;
    public Text tipText;

    private SortedDictionary<string, string> keyValues;
    private List<string> keycodes;
    private string copyList = "";
    private string keycode = "";
    private bool isCreate = false;

    // Start is called before the first frame update
    void Start()
    {
        keyValues = new SortedDictionary<string, string>();
        keycodes = new List<string>();

        createPanelButton.onClick.AddListener(GotoCreatePanel);
        getPanelButton.onClick.AddListener(GotoGetPanel);
    }

    private void GotoCreatePanel()
    {
        createPanel.SetActive(true);
        isCreate = true;
        createCopyAll.interactable = false;
        createButton.onClick.RemoveAllListeners();
        createCopyAll.onClick.RemoveAllListeners();
        createButton.onClick.AddListener(CreateButton);
        createCopyAll.onClick.AddListener(CopyAll);
        createReturn.onClick.AddListener(ReturnButton);
    }

    private void GotoGetPanel()
    {
        getPanel.SetActive(true);
        isCreate = false;
        getCopyAll.interactable = false ;
        getButton.onClick.RemoveAllListeners();
        getCopyAll.onClick.RemoveAllListeners();
        getButton.onClick.AddListener(GetButton);
        getCopyAll.onClick.AddListener(CopyAll);
        getReturn.onClick.AddListener(ReturnButton);
    }

    private void CreateButton()
    {
        createButton.interactable = false;
        createCopyAll.interactable = false;
        
        keyValues.Clear();
        keyValues.Add("createCount", createDp.captionText.text);
        Debug.LogError(createDp.captionText.text);
        keycodes.Clear();
        StartCoroutine(HttpManager.My.HttpSend(Url.creatKeyUrl, (www) => {
            ResponseJson rj = JsonUtility.FromJson<ResponseJson>(www.downloadHandler.text);
            if(rj.status == 1)
            {
                SetTipText("创建成功!");
                keycode = "";
                copyList = "";
                for(int i=0; i< rj.data.Length; i++)
                {
                    keycode += rj.data[i];
                    copyList += rj.data[i];
                    if ((i+1) % 12 == 0)
                    {
                        keycodes.Add(keycode);
                        keycode = "";
                        copyList += "\n";
                    }
                }
                CreateKeyCodes(createContent);
            }
            else
            {
                SetTipText(rj.data);
            }
        }, keyValues, HttpType.Post));
    }

    private void GetButton()
    {
        getButton.interactable = false;
        getCopyAll.interactable = false;
        
        keyValues.Clear();
        keyValues.Add("getCount", getDp.captionText.text);
        Debug.LogError(getDp.captionText.text);
        keycodes.Clear();
        StartCoroutine(HttpManager.My.HttpSend(Url.getKeyUrl, (www) => {
            ResponseJson rj = JsonUtility.FromJson<ResponseJson>(www.downloadHandler.text);
            if (rj.status == 1)
            {
                SetTipText("获取成功!");
                keycode = "";
                copyList = "";
                for (int i = 0; i < rj.data.Length; i++)
                {
                    keycode += rj.data[i];
                    copyList += rj.data[i];
                    if ((i + 1) % 12 == 0)
                    {
                        keycodes.Add(keycode);
                        keycode = "";
                        copyList += "\n";
                    }
                }
                CreateKeyCodes(getContent);
            }
            else
            {
                SetTipText(rj.data);
            }
        }, keyValues, HttpType.Post));
    }

    private void CopyAll()
    {
        GUIUtility.systemCopyBuffer = copyList;
        SetTipText("复制成功!");
    }

    private void ReturnButton()
    {
        if (isCreate)
        {
            createReturn.onClick.RemoveAllListeners();
            createPanel.SetActive(false);
            ClearContent(createContent);
        }
        else
        {
            getReturn.onClick.RemoveAllListeners();
            getPanel.SetActive(false);
            ClearContent(getContent);
        }
    }

    private void ClearContent(Transform content)
    {
        Debug.Log("clear");
        foreach(Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }

    private void CreateKeyCodes(Transform parent)
    {
        foreach(string code in keycodes)
        {
            CreatKeyCode(code, parent);
        }
        if (isCreate)
        {
            createCopyAll.interactable = true;
            createButton.interactable = true;
        }
        else
        {
            getCopyAll.interactable = true;
            getButton.interactable = true;
        }
    }

    private void CreatKeyCode(string code, Transform parent)
    {
        GameObject kc = Instantiate(kcTransform.gameObject, parent);
        KeyCodeSample keyCodeSample = kc.GetComponent<KeyCodeSample>();
        keyCodeSample.Setup(code);
    }

    void SetTipText(string tip)
    {
        tipText.text = tip;
        if (tipText.gameObject.activeInHierarchy)
        {
            //int dk = DOTween.KillAll();
            DOTween.Kill("tip");
            //Debug.LogError("stop "+dk);
            tipText.DOFade(1, 0.02f);
        }
        tipText.gameObject.SetActive(true);
        tipText.DOFade(0, 2).SetId("tip").OnComplete(() => {
            tipText.gameObject.SetActive(false);
            tipText.DOFade(1, 0.02f);
        });
    }
}

[Serializable]
class ResponseJson
{
    public int status;
    public string data;
}