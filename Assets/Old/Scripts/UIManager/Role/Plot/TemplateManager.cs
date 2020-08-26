using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class TemplateManager : MonoBehaviour
{
    public Transform bottom;
    public Transform mid;
    public Transform top;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        CreatRoleManager.My.gearTemplate = mid;
        CreatRoleManager.My.workerTemplate = top;
        CreatRoleManager.My.roleTemplate = bottom;
     mid .SetParent( CreatRoleManager.My. template_MidPos);
      top.SetParent( CreatRoleManager.My.template_TopPos);
        CreatRoleManager.My.gearPlot = mid.GetComponentsInChildren<PlotSign>();
        CreatRoleManager.My.workerPlot = top.GetComponentsInChildren<PlotSign>();
        CreatRoleManager.My.PutEquip();
        CreatRoleManager.My.PutWorker();
        OpenMidTemplate(0f);
        yield return new WaitForSeconds(0.01f);
        OpenTopTemplate(0f);
        yield return new WaitForSeconds(0.01f);
        CreatRoleManager.My.CheckAllConditions();
    }

    public void OpenMidTemplate(float time)
    {
        top .GetComponent<Image>().DOFade(0.3f, time).SetUpdate(true).Play();
        top.GetComponent<Image>().raycastTarget = false;
        for (int i = 0; i < top.childCount; i++)
        {
            top.GetChild(i).GetComponent<BoxCollider>().enabled = false;
            top.GetChild(i).GetComponent<Image>().raycastTarget = false;
            top.GetChild(i).GetComponent<Image>().DOFade(0.3f, time).SetUpdate(true).Play();
       
            mid.GetChild(i).GetComponent<BoxCollider>().enabled = true;
            mid.GetChild(i).GetComponent<Image>().raycastTarget = true;
            mid.GetChild(i).GetComponent<Image>().DOFade(1, time).SetUpdate(true).Play();
        }

        for (int i = 0; i <EquipListManager.My.equipPos.childCount; i++)
        {
            for (int j = 0; j <EquipListManager.My.equipPos.GetChild(i).childCount; j++)
            {
                EquipListManager.My.equipPos.GetChild(i).GetChild(j).GetComponent<BoxCollider>().enabled = true;
                EquipListManager.My.equipPos.GetChild(i).GetChild(j).GetComponent<Image>().raycastTarget = true;
                EquipListManager.My.equipPos.GetChild(i).GetChild(j).GetComponent<Image>().DOFade(1, time).Play();
              
            }
        }
        
        for (int i = 0; i <WorkerListManager.My.workerPos.childCount; i++)
        {
            for (int j = 0; j <WorkerListManager.My.workerPos.GetChild(i).childCount; j++)
            {
                WorkerListManager.My.workerPos.GetChild(i).GetChild(j).GetComponent<BoxCollider>().enabled = false;
                WorkerListManager.My.workerPos.GetChild(i).GetChild(j).GetComponent<Image>().raycastTarget = false;
                WorkerListManager.My.workerPos.GetChild(i).GetChild(j).GetComponent<Image>().DOFade(0.3f, time).Play();
              
            }
        }
    }

    public void OpenTopTemplate(float time)
    {
        top .GetComponent<Image>().DOFade(1, time).SetUpdate(true).Play();
        top.GetComponent<Image>().raycastTarget = true;

        for (int i = 0; i < top.childCount; i++)
        {
            mid.GetChild(i).GetComponent<BoxCollider>().enabled = false;
            mid.GetChild(i).GetComponent<Image>().raycastTarget = false;
         //   mid.GetChild(i).GetComponent<Image>().DOFade(1f, 0.3f);
            top.GetChild(i).GetComponent<BoxCollider>().enabled = true;
            top.GetChild(i).GetComponent<Image>().raycastTarget = true;
            top.GetChild(i).GetComponent<Image>().DOFade(1, time).SetUpdate(true);
        }
        for (int i = 0; i <EquipListManager.My.equipPos.childCount; i++)
        {
            for (int j = 0; j <EquipListManager.My.equipPos.GetChild(i).childCount; j++)
            {
                EquipListManager.My.equipPos.GetChild(i).GetChild(j).GetComponent<BoxCollider>().enabled = false;
                EquipListManager.My.equipPos.GetChild(i).GetChild(j).GetComponent<Image>().raycastTarget = false;
                EquipListManager.My.equipPos.GetChild(i).GetChild(j).GetComponent<Image>().DOFade(1f, time).Play();
            }
        }
        for (int i = 0; i <WorkerListManager.My.workerPos.childCount; i++)
        {
            for (int j = 0; j <WorkerListManager.My.workerPos.GetChild(i).childCount; j++)
            {
                WorkerListManager.My.workerPos.GetChild(i).GetChild(j).GetComponent<BoxCollider>().enabled = true;
                WorkerListManager.My.workerPos.GetChild(i).GetChild(j).GetComponent<Image>().raycastTarget = true;
                WorkerListManager.My.workerPos.GetChild(i).GetChild(j).GetComponent<Image>().DOFade(1, time).Play();
            }
        }
    }

    public void OnGUI()
    {
        //if (GUILayout.Button("中间"))
        //{
        //    OpenMidTemplate(0.3f);
        //}
        //if (GUILayout.Button("顶部"))
        //{
        //    OpenTopTemplate(0.3f);
        //}
    }

    // Update is called once per frame
    void Update()
    {
    }
}