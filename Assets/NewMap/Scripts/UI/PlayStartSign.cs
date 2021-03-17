using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayStartSign : MonoBehaviour,IPointerEnterHandler,IPointerClickHandler,IPointerExitHandler
{
    public GameObject port;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (port == Camera.main.GetComponent<EditorMapManager>().currentPort)
        {
            GetComponent<Image>().color = Color.green;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
            
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InitPort();
    }

    public void InitPort()
    {
       
        for (int i = 0; i <  port.GetComponent<EditorConsumerSpot>().paths.Count; i++)
        {
            var cell = HexGrid.My.GetCell(new HexCoordinates(port.GetComponent<EditorConsumerSpot>().paths[i].x,
                port.GetComponent<EditorConsumerSpot>().paths[i].y));
                cell.SetLabel(i.ToString());
                cell.EnableHighlight(Color.grey);
         
        }

       
    }


    public void SetPort()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InitPort();
        Camera.main.GetComponent<EditorMapManager>().currentPort = port;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (port == Camera.main.GetComponent<EditorMapManager>().currentPort)
            return;
        for (int i = 0; i <   HexGrid.My. cells.Length; i++)
        {
            HexGrid.My.cells[i].RefreshSelfOnly();
        }
    }
}
