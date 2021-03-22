using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TSJ : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public int costTechNumber;

    public GameObject goCopy;

    public GameObject effectPrb;

    public Text costNumber;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(1))
            return;
        goCopy = Instantiate(gameObject, transform.parent);
        goCopy.transform.DOScale(1f, 0f).Play();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(1))
            return;
        if (goCopy == null)
            return;
        Vector3 pos = new Vector3();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(goCopy.GetComponent<RectTransform>(), eventData.position,
        Camera.main, out pos);
        //goCopy.transform.position = pos;
        goCopy.transform.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (goCopy == null)
            return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hit = Physics.RaycastAll(ray);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.CompareTag("Building"))
            {
                if (!hit[i].transform.GetComponent<Building>().isUseTSJ)
                {
                    if (StageGoal.My.CostTechPoint(costTechNumber))
                    {
                        StageGoal.My.CostTp(costTechNumber, CostTpType.Mirror);
                        AudioManager.My.PlaySelectType(GameEnum.AudioClipType.ThreeMirror);
                        hit[i].transform.GetComponent<Building>().UseTSJ();
                        GameObject effect = Instantiate(effectPrb, hit[i].transform);
                        effect.transform.localPosition = Vector3.zero;
                        //Debug.Log("使用透视镜成功");
                        DataUploadManager.My.AddData(DataEnum.使用透视镜);
                        if (!PlayerData.My.isSOLO)
                        {
                            string str1 = "UseThreeMirror|";
                            str1 += "2";
                            str1 += "," + hit[i].transform.GetComponent<Building>().buildingId;
                            str1 += "," + costTechNumber.ToString();
                            if (PlayerData.My.isServer)
                            {
                                PlayerData.My.server.SendToClientMsg(str1);
                            }
                            else
                            {
                                PlayerData.My.client.SendToServerMsg(str1);
                            }
                        }
                        break;
                    }
                    else
                    {
                        //NPCListInfo.My.ShowHideTipPop("科技点数不足！");
                        break;
                    }
                }
            }
        }
        Destroy(goCopy);
    }

    // Start is called before the first frame update
    void Start()
    {
        costNumber.text = costTechNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        costNumber.color = costTechNumber <= StageGoal.My.playerTechPoint ? Color.white : Color.red;
    }
}
