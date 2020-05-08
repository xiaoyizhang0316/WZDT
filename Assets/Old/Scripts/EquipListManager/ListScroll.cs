using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ListScroll : MonoBehaviour
{
    public List<GameObject> equipPosList;

    public Transform equipTF;

    public int index;

    public int currentIndex;

    public GameObject objPrb;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        PlayerPrefs.DeleteAll();
        currentIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CreatOBJInList(GameObject obj)
    {
        for (int i = 0; i < equipPosList.Count; i++)
        {
            if (PlayerPrefs.GetInt(gameObject.name + equipPosList[i].name + index.ToString(), 0) == 0)
            {
                obj.name = equipPosList[i].name + index.ToString();
                obj.transform.position = equipPosList[i].transform.position;
                PlayerPrefs.SetInt(gameObject.name + equipPosList[i].name + index.ToString(), 1);
                if (equipPosList.Count - 1 == i)
                {
                    index++;
                }

                return;
            }
        }
    }


    public void Upturning()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
        }

        UpdateList();
    }

    public void UpdateList()
    {
        for (int i = 0; i < equipTF.childCount; i++)
        {
            equipTF.transform.GetChild(i).gameObject.SetActive(false);
        }


        for (int i = 0; i < equipPosList.Count; i++)
        {
            for (int j = 0; j <equipTF.transform.childCount; j++)
            {
                          if(equipTF.GetChild(j).name ==(i.ToString() + currentIndex.ToString()))
                          {
                              equipTF.GetChild(j).gameObject.SetActive(true);
                          }
            } 
     
         
        }
    }

    public void Downturning()
    {
        if (currentIndex < index)
        {
            currentIndex++;
        }

        UpdateList();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("创建"))
        {
            CreatOBJInList(Instantiate(objPrb, equipTF));
        }

        if (GUILayout.Button("shuaxin"))
        {
            UpdateList();
        }

        if (GUILayout.Button("上翻"))
        {
            Upturning();
        }

        if (GUILayout.Button("下翻"))
        {
            Downturning();
        }
    }
}