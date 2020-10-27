using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Client_WaveNumber : MonoBehaviour,IPointerClickHandler
{
    public int waveNumber;

    public int buildingNumber;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (WaveCount.My.showDetail)
        {
            return;
        }
        BuildingManager.My.HideAllPath();
        BuildingManager.My.GetBuildingByIndex(buildingNumber).ShowPathLine();
        WaveCount.My.InitWaveBg(waveNumber, buildingNumber);
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
