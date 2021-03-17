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
        
    
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
     
    }


    public void OnPointerExit(PointerEventData eventData)
    {
         
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
}