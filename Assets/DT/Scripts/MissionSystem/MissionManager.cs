using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : IOIntensiveFramework.MonoSingleton.MonoSingleton<MissionManager>
{
    public Text missionText;

    public Transform contentTF;

    public GameObject missionPrb;
    public GameObject mainMissionPrb;

    public List<MissionSign> signs;

    public void AddMission(MissionData data)
    {
        GameObject sign;
        if (data.isMainmission)
        {
            sign = Instantiate(mainMissionPrb, contentTF);
        }
        else
        {
            sign = Instantiate(missionPrb, contentTF);
        }

        sign.GetComponent<MissionSign>().Init( data);
        
    }

    public void ChangeTital(string content)
    {
        missionText.text = content;
    }


    public void ClearSigns()
    {
        for (int i = 0; i < signs.Count; i++)
        {
            signs[i].transform.DOLocalMoveX(signs[i].transform.localPosition.x - 100, 0.3f);
            Destroy(signs[i].gameObject,0.3f);
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
