using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T3_2 : BaseGuideStep
{
    public List<int> tempEquipList;
    public List<int> RealEquipList;
    public List<int> tempWorkerList;
    public List<int> RealWorkerList;
    public GameObject Panel;
    public Transform pos_equip;
    public Transform pos_worker;
    public GameObject equipPrb;
    public GameObject workerPrb;
    public override IEnumerator StepStart()
    {
        Panel.SetActive(true);
        for (int i = 0; i <tempEquipList.Count; i++)
        {
            PlayerData.My.GetNewGear(tempEquipList[i]);
   
        }
        for (int i = 0; i < pos_equip.transform.childCount; i++)
        {
            Destroy(pos_equip.transform.GetChild(0).gameObject);
        }
        for (int i = 0; i < tempEquipList.Count; i++)
        {
            GameObject equip = Instantiate(equipPrb, pos_equip);
            equip.GetComponent<WinEquipSign>().Init(tempEquipList[i] );  
            
           
        }
        
        for (int i = 0; i <tempWorkerList.Count; i++)
        {
            PlayerData.My.GetNewWorker(tempWorkerList[i]);
   
        }
        for (int i = 0; i < pos_worker.transform.childCount; i++)
        {
            Destroy(pos_worker.transform.GetChild(0).gameObject);
        }
        for (int i = 0; i < tempWorkerList.Count; i++)
        {
            GameObject worker = Instantiate(workerPrb, pos_worker);
            worker.GetComponent<WinWorkerSign>().Init(tempWorkerList[i] );    
        }
        yield return new WaitForSeconds(1);

    }

    public override IEnumerator StepEnd()
    {
        Panel.SetActive(false);
        yield return new WaitForSeconds(1);
    }
 
}
