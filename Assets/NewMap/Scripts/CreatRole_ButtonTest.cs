using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameEnum;

public class CreatRole_ButtonTest : MonoBehaviour, IDragHandler, IPointerClickHandler, IPointerExitHandler,
    IBeginDragHandler, IEndDragHandler,IPointerEnterHandler
{
    public Vector3 world;

    /// <summary>
    /// 角色预制件
    /// </summary>
    public GameObject RolePrb;

    public GameObject role;

    public HexGrid hexGrid;

    public HexMapEditor MapEditor;

    public bool isPlaystart;
    /// <summary>
    /// 窗口开关控制
    /// </summary>
    private bool secondMenuStatus = false;

     
    // Start is called before the first frame update
    void Start()
    {
       
    }

   
    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButton(1))
        {
            Destroy(role, 0f);
            role = null;
          
         
        }
    }
    
    public void OnDrag(PointerEventData eventData)
    {



     MapEditor.HandleInput(role);
   
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
       
       
     
      
        role = Instantiate(RolePrb   );
        if (isPlaystart)
        {
            Camera.main.GetComponent<EditorMapManager>().CreatPrb(role);

        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        role.GetComponent<EditorLandItem>().x = MapEditor.previousCell.coordinates.X;
        role.GetComponent<EditorLandItem>().y = MapEditor.previousCell.coordinates.Z;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
         
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
}