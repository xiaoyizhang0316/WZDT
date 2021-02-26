using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WaveNumber : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{

    public int buildingNumber;

    public void Init(int number)
    {
        buildingNumber = number;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BuildingManager.My.GetBuildingByIndex(buildingNumber).StopShowPathLine();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        BuildingManager.My.GetBuildingByIndex(buildingNumber).ShowPathLine();
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
