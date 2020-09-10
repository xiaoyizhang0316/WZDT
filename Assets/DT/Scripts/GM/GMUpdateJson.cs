using System;
using System.Collections;
using System.Collections.Generic;
using Google.Protobuf;
using UnityEngine;
using UnityEngine.UI;

public class GMUpdateJson : MonoBehaviour
{
    public Dropdown dropdown;
    public Dropdown jndropdown;
    public Button select;
    public InputField content;
    //public InputField jsonName;
    List<string> dropDownText = new List<string>();
    List<string> jndropDownText = new List<string>();
    SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
    string[] str = {"FTE_0","FTE_1", "FTE_2", "FTE_3", "FTE_4", "FTE_5", "FTE_6", "FTE_7", "FTE_8", "FTE_9" };

    JsonTye jsonTye;
    bool isDoubleConfirm = false;

    private void Start()
    {
        UpdateDropDown();
        dropdown.onValueChanged.AddListener((i)=>ShowSelect(i));
        select.onClick.AddListener(SelectConfirm);
    }

    public void UpdateDropDown()
    {
        dropdown.options.Clear();
        Dropdown.OptionData temp;
        foreach(string jt in Enum.GetNames(typeof(JsonTye)))
        {
            dropDownText.Add(jt);
            temp = new Dropdown.OptionData();
            temp.text = jt;
            dropdown.options.Add(temp);
        }
        dropdown.captionText.text = dropDownText[0];
        UpdateJnDropDown(0);
    }

    private void ShowSelect(int i)
    {
        Debug.Log(dropdown.captionText.text);
        JsonTye jt = (JsonTye)Enum.Parse(typeof(JsonTye), dropdown.captionText.text);

        UpdateJnDropDown(jt);
    }

    private void UpdateJnDropDown(JsonTye jt)
    {
        jndropDownText.Clear();
        switch (jt)
        {
            case JsonTye.StageEnemy:
                for (int i = 0; i < str.Length; i++)
                {
                    jndropDownText.Add(str[i]);
                }
                break;
            case JsonTye.StageNpc:
                for(int i = 0; i<str.Length; i++)
                {
                    jndropDownText.Add(str[i]);
                }
                break;
            case JsonTye.BuffData:
                jndropDownText.Add(JsonTye.BuffData.ToString());
                break;
            case JsonTye.ConsumableData:
                jndropDownText.Add(JsonTye.ConsumableData.ToString());
                break;
            case JsonTye.ConsumerTypeData:
                jndropDownText.Add(JsonTye.ConsumerTypeData.ToString());
                break;
            case JsonTye.EquipData:
                jndropDownText.Add(JsonTye.EquipData.ToString());
                break;
            case JsonTye.RoleTemplateData:
                jndropDownText.Add(JsonTye.RoleTemplateData.ToString());
                break;
            case JsonTye.SkillData:
                jndropDownText.Add(JsonTye.SkillData.ToString());
                break;
            case JsonTye.StageData:
                jndropDownText.Add(JsonTye.StageData.ToString());
                break;
            case JsonTye.TradeSkillData:
                jndropDownText.Add(JsonTye.TradeSkillData.ToString());
                break;
            case JsonTye.TranslateData:
                jndropDownText.Add(JsonTye.TranslateData.ToString());
                break;
            case JsonTye.WorkerData:
                jndropDownText.Add(JsonTye.WorkerData.ToString());
                break;
        }

        jndropdown.options.Clear();
        Dropdown.OptionData temp;
        foreach (string s in jndropDownText)
        {
            //jndropDownText.Add(s);
            temp = new Dropdown.OptionData();
            temp.text = s;
            jndropdown.options.Add(temp);
        }
        jndropdown.captionText.text = jndropDownText[0];
    }

    private void SelectConfirm()
    {
        jsonTye = (JsonTye)Enum.Parse(typeof(JsonTye), dropdown.captionText.text);
        Debug.Log("select " + dropdown.value+" " + jsonTye);
        string err = "";

        string jsonContent = content.text;

        if(jsonContent.Replace(" ", "")=="")
        {
            Debug.Log("内容为空");
            HttpManager.My.ShowTip("上传失败，内容为空");
        }
        else
        {
            if (isDoubleConfirm)
            {
                UploadJson(jsonContent);
            }
            else
            {
                if (jsonTye == JsonTye.StageEnemy || jsonTye == JsonTye.StageNpc)
                {
                    HttpManager.My.ShowTwoClickTip("是否请确认Json名字和内容是否正确？", () => { isDoubleConfirm = true; }, () => { UploadJson(jsonContent); });
                }
                else
                {
                    UploadJson(jsonContent);
                }
            }
        }
    }

    private void UploadJson(string jsonContent)
    {
        string err = "";
        Debug.Log("upload");
        if (GMData.IsTrueContent(jsonContent, jsonTye, out err))
        {
            // 上传json
            Debug.Log(err);
            ModifyJson modifyJson = new ModifyJson();
            modifyJson.JsonContent = jsonContent;
            modifyJson.JsonName = jndropdown.captionText.text;
            modifyJson.JsonType = jsonTye.ToString();
            keyValues.Clear();
            var bytes = modifyJson.ToByteArray();
            string request = Base64Convert.ByteArrayToString(bytes);
            request = CompressUtils.Compress(request);
            keyValues.Add("request", request);

            StartCoroutine(HttpManager.My.HttpSend("http://39.106.226.52:8080/modifyJson", (www) =>
            {
                //GMAck gMAck = GMAck.Parser(www.downloadHandler.text);
                ResponseTest responseTest = JsonUtility.FromJson<ResponseTest>(www.downloadHandler.text);
                var buf = Base64Convert.StringToByteArray(responseTest.response);

                GMAck gMAck = GMAck.Parser.ParseFrom(buf);
                if (gMAck.IsSuccess)
                {
                    HttpManager.My.ShowTip("修改成功");
                }
                else
                {
                    HttpManager.My.ShowTip(gMAck.ErrMsg);
                }

                Debug.Log(gMAck.Status);
            }, keyValues, HttpType.Post));
        }
        else
        {
            Debug.Log(err);
            HttpManager.My.ShowTip(err);
        }
        isDoubleConfirm = false;
    }
}
