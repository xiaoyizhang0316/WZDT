using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 拖拽装备和角色UI
/// </summary>
public class DragUI : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public DragType dragType;
    public Vector3 startPos;


    public enum DragType
    {
        equip,
        people,
    }

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    /// <summary>
    /// 检测所有位置是否全部已经放入 isReleased -是否之前放到过地图中
    /// </summary>
    public bool CheckAllRight(bool isReleased)
    {
        int index = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<BaseAssembleUISign>().lastPlot != null &&
                transform.GetChild(i).GetComponent<BaseAssembleUISign>().isRelease)
            {
                index++;
            }
        }
        if (index == transform.childCount)
        {
            if (!transform.GetChild(0).GetComponent<BaseAssembleUISign>().lastPlot.GetComponent<PlotSign>().isOccupied)
            {
                Remove();
                Debug.Log("占用销毁"+gameObject.name);
                Destroy(this.gameObject, 0f);
                return false;
            }
            else
            {
                Adsorb(transform.GetChild(0).GetComponent<BaseAssembleUISign>().lastpos + transform.position, Save);
                return true;
                
            }

            // Adsorb(transform.GetChild(0).GetComponent<BaseAssembleUISign>(). lastPlot.position,Save);
            //Save();
            //CreatRoleManager.My.CheckAllConditions();
        }
        else
        {
            if (isReleased)
            {
                int intCount = 0;
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).GetComponent<BaseAssembleUISign>().lastPlot != null &&
                        transform.GetChild(i).GetComponent<BaseAssembleUISign>().isRelease)
                    {
                        intCount++;
                    }
                }
                if (intCount == 0)
                {
                    Remove();

                    Destroy(this.gameObject, 0f);
                    //CreatRoleManager.My.CheckAllConditions();
                    return false;
                }
                else
                {
                    //回到上一次吸附的地点,并且当前是正确的点
                    Adsorb(startPos);
                    //  CreatRoleManager.My.CheckAllConditions();
                    return true;
                }
            }
            else
            {
                Debug.Log("放下销毁"+gameObject.name);
                Remove();
                Destroy(this.gameObject, 0f);
                // CreatRoleManager.My.CheckAllConditions();
                return false;
            }
        }
    }

    /// <summary>
    /// 吸附
    /// </summary>
    public void Adsorb(Vector3 Pos, Action action = null)
    {
        startPos = Pos;
        Tweener tweener = transform.DOMove(Pos, 0.05f).OnComplete(() => action()).Play();
        tweener.SetUpdate(true);
    }


    public void Save()
    {
        if (dragType == DragType.equip)
        {
            if (CreatRoleManager.My.EquipList.ContainsKey(int.Parse(name.Split('_')[1])))
            {
                Debug.Log(transform.position + "装备" + name.Split('_')[1]);
                CreatRoleManager.My.EquipList[int.Parse(name.Split('_')[1])] = transform.localPosition;
            }
            else
            {
                Debug.Log(transform.position + "装备" + name.Split('_')[1]);

                CreatRoleManager.My.EquipList.Add(int.Parse(name.Split('_')[1]), transform.localPosition);
            }
        }

        if (dragType == DragType.people)
        {
            if (CreatRoleManager.My.peoPleList.ContainsKey(int.Parse(name.Split('_')[1])))
            {
                Debug.Log(transform.position + "人" + name.Split('_')[1]);
                CreatRoleManager.My.peoPleList[int.Parse(name.Split('_')[1])] = transform.localPosition;
            }
            else
            {
                Debug.Log(transform.position + "人" + name.Split('_')[1]);
                CreatRoleManager.My.peoPleList.Add(int.Parse(name.Split('_')[1]), transform.localPosition);
            }
        }
        CreatRoleManager.My.CheckAllConditions();
    }

    public void Remove()
    {
        if (dragType == DragType.equip)
        {
            if (CreatRoleManager.My.EquipList.ContainsKey(int.Parse(name.Split('_')[1])))
            {
                CreatRoleManager.My.EquipList.Remove(int.Parse(name.Split('_')[1]));
            }
            else
            {
            }
        }
        if (dragType == DragType.people)
        {
            if (CreatRoleManager.My.peoPleList.ContainsKey(int.Parse(name.Split('_')[1])))
            {
                CreatRoleManager.My.peoPleList.Remove(int.Parse(name.Split('_')[1]));
            }
            else
            {
            }
        }
        CreatRoleManager.My.CheckAllConditions();
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (dragType == DragType.equip)
        {
            CreatRoleManager.My.ShowEquipListPOPDatal(int.Parse(name.Split('_')[1]));
        }
        else
        {
            CreatRoleManager.My.ShowWorkListPOPDatal(int.Parse(name.Split('_')[1]));

        }
    }
}