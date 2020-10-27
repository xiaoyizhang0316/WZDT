<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Client_WaveNumber : MonoBehaviour,IPointerClickHandler
{
    public int waveNumber;

=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Client_WaveNumber : MonoBehaviour,IPointerClickHandler
{
    public int waveNumber;

>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
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
<<<<<<< HEAD
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
=======
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
