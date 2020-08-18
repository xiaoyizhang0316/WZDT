using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumerFloatWindow : MonoBehaviour
{
    public ConsumeSign targetConsume;

    public Image typeSprite;

    public GameObject singleBuffPrb;

    public Transform buffListTF;

    public Text typeName;

    public Text healthText;

    public void Init(ConsumeSign sign)
    {
        if (targetConsume != null)
            BuildingManager.My.GetBuildingByIndex(targetConsume.buildingIndex).StopShowPathLine();
        CancelInvoke("Close");
        targetConsume = sign;
        string path = "Sprite/ConsumerType/" + targetConsume.consumerType.ToString();
        typeSprite.sprite = Resources.Load<Sprite>(path);
        List<int> buffList = new List<int>();
        for (int i = 0; i < targetConsume.bornBuffList.Count; i++)
        {
            buffList.Add(targetConsume.bornBuffList[i]);
        }
        ClearList();
        for (int i = 0; i < buffList.Count; i++)
        {
            if (i > 1)
            {
                if (BuildingManager.My.GetBuildingByIndex(targetConsume.buildingIndex).isUseTSJ)
                {
                    GameObject go = Instantiate(singleBuffPrb, buffListTF);
                    go.GetComponent<WaveBuffSign>().Init(buffList[i]);
                }
                else
                {
                    GameObject go = Instantiate(singleBuffPrb, buffListTF);
                    go.GetComponent<WaveBuffSign>().Init(999);
                }
            }
            else
            {
                GameObject go = Instantiate(singleBuffPrb, buffListTF);
                go.GetComponent<WaveBuffSign>().Init(buffList[i]);
            }
        }
        typeName.text = targetConsume.consumeData.consumerName;
        healthText.text = "满意度需要：" + targetConsume.consumeData.maxHealth.ToString();
        BuildingManager.My.GetBuildingByIndex(targetConsume.buildingIndex).ShowPathLine();
        Invoke("Close", 5f);
    }

    public void ClearList()
    {
        int num = buffListTF.childCount;
        for (int i = 0; i < num; i++)
        {
            DestroyImmediate(buffListTF.GetChild(0).gameObject);
        }
    }

    public void Close()
    {
        BuildingManager.My.GetBuildingByIndex(targetConsume.buildingIndex).StopShowPathLine();
        gameObject.SetActive(false);
        FloatWindow.My.Hide();
    }
}
