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
        targetConsume = sign;
        string path = "Sprite/ConsumerType/" + targetConsume.consumerType.ToString();
        typeSprite.sprite = Resources.Load<Sprite>(path);
        List<int> buffList = new List<int>();
        for (int i = 0; i < targetConsume.buffList.Count; i++)
        {
            buffList.Add(targetConsume.buffList[i].buffId);
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
    }

    public void ClearList()
    {
        int num = buffListTF.childCount;
        for (int i = 0; i < num; i++)
        {
            DestroyImmediate(buffListTF.GetChild(0).gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
